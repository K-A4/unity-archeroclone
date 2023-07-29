using Zenject;

public class EnemyDirection : ITickable
{
    private readonly Player player;
    private readonly CheckForPlayerInSight inSightChecker;
    private readonly EnemyFacade enemyFacade;

    public EnemyDirection(
        Player player, 
        CheckForPlayerInSight inSightChecker, 
        EnemyFacade enemyFacade)
    {
        this.player = player;
        this.inSightChecker = inSightChecker;
        this.enemyFacade = enemyFacade;
    }

    public void Tick()
    {
        if (inSightChecker.CheckPlayerInSight())
        {
            var direction = player.Position - enemyFacade.Transform.position;
            enemyFacade.Transform.forward = direction;
        }
    }
}
