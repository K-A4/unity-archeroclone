using UnityEngine;
using Zenject;
using System;

public class PlayerMovement : IFixedTickable, IDisposable
{
    private readonly Player player;
    private readonly PlayerInput playerInput;
    private readonly Settings settings;
    private readonly PlayerView playerView;
    private readonly PlayerFireHandler.Settings fireSettings;
    private readonly SignalBus signalBus;
    private Transform chaseTarget;
    private float startChaseTime;

    public PlayerMovement(Player player, PlayerInput input, Settings settings, PlayerView playerView, PlayerFireHandler.Settings fireSettings, SignalBus signalBus)
    {
        this.player = player;
        playerInput = input;
        this.settings = settings;
        this.playerView = playerView;
        this.fireSettings = fireSettings;
        this.signalBus = signalBus;
        signalBus.Subscribe<PlayerInput.StartFire>(StartRotateToEnemy);
    }

    private void StartRotateToEnemy()
    {
        chaseTarget = FindNearestEnemy();
        startChaseTime = 0.0f;
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<PlayerInput.StartFire>(StartRotateToEnemy);
    }

    public void FixedTick()
    {
        if (playerInput.InputVector != Vector2.zero)
        {
            var movingVec3 = new Vector3(playerInput.InputVector.x, 0, playerInput.InputVector.y);
            movingVec3 *= settings.MoveSpeed;
            player.Velocity = Vector3.MoveTowards(player.Velocity, movingVec3, settings.Acceleration * Time.deltaTime);
            player.LookDir = movingVec3;
        }
        else
        {
            if (chaseTarget)
            {
                player.LookDir = Vector3.Lerp(player.LookDir, chaseTarget.position - player.Position, startChaseTime);
                startChaseTime += Time.deltaTime;
            }
        }
    }

    private Transform FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(playerView.transform.position, fireSettings.CheckRadius, fireSettings.LayerMask);
        var minDistance = float.MaxValue;
        var minIndex = 0;
        Transform chaseTarget = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            var distance = (playerView.transform.position - colliders[i].transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        if (colliders.Length != 0)
        {
            chaseTarget = colliders[minIndex].transform;
        }

        return chaseTarget;
    }

    [System.Serializable]
    public class Settings
    {
        public float BoundaryBuffer;
        public float BoundaryAdjustForce;
        public float MoveSpeed;
        public float Acceleration;
    }
}
