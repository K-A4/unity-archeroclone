using System;
using UnityEngine;
using Zenject;

public class PlayerRotation : ITickable, IDisposable
{
    public bool RotatedOnEnemy;

    private float startChaseTime;

    private readonly PlayerInput playerInput;
    private readonly Player player;
    private readonly SignalBus signalBus;
    private readonly NearestEnemyFinder enemyFinder;
    private readonly Settings settings;

    public PlayerRotation(
        Player player, 
        PlayerInput playerInput, 
        SignalBus signalBus, 
        NearestEnemyFinder enemyFinder, 
        Settings settings)
    {
        this.playerInput = playerInput;
        this.player = player;
        this.signalBus = signalBus;
        this.enemyFinder = enemyFinder;
        this.settings = settings;
        signalBus.Subscribe<PlayerInput.StartFire>(StartRotateToEnemy);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<PlayerInput.StartFire>(StartRotateToEnemy);
    }

    private void StartRotateToEnemy()
    {
        RotatedOnEnemy = false;
        startChaseTime = 0;
    }

    public void Tick()
    {
        if (playerInput.InputVector != Vector2.zero)
        {
            var movingVec3 = new Vector3(playerInput.InputVector.x, 0, playerInput.InputVector.y);
            player.LookDir = movingVec3;
        }
        else
        {
            var chaseTarget = enemyFinder.FindNearestEnemy(player.Position);

            if (chaseTarget)
            {
                var vectorToNearEnemy = chaseTarget.position - player.Position;
                if (Vector3.Dot(player.LookDir, vectorToNearEnemy.normalized) > settings.RotateThreshold)
                {
                    RotatedOnEnemy = true;
                }
                player.LookDir = Vector3.Lerp(player.LookDir, vectorToNearEnemy.normalized, startChaseTime / settings.RotationToEnemyTime);
                startChaseTime += Time.deltaTime;
            }
        }
    }

    [Serializable]
    public class Settings
    {
        public float RotateThreshold;
        public float RotationToEnemyTime;
    }
}
