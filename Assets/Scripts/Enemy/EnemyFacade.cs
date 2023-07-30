using System;
using Zenject;
using UnityEngine;

public class EnemyFacade : MonoBehaviour, IHittable
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
    private EnemyHealth health;

    [Inject]
    public void Construct(Settings settings, EnemyHealth health)
    {
        this.settings = settings;
        this.health = health;
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    [Serializable]
    public class Settings
    {
        public int MoneyValue;
    }
}
