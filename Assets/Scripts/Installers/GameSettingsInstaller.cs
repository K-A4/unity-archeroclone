using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public PlayerMovement.Settings PlayerMovement;
    public PlayerFireHandler.Settings PlayerFiring;

    public override void InstallBindings()
    {
        Container.BindInstance(PlayerMovement).IfNotBound();
        Container.BindInstance(PlayerFiring).IfNotBound();
    }
}