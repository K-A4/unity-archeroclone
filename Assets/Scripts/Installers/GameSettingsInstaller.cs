using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public PlayerMovement.Settings PlayerMovement;
    public PlayerFireHandler.Settings PlayerFiring;
    public GameInstaller.Settings GameInstaller;
    public Bullet.Settings Bullet;
    public PlayerRotation.Settings PlayerRotation;
    public NearestEnemyFinder.Settings EnemyFinder;
    public LevelBounds.Settings LevelBounds;
    public Levels.LevelsSettings levelsSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(PlayerMovement).IfNotBound();
        Container.BindInstance(PlayerFiring).IfNotBound();
        Container.BindInstance(GameInstaller).IfNotBound();
        Container.BindInstance(Bullet).IfNotBound();
        Container.BindInstance(PlayerRotation).IfNotBound();
        Container.BindInstance(LevelBounds).IfNotBound();
        Container.BindInstance(EnemyFinder).IfNotBound();
        Container.BindInstance(levelsSettings).IfNotBound();
    }
}
