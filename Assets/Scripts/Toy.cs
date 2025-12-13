using UnityEngine;
using System;

public class Toy : MonoBehaviour
{
    [Header("Toy Settings")]
    public string targetPetTag;        // "Dragon", "Cat", "Dog"
    public int happinessAmount = 30;  
    public float cooldownTime = 5f;

    private bool canUse = true;

    // Reference to ToyManager so toy can despawn itself
    private ToyManager toyManager;

    public void SetManager(ToyManager manager)
    {
        toyManager = manager;
    }

}