using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Properties

    public Attractor Attractor { get; set; }
    public Repeller Repeller { get; set; }
    public Singularity Singularity { get; set; }

    private Rigidbody Physics { get; set; }


    private void Awake() {
        SetComponents();
    }


    // Public

    public void AddForce(Vector3 force, ForceMode mode = ForceMode.Impulse)
    {
        Physics.AddForce(force, mode);
    }

    public void DecreseMass(float reduction)
    {
        SetMass(GetMass() - reduction);
    }

    public void IncreaseMass(float increase)
    {
        SetMass(GetMass() + increase);
    }

    public float GetMass()
    {
        return Physics.mass;
    }

    public bool Merging()
    {
        return (Singularity != null) ? Singularity.Merging : false;
    }

    public void SetMass(float mass)
    {
        Physics.mass = mass;
    }

    public Vector3 WeightedMidpoint(Body other)
    {
        Vector3 direction = (transform.position - other.transform.position).normalized;
        float combinedMass = GetMass() + other.GetMass();

        return transform.position += direction * (combinedMass - GetMass()) / combinedMass;
    }


    // Private

    private void SetComponents()
    {
        Attractor = GetComponent<Attractor>();
        Physics = GetComponent<Rigidbody>();
        Repeller = GetComponent<Repeller>();
        Singularity = GetComponent<Singularity>();
    }
}
