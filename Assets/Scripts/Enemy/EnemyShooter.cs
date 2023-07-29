using UnityEngine;
using Zenject;

public class EnemyShooter : ITickable
{
    private readonly CheckForPlayerInSight inSightChecker;
    private readonly EnemyFacade enemyFacade;
    private Bullet.Factory bulletFactory;
    private EnemyHealth.Settings enemySettings;
    private readonly GameStarter gameStarter;
    private readonly Mover enemyMover;
    private float timeElapsed;

    public EnemyShooter(
        CheckForPlayerInSight inSightChecker, 
        EnemyFacade enemyFacade, 
        Bullet.Factory bulletFactory, 
        EnemyHealth.Settings enemySettings,
        GameStarter gameStarter,
        Mover enemyMover)
    {
        this.inSightChecker = inSightChecker;
        this.enemyFacade = enemyFacade;
        this.bulletFactory = bulletFactory;
        this.enemySettings = enemySettings;
        this.gameStarter = gameStarter;
        this.enemyMover = enemyMover;
    }

    public void Tick()
    {
        if (inSightChecker.CheckPlayerInSight() && gameStarter.GameStarted && !enemyMover.IsMoving())
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time > timeElapsed)
        {
            var bullet = bulletFactory.Create(enemySettings.ShootSpeed, enemySettings.Damage);
            bullet.transform.position = enemyFacade.Transform.position;
            bullet.transform.rotation = enemyFacade.Transform.rotation;
            bullet.gameObject.layer = LayerMask.NameToLayer("EnemyShot");
            timeElapsed = Time.time + (60.0f / enemySettings.FireRate);
        }
    }
}
