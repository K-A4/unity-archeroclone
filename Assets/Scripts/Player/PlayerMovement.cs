using UnityEngine;
using Zenject;
using System;

public class PlayerMovement : IFixedTickable
{
    private readonly Player player;
    private readonly PlayerInput playerInput;
    private readonly Settings settings;
    private readonly LevelBounds levelBounds;
    private readonly SignalBus signalBus;
    private bool moving;

    public PlayerMovement(
        Player player, 
        PlayerInput playerInput, 
        Settings settings, 
        LevelBounds levelBounds, 
        SignalBus signalBus)
    {
        this.player = player;
        this.playerInput = playerInput;
        this.settings = settings;
        this.levelBounds = levelBounds;
        this.signalBus = signalBus;
        signalBus.Subscribe<StartGame>(StartMoving);
    }

    public void FixedTick()
    {
        if (playerInput.InputVector != Vector2.zero && moving)
        {
            var movingVec3 = new Vector3(playerInput.InputVector.x, 0, playerInput.InputVector.y);
            movingVec3 *= settings.MoveSpeed;
            player.Velocity = Vector3.MoveTowards(player.Velocity, movingVec3, settings.Acceleration * Time.deltaTime);
        }
        KeepInLevel();
    }

    private void KeepInLevel()
    {
        player.Position = levelBounds.KeepInBound(player.Position);
    }

    private void StartMoving()
    {
        moving = true;
    }

    [Serializable]
    public class Settings
    {
        public float BoundaryBuffer;
        public float BoundaryAdjustForce;
        public float MoveSpeed;
        public float Acceleration;
    }
}
