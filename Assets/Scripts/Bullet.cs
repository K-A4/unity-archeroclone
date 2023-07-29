using System;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPoolable<float, float, IMemoryPool>, IDisposable
{
    private float speed;
    private float damage;
    private IMemoryPool pool;
    private Settings settings;
    private float startTime;

    [Inject]
    public void Construct(Settings settings)
    {
        this.settings = settings;
    }

    public void OnSpawned(float speed, float damage, IMemoryPool pool)
    {
        this.speed = speed;
        this.damage = damage;
        this.pool = pool;
        startTime = Time.time;
    }

    public void OnDespawned()
    {
        pool = null;
    }

    public void Update()
    {
        Fly();
        DespawnIfLifeTimeGone();
    }

    private void Fly()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void DespawnIfLifeTimeGone()
    {
        if (Time.time - startTime > settings.LifeTime)
        {
            pool.Despawn(this);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IHittable hittable))
        {
            hittable.TakeDamage(damage);
            pool.Despawn(this);
        }
    }

    public void Dispose()
    {
        pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<float, float, Bullet>
    {
    }

    [Serializable]
    public class Settings
    {
        public float LifeTime;
    }
}