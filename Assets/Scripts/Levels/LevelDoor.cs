using UnityEngine;
using Zenject;

public class EnterDoor { }

public class LevelDoor : MonoBehaviour, IInteractable
{
    private bool opened;
    private Levels levels;
    private SignalBus signalBus;

    [Inject]
    public void Construct(
        Levels levels,
        SignalBus signal)
    {
        this.levels = levels;
        this.signalBus = signal;
        InitializePosition();
        signalBus.Subscribe<AllEnemiesDied>(OpenDoor);
    }

    private void OpenDoor()
    {
        opened = true;
    }

    private void OnDestroy()
    {
        signalBus.Unsubscribe<AllEnemiesDied>(OpenDoor);
    }

    public void Interact(IInteractor interacter)
    {
        if (opened)
        {
            signalBus.Fire<EnterDoor>();
            levels.NextLevel();
        }
    }

    private void InitializePosition()
    {
        transform.position = levels.CurrentLevel.DoorPosition;
    }
    
}
