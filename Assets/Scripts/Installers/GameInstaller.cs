using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    Settings settings = null;

    public override void InstallBindings()
    {
        GameSignalInstaller.Install(Container);

        //Container.BindFactory<float, float, float, BulletType, Bullet, Bullet.Factory>()
        //        .FromPoolableMemoryPool<float, float, float, BulletType, Bullet, BulletPool>(poolBinder => poolBinder
        //            .WithInitialSize(10)
        //            .FromComponentInNewPrefab(settings.BulletPrefab)
        //            .UnderTransformGroup("Bullets"));
        Container.BindMemoryPool<Bullet, Bullet.Pool>()
            .WithInitialSize(10)
            .WithMaxSize(15)
            .FromComponentInNewPrefab(settings.BulletPrefab)
            .UnderTransformGroup("Bullets");
    }

    [System.Serializable]
    public class Settings
    {
        public GameObject BulletPrefab;
    }

    //class BulletPool : MonoPoolableMemoryPool<float, float, float, BulletType, IMemoryPool, Bullet>
    //{
    //}
}