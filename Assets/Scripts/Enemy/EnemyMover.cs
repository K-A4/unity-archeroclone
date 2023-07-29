using UnityEngine;
using System;
using UnityEngine.AI;
using Zenject;

public class EnemyMover : Mover
{
    public override bool IsMoving()
    {
        return !agent.isStopped;
    }

    private NavMeshAgent agent;
    private Player player;
    private SignalBus signalBus;
    private bool moving;
    private Settings settings;
    private CheckForPlayerInSight inSightChecker;

    [Inject]
    public void Construct(
        Settings settings, 
        Player player, 
        SignalBus signalBus,
        CheckForPlayerInSight inSightChecker)
    {
        this.player = player;
        this.signalBus = signalBus;
        this.settings = settings;
        this.inSightChecker = inSightChecker;

        InitializeAgent(settings);

        signalBus.Subscribe<StartGame>(StartMoving);
    }

    private void InitializeAgent(Settings settings)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = settings.MoveSpeed;
        agent.acceleration = settings.Acceleration;
    }

    private void Update()
    {
        if (moving && !inSightChecker.CheckPlayerInSight())
        {
            agent.isStopped = false;
            agent.SetDestination(player.Position);
        }

        CheckPlayerInRange();

        CheckStopRadius();
    }

    public void CheckPlayerInRange()
    {
        if (agent.remainingDistance > settings.MoveRange)
        {
            agent.isStopped = true;
        }
    }

    private void CheckStopRadius()
    {
        if (agent.remainingDistance < settings.StopRadius)
        {
            agent.isStopped = true;
        }
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
        public float MoveSpeed;
        public float Acceleration;
        public float MoveRange;
        public float StopRadius;
    }
}
