using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartGame { }

public class GameStarter : ITickable
{
    public bool GameStarted { get; private set; }

    private readonly SignalBus signalBus;
    private readonly Settings settings;
    private float timeToElapsed;

    public GameStarter(SignalBus signalBus, Settings settings)
    {
        this.signalBus = signalBus;
        this.settings = settings;
        signalBus.Subscribe<EnterDoor>(StartNewLevel);
        timeToElapsed = Time.time + settings.TimeToStart;
    }

    public void Tick()
    {
        if (Time.time > timeToElapsed && !GameStarted)
        {
            signalBus.Fire<StartGame>();
            GameStarted = true;
        }
    }

    private void StartNewLevel()
    {
        SceneManager.LoadScene("GamePlay");
    }

    [Serializable]
    public class Settings
    {
        public float TimeToStart;
    }
}
