using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Settings settings = null;

    [SerializeField] private VirtualGamepad vg = null;

    public override void InstallBindings()
    {
        Container.Bind<Player>().AsSingle().WithArguments(settings);

        Container.BindInterfacesAndSelfTo<PlayerFireHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().WithArguments(vg);
        Container.BindInterfacesTo<PlayerMovement>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerRotation>().AsSingle();
        Container.BindInterfacesAndSelfTo<NearestEnemyFinder>().AsSingle();
        Container.Bind<MoneyCollector>().AsSingle();
    }

    [System.Serializable]
    public class Settings
    {
        public Rigidbody RigidBody;
        public MeshRenderer MeshRenderer;
    }
}