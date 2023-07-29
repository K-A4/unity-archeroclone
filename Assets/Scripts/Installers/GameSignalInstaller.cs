using Zenject;

public class GameSignalInstaller : Installer<GameSignalInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<PlayerInput.StartFire>();
        Container.DeclareSignal<PlayerInput.StopFire>();
        Container.DeclareSignal<StartGame>();
        Container.DeclareSignal<EnemyDied>();
        Container.DeclareSignal<AllEnemiesDied>();
        Container.DeclareSignal<EnterDoor>();
    }
}