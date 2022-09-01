using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    private ParticleSystem selfParticalSystem;
    private ParticleSystem[] particleSystems;

    private void Awake()
    {
        selfParticalSystem = GetComponent<ParticleSystem>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    public void SetPotionColor(Color newColor)
    {
        selfParticalSystem.startColor = newColor;
        foreach(var particleSystem in particleSystems)
        {
            particleSystem.startColor = newColor;
        }
    }
}
