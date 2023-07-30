using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {
        GameSignalInstaller.Install(Container);

        Container.Bind<MoneyCollector>().AsSingle();
        Container.Bind<Levels>().AsSingle();
    }
}
