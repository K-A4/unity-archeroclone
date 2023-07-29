using System;
using UnityEngine;
using Zenject;

public class EnemyFlying : Mover
{
    public override bool IsMoving()
    {
        return moving;
    }

    private bool moving;
    private Player player;
    private SignalBus signalBus;
    private Rigidbody rigidbody;
    private EnemyMover.Settings settings;
    private Settings flySettings;
    private GameStarter gameStarter;
    private float DistanceToPlayer { get => (player.Position - transform.position).magnitude; }
    
    [Inject]
    public void Construct(
        Player player,
        SignalBus signalBus,
        EnemyMover.Settings settings,
        GameStarter gameStarter,
        Settings flysettings)
    {
        this.player = player;
        this.signalBus = signalBus;
        this.settings = settings;
        this.gameStarter = gameStarter;
        this.flySettings = flysettings;

        rigidbody = GetComponent<Rigidbody>();
        signalBus.Subscribe<StartGame>(StartMoving);
    }

    void Update()
    {
        if (gameStarter.GameStarted)
        {
            if (moving)
            {
                var directionToPlayer = (player.Position - transform.position).normalized;
                var movingVec3 = settings.MoveSpeed * directionToPlayer;
                rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, movingVec3, settings.Acceleration * Time.deltaTime);
            }

            if (settings.StopRadius > DistanceToPlayer)
            {
                moving = false;
            }
            else
            {
                moving = true;
            }
        }

        var pos = rigidbody.position;
        var flyPosition = new Vector3(pos.x, flySettings.FlyHeight, pos.z);
        rigidbody.position = Vector3.Lerp(pos, flyPosition, Time.deltaTime * settings.MoveSpeed);
    }

    private void StartMoving()
    {
        moving = true;
    }

    public void OnDestroy()
    {
        signalBus.Unsubscribe<StartGame>(StartMoving);
    }

    [Serializable]
    public class Settings
    {
        public float FlyHeight;
    }
}
