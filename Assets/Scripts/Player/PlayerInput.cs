using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : ITickable
{
    public Vector2 InputVector { get; private set; }

    private readonly VirtualGamepad virtualGamePad;

    public PlayerInput(VirtualGamepad virtualGamePad,  SignalBus signalBus)
    {
        this.virtualGamePad = virtualGamePad;
        virtualGamePad.OnUp.AddListener(() => signalBus.Fire<StartFire>());
        virtualGamePad.OnDown.AddListener(() => signalBus.Fire<StopFire>());
    }

    public void Tick()
    {
        InputVector = virtualGamePad.Value;
    }

    public class StartFire { }
    public class StopFire { }
}
