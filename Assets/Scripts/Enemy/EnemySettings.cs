using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EnemySettingsInstaller", menuName = "Installers/EnemySettingsInstaller")]
public class EnemySettings : ScriptableObjectInstaller<EnemySettings>
{
    public EnemyHealth.Settings Enemy;
    public EnemyMover.Settings EnemyMove;
    public EnemyFlying.Settings EnemyFlying;
    public CheckForPlayerInSight.Settings PlayerInSight;
    public EnemyFacade.Settings PlayerFacade;

    public override void InstallBindings()
    {
        Container.BindInstance(Enemy).IfNotBound();
        Container.BindInstance(EnemyMove).IfNotBound();
        Container.BindInstance(EnemyFlying).IfNotBound();
        Container.BindInstance(PlayerInSight).IfNotBound();
        Container.BindInstance(PlayerFacade).IfNotBound();
    }
}
