using System;
using UnityEngine;

public class NearestEnemyFinder
{
    private readonly EnemySpawner enemiesSpawner;
    private readonly Settings settings;

    public NearestEnemyFinder(
        EnemySpawner enemiesSpawner,
        Settings settings)
    {
        this.enemiesSpawner = enemiesSpawner;
        this.settings = settings;
    }

    public Transform FindNearestEnemy(Vector3 position)
    {
        var minDistance = float.MaxValue;
        var minIndex = -1;
        Transform chaseTarget = null;
        var enemies = enemiesSpawner.Enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            var vectorToEnemy = (enemies[i].transform.position - position);

            var distance = vectorToEnemy.sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        if (minIndex != -1)
        {
            chaseTarget = enemies[minIndex].transform;
        }

        return chaseTarget;
    }

    [Serializable]
    public class Settings
    {
        public LayerMask RaycastMask;
    }
}
