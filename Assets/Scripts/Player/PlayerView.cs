using UnityEngine;
using Zenject;

public interface IHittable 
{
    public void TakeDamage(float damage);
}

public class PlayerView : MonoBehaviour, IHittable
{
    [SerializeField] private LayerMask layerMask;
    [Inject] private readonly Player player;

    public void TakeDamage(float damage)
    {
        player.TakeDamage(damage);
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
