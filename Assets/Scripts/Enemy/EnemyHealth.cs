using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour, IHittable
{
    private Player player;
    private Settings settings;
    private SignalBus signalBus;
    private EnemyFacade facade;

    private float health;

    [Inject]
    public void Construct(
        Player player, 
        Settings settings, 
        SignalBus signalBus,
        EnemyFacade facade)
    {
        this.player = player;
        this.signalBus = signalBus;
        this.facade = facade;
        health = settings.Health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            signalBus.Fire(new EnemyDied() { enemy = facade});
            gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class Settings
    {
        public float Health;
        public float FireRate;
        public float Damage;
        public float ShootSpeed;
    }
}
