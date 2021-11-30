using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Inspector

    public bool centerOfUniverse = false;



    // Properties

    public Attractor Attractor { get; set; }
    public bool Merging { get; set; }
    public Repeller Repeller { get; set; }

    private Rigidbody Physics { get; set; }


    private void Awake() {
        SetComponents();
        StartCoroutine(WaitForSpace());
    }

    private void OnCollisionEnter(Collision other) {
        Body partner = other.transform.GetComponent<Body>();
        if (!Merging) Merge(partner);
    }


    // Public

    public void AddForce(Vector3 force)
    {
        Physics.AddForce(force);
    }

    public float GetMass()
    {
        return Physics.mass;
    }

    public void IncreaseMass(float increase)
    {
        Physics.mass += increase;
    }

    public void RemoveFromSpace()
    {
        if (!centerOfUniverse) {
            Space.Instance.Bodies.Remove(this);
            Destroy(this.transform.gameObject);
        }
    }

    public void SetMass(float mass)
    {
        Physics.mass = mass;
    }


    // Private

    private void Merge(Body other)
    {
        if (GetMass() > other.GetMass()) {
            other.Merging = true;
            IncreaseMass(other.GetMass());
            transform.position = WeightedMidpoint(other);
            other.RemoveFromSpace();
        } else {
            Merging = true;
            other.IncreaseMass(GetMass());
            transform.position = WeightedMidpoint(other);
            RemoveFromSpace();
        }

        transform.localScale = new Vector3(1, 1, 1) * (0.5f + Mathf.Log(GetMass()));
    }

    private void SetComponents()
    {
        Attractor = GetComponent<Attractor>();
        Merging = false;
        Physics = GetComponent<Rigidbody>();
        Repeller = GetComponent<Repeller>();

        if (!centerOfUniverse) Physics.mass = Random.Range(1, 10);
        transform.localScale = new Vector3(1, 1, 1) * (0.5f + Mathf.Log(GetMass()));
    }

    private IEnumerator WaitForSpace()
    {
        while (Space.Instance == null) {
            yield return null;
        }

        Space.Instance.Bodies.Add(this);
    }

    private Vector3 WeightedMidpoint(Body other)
    {
        Vector3 direction = (transform.position - other.transform.position).normalized;
        float combinedMass = GetMass() + other.GetMass();

        return transform.position += direction * (combinedMass - GetMass()) / combinedMass;
    }
}
