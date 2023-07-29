using System;
using Zenject;
using UnityEngine;

public class EnemyFacade : MonoBehaviour
{
    public int MoneyValue
    {
        get => settings.MoneyValue;
    }

    public Transform Transform
    {
        get => transform;
    }

    private Settings settings;

    [Inject]
    public void Construct(Settings settings)
    {
        this.settings = settings;
    }

    [Serializable]
    public class Settings
    {
        public int MoneyValue;
    }
}
