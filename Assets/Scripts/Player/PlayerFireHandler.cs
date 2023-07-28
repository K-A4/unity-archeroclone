using UnityEngine;
using System;
using Zenject;

public class PlayerFireHandler : ITickable, IDisposable
{
    private bool firing = true;
    private readonly SignalBus signalBus;
    private readonly Settings fireSettings;
    private readonly Player player;
    private readonly Bullet.Pool bulletPool;

    private float timeToFire;
    private float firePeriod { get => 60.0f / fireSettings.FireRate; } 

    public PlayerFireHandler(Settings fireSettings, Player player, SignalBus signalBus, Bullet.Pool bulletPool)
    {
        this.fireSettings = fireSettings;
        this.player = player;
        this.signalBus = signalBus;
        this.bulletPool = bulletPool;
        signalBus.Subscribe<PlayerInput.StartFire>(StartFire);
        signalBus.Subscribe<PlayerInput.StopFire>(StopFire);
    }

    public void StartFire()
    {
        firing = true;
        timeToFire = Time.time + (firePeriod / 10.0f);
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

        var bullet = bulletPool.Spawn(fireSettings.Speed, fireSettings.Damage);

        bullet.transform.position = player.Position;
        bullet.transform.rotation = player.Rotation;
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
        public float Speed;
        public float Damage;
        public float CheckRadius;
        public LayerMask LayerMask;
    }
}
