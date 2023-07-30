using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ParticleSpawner : MonoBehaviour 
{
    [SerializeField]
    private ParticleSystem ShotParticleSystem;

    public void PlayShot(Vector3 pos, Vector3 dir)
    {
        ShotParticleSystem.transform.position = pos;
        ShotParticleSystem.transform.forward = dir;
        ShotParticleSystem.Play();
    }
}
