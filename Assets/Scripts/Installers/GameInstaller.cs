using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject] private readonly Settings settings;

    public override void InstallBindings()
    {
        GameSignalInstaller.Install(Container);
        Container.BindInterfacesAndSelfTo<GameStarter>().AsSingle();
        BindBullets();
        BindEnemies();
        Container.Bind<LevelBounds>().AsSingle();
    }

    private void BindBullets()
    {
        Container.BindFactory<float, float, Bullet, Bullet.Factory>()
                    .FromMonoPoolableMemoryPool(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .WithMaxSize(15)
                    .FromComponentInNewPrefab(settings.BulletPrefab)
                    .UnderTransformGroup("Bullets"));
    }

    private void BindEnemies()
    {
        Container.Bind<EnemySpawner>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public GameObject BulletPrefab;
        public GameObject Door;
    }
}