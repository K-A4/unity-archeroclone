using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : ITickable
{
    public Vector2 InputVector { get; private set; }

    private readonly VirtualGamepad virtualGamePad;
    private readonly PlayerFireHandler fireHandler;

    public PlayerInput(VirtualGamepad virtualGamePad, PlayerFireHandler fireHandler, SignalBus signalBus)
    {
        this.virtualGamePad = virtualGamePad;
        this.fireHandler = fireHandler;
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
