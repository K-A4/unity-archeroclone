using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private Settings settings = null;

    [SerializeField]
    private PlayerView player;

    [SerializeField]
    private VirtualGamepad vg = null;

    public override void InstallBindings()
    {
        Container.Bind<Player>().AsSingle().WithArguments(settings);
        Container.Bind<PlayerView>().FromInstance(player);
        Container.BindInterfacesAndSelfTo<PlayerFireHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().WithArguments(vg);
        Container.BindInterfacesTo<PlayerMovement>().AsSingle();
    }

    [System.Serializable]
    public class Settings
    {
        public Rigidbody RigidBody;
        public MeshRenderer MeshRenderer;
    }
}