using UnityEngine;
using Zenject;

public class EnemyHitter : MonoBehaviour
{
    private EnemyHealth.Settings settings;

    [Inject]
    public void Construct(EnemyHealth.Settings settings)
    {
        this.settings = settings;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IHittable hittable))
        {
            hittable.TakeDamage(settings.Damage);
        }
    }
}
