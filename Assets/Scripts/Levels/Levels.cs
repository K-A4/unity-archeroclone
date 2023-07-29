using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Levels
{
    public LevelsSettings.Level CurrentLevel => levelsSettings.LevelsList[curentIndex];
    public int CurrentIndex => curentIndex;
    private readonly LevelsSettings levelsSettings;
    private int curentIndex;

    public Levels(LevelsSettings levelsSettings) 
    {
        this.levelsSettings = levelsSettings;
    }

    public void NextLevel()
    {
        curentIndex = ++curentIndex % levelsSettings.LevelsList.Count;
    }

    [Serializable]
    public class LevelsSettings
    {
        public List<Level> LevelsList = new List<Level>();
        

        [Serializable]
        public class Level
        {
            public int EnemyCount => EnemyPrefabs.Sum(x => x.Quantity);
            public List<EnemiesInfoOnLevel> EnemyPrefabs = new List<EnemiesInfoOnLevel>();
            public Vector3 DoorPosition;
            //public LevelBounds
        }

        [Serializable]
        public class EnemiesInfoOnLevel
        {
            public int Quantity;
            public GameObject EnemyPrefab;
        }
    }
}
