using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartGame { }

public class GameStarter : ITickable
{
    public bool GameStarted { get; private set; }

    private readonly SignalBus signalBus;

    public GameStarter(SignalBus signalBus)
    {
        this.signalBus = signalBus;
        signalBus.Subscribe<EnterDoor>(StartNewLevel);
    }

    public void Tick()
    {
        if (Time.time > 3.0f && !GameStarted)
        {
            signalBus.Fire<StartGame>();
            GameStarted = true;
        }
    }

    private void StartNewLevel()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
