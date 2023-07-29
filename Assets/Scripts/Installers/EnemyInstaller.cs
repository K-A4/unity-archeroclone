using Zenject;

public class EnemyInstaller : MonoInstaller<EnemyInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EnemyDirection>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyShooter>().AsSingle();
        Container.BindInterfacesAndSelfTo<CheckForPlayerInSight>().AsSingle();
    }
}
