using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : IDisposable
{
    public List<Transform> Enemies => enemies;
    private List<Transform> enemies = new List<Transform>();
    private readonly LevelBounds levelBounds;
    private readonly SignalBus signalBus;
    private readonly Levels levels;
    private readonly DiContainer container;

    public EnemySpawner(
        LevelBounds levelBounds, 
        SignalBus signalBus,
        Levels levelsSettings,
        DiContainer container)
    {
        this.levelBounds = levelBounds;
        this.signalBus = signalBus;
        this.levels = levelsSettings;
        this.container = container;
        SpawnEnemies();
        signalBus.Subscribe<EnemyDied>(RemoveEnemy);
    }

    public void SpawnEnemies()
    {
        var levelInfo = levels.CurrentLevel;
        for (int i = 0; i < levelInfo.EnemyPrefabs.Count; i++)
        {
            var prefabs = levelInfo.EnemyPrefabs;
            
            var prefab = prefabs[i].EnemyPrefab;
            for (int k = 0; k < prefabs[i].Quantity; k++)
            {
                var enemy = container.InstantiatePrefab(prefab).GetComponent<EnemyFacade>();
                enemies.Add(enemy.Transform);
                enemy.Transform.position = GetRandomSpawnPosition();
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        var r = UnityEngine.Random.value;
        var xPos = Mathf.Lerp(levelBounds.Min.x, levelBounds.Max.x, r);
        var zDistance = levelBounds.Max.z - levelBounds.Min.z;
        r = UnityEngine.Random.value;
        var zPos = (zDistance / 3.0f) + Mathf.Lerp(levelBounds.Min.z, levelBounds.Max.z - (zDistance / 3.0f), r);
        return  new Vector3(xPos, 1, zPos);
    }

    private void RemoveEnemy(EnemyDied enemyInfo)
    {
        enemies.Remove(enemyInfo.enemy.Transform);
        if (enemies.Count == 0)
        {
            signalBus.Fire<AllEnemiesDied>();
        }
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<EnemyDied>(RemoveEnemy);
    }
}
