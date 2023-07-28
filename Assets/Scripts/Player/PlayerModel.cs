using UnityEngine;

public class Player
{
    private readonly Rigidbody rigidBody;
    private readonly MeshRenderer renderer;

    private float health = 100.0f;

    public Player(PlayerInstaller.Settings PlayerSettings)
    {
        rigidBody = PlayerSettings.RigidBody;
        renderer = PlayerSettings.MeshRenderer;
    }

    public MeshRenderer Renderer
    {
        get { return renderer; }
    }

    public bool IsDead
    {
        get; set;
    }

    public float Health
    {
        get { return health; }
    }

    public Vector3 LookDir
    {
        get { return rigidBody.transform.forward; }
        set { rigidBody.transform.forward = value; }
    }

    public Quaternion Rotation
    {
        get { return rigidBody.rotation; }
        set { rigidBody.rotation = value; }
    }

    public Vector3 Position
    {
        get { return rigidBody.position; }
        set { rigidBody.position = value; }
    }

    public Vector3 Velocity
    {
        get { return rigidBody.velocity; }
        set { rigidBody.velocity = value; }
    }

    public void TakeDamage(float healthLoss)
    {
        health = Mathf.Max(0.0f, health - healthLoss);
    }

    public void AddForce(Vector3 force)
    {
        rigidBody.AddForce(force);
    }
}
