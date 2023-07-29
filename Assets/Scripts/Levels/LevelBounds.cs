using UnityEngine;
using System;

public class LevelBounds
{
    private readonly Settings settings;

    public Vector3 Min => settings.Min;
    public Vector3 Max => settings.Max;

    public LevelBounds(Settings settings)
    {
        this.settings = settings;
    }

    public Vector3 KeepInBound(Vector3 position)
    {
        Vector3 newPos = position;

        if (position.x < settings.Min.x)
        {
            newPos.x = settings.Min.x;
        }

        if (position.z < settings.Min.z)
        {
            newPos.z = settings.Min.z;
        }

        if (position.x > settings.Max.x)
        {
            newPos.x = settings.Max.x;
        }

        if (position.z > settings.Max.z)
        {
            newPos.z = settings.Max.z;
        }

        return newPos;
    }

    [Serializable]
    public class Settings
    {
        public Vector3 Min;
        public Vector3 Max;
    }
}
