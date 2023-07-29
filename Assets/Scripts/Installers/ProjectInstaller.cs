using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<Levels>().AsSingle();
    }
}