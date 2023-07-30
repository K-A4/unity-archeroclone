using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Inject]
    private readonly Settings settings;
    [SerializeField] 
    private ParticleSpawner particleSpawner;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameStarter>().AsSingle();
        BindBullets();
        BindEnemies();
        Container.BindInstance(particleSpawner).AsSingle();
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