using UnityEngine;
using Zenject;

public class PlayerMovement : IFixedTickable
{
    private readonly Player player;
    private readonly PlayerInput playerInput;
    private readonly Settings settings;

    public PlayerMovement(Player player, PlayerInput input, Settings settings)
    {
        this.player = player;
        playerInput = input;
        this.settings = settings;
    }

    public void FixedTick()
    {
        if (playerInput.InputVector != Vector2.zero)
        {
            var movingVec3 = new Vector3(playerInput.InputVector.x, 0, playerInput.InputVector.y);
            movingVec3 *= settings.MoveSpeed;
            player.Velocity = Vector3.MoveTowards(player.Velocity, movingVec3, settings.Acceleration * Time.deltaTime);
        }
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
