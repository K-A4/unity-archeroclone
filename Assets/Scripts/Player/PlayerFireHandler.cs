using UnityEngine;
using System;
using Zenject;

public class PlayerFireHandler : ITickable, IDisposable
{
    private bool firing = true;
    private readonly SignalBus signalBus;
    private readonly Settings fireSettings;
    private readonly Player player;

    private float timeToFire;
    private float firePeriod { get => fireSettings.FireRate / 60.0f; } 

    public PlayerFireHandler(Settings fireSettings, Player player, SignalBus signalBus)
    {
        this.fireSettings = fireSettings;
        this.player = player;
        this.signalBus = signalBus;
        signalBus.Subscribe<PlayerInput.StartFire>(StartFire);
        signalBus.Subscribe<PlayerInput.StopFire>(StopFire);
    }

    public void StartFire()
    {
        firing = true;
    }

    public void StopFire()
    {
        firing = false;
    }

    public void Tick()
    {
        if (firing)
        {
            if (timeToFire < Time.time)
            {
                Fire();
                timeToFire = Time.time + firePeriod;
            }
        }
    }

    private void Fire()
    {
        Debug.Log("Fire");
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<PlayerInput.StartFire>(StartFire);
        signalBus.Unsubscribe<PlayerInput.StopFire>(StopFire);
    }

    [System.Serializable]
    public class Settings
    {
        public float FireRate;
        public float Damage;
    }
}
