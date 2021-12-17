using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{
    // Inspector

    float maximumShield = 100f;


    // Properties

    public event Action<float> OnShieldChanged = delegate {};

    public float CurrentShields { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }


    // Public

    public void ChangeShields(float amount)
    {
        CurrentShields += amount;

        if (CurrentShields > maximumShield) {
            CurrentShields = maximumShield;
        } else if (CurrentShields <= 0f) {
            CurrentShields = 0f;
        }

        float shieldPercentage = CurrentShields / maximumShield;
        OnShieldChanged(shieldPercentage);
    }

    public bool Empty()
    {
        return CurrentShields <= 0;
    }


    // Private

    private void SetComponents()
    {
        CurrentShields = maximumShield;
    }
}
