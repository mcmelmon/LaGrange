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
        CurrentShields = Mathf.Min(maximumShield, CurrentShields + amount);

        float shieldPercentage = CurrentShields / maximumShield;
        OnShieldChanged(shieldPercentage);
    }


    // Private

    private void SetComponents()
    {
        CurrentShields = maximumShield;
    }
}
