using System;
using UnityEngine;
using Zenject;

public enum BulletType
{ 
    Enemy,
    Player
}

public class Bullet : MonoBehaviour, IPoolable<float, float, IMemoryPool>, IDisposable
{
    [SerializeField] private LayerMask layerMask;
    private float speed;
    private float damage;
    private IMemoryPool pool;

    public void OnSpawned(float speed, float damage, IMemoryPool pool)
    {
        this.speed = speed;
        this.damage = damage;
        this.pool = pool;
    }

    public void OnDespawned()
    {
        pool = null;
    }

    public void Update()
    {

        Fly();
    }

    private void Fly()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHittable hittable))
        {
            hittable.TakeDamage(damage);
        }
        pool.Despawn(this);
    }

    public void Dispose()
    {
        pool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<float, float, Bullet>
    {

    }
}