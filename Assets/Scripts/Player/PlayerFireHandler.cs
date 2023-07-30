using UnityEngine;
using System;
using Zenject;

public class PlayerFireHandler : ITickable, IDisposable
{
    private bool firing = true;
    private readonly SignalBus signalBus;
    private readonly Settings fireSettings;
    private readonly Player player;
    private readonly Bullet.Factory bulletFactory;
    private readonly PlayerRotation playerRotation;
    private readonly GameStarter gameStarter;
    private float timeToFire;
    private float firePeriod { get => 60.0f / fireSettings.FireRate; } 

    public PlayerFireHandler(
        Settings fireSettings, 
        Player player, 
        SignalBus signalBus, 
        Bullet.Factory bulletFactory, 
        PlayerRotation playerRotation, 
        GameStarter gameStarter)
    {
        this.fireSettings = fireSettings;
        this.player = player;
        this.signalBus = signalBus;
        this.bulletFactory = bulletFactory;
        this.playerRotation = playerRotation;
        this.gameStarter = gameStarter;

        signalBus.Subscribe<PlayerInput.StartFire>(StartFire);
        signalBus.Subscribe<PlayerInput.StopFire>(StopFire);
        signalBus.Subscribe<AllEnemiesDied>(StopFire);
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
        if (playerRotation.RotatedOnEnemy && gameStarter.GameStarted && firing)
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
        var bullet = bulletFactory.Create(fireSettings.Speed, fireSettings.Damage);

        bullet.transform.position = player.Position;
        bullet.transform.rotation = player.Rotation;
        bullet.gameObject.layer = LayerMask.NameToLayer("Shot");
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<PlayerInput.StartFire>(StartFire);
        signalBus.Unsubscribe<PlayerInput.StopFire>(StopFire);
        signalBus.Unsubscribe<AllEnemiesDied>(StopFire);
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
