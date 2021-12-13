using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Inspector

    public float shipForceMultiplier = 3f;

    // Properties

    public Attractor Attractor { get; set; }
    public Repeller Repeller { get; set; }
    public Singularity Singularity { get; set; }

    private Rigidbody Physics { get; set; }


    private void Awake() {
        SetComponents();
    }

    private void Start() {
        Space.Instance.Bodies.Add(this);
    }

    private void Update() {
        float distanceFromPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (distanceFromPlayer > 150) RemoveFromSpace();
    }


    // Public

    public void AddForce(Vector3 force, ForceMode mode = ForceMode.Impulse)
    {
        if (IsShip()) {
            force *= shipForceMultiplier;
        }

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

    public bool IsShip()
    {
        return GetComponent<Player>();
    }

    public float GetMass()
    {
        return Physics.mass;
    }

    public bool Merging()
    {
        return (Singularity != null) ? Singularity.Merging : false;
    }

    public void RemoveFromSpace()
    {
        LineRenderer[] lines = GetComponentsInChildren<LineRenderer>();

        if (Attractor != null) {
            foreach (KeyValuePair<Body, LineRenderer> pair in Attractor.Lines) {
                Destroy(pair.Value.transform.gameObject);

                if (pair.Key.Attractor.Lines.ContainsKey(this)) {
                    Destroy(pair.Key.Attractor.Lines[this]);
                    pair.Key.Attractor.Lines.Remove(this);
                }
            }
        }

        Space.Instance.Bodies.Remove(this);
        Destroy(this.transform.gameObject);
    }

    public void SetMass(float mass)
    {
        Physics.mass = mass;
    }

    public void StartWithRandomForce()
    {
        Vector3 direction = new Vector3(Random.Range(-359, 359),Random.Range(-359, 359),Random.Range(-359, 359)).normalized;
        float oomph =  Random.Range(7, 11);
        Vector3 force = direction * oomph;

        AddForce(force, ForceMode.VelocityChange);
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
