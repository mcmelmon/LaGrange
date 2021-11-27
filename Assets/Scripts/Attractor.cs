using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Inspector

    public bool centerOfUniverse = false;

    // Properties

    public Rigidbody Body { get; set; }
    public bool Merging { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
        StartCoroutine(WaitForSpace());
    }

    private void FixedUpdate() {
        foreach (var attractor in Space.Instance.GetBodies(this)) {
            if (!Merging) Attract(attractor);
        }
    }

    private void OnCollisionEnter(Collision other) {
        Attractor partner = other.transform.GetComponent<Attractor>();
        if (!Merging) Merge(partner);
    }

    // Public

    public void RemoveFromSpace()
    {
        if (!centerOfUniverse) {
            Space.Instance.Bodies.Remove(this);
            Destroy(this.transform.gameObject);
        }
    }


    // Private

    private void Attract(Attractor other)
    {
        Vector3 direction = transform.position - other.transform.position;
        float distance = direction.magnitude;

        float gravitation = Space.Instance.G * (Body.mass * other.Body.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * gravitation;

        if (!float.IsNaN(force.x)) other.Body.AddForce(force);
    }

    private void Merge(Attractor other)
    {
        if (Body.mass > other.Body.mass) {
            other.Merging = true;
            Body.mass += other.Body.mass;
            transform.position = WeightedMidpoint(other);
            other.RemoveFromSpace();
        } else {
            Merging = true;
            other.Body.mass += Body.mass;
            transform.position = WeightedMidpoint(other);
            RemoveFromSpace();
        }

        transform.localScale = new Vector3(1, 1, 1) * (0.5f + Mathf.Log(Body.mass));
    }

    private void SetComponents()
    {
        Body = GetComponent<Rigidbody>();
        Merging = false;

        if (!centerOfUniverse) Body.mass = Random.Range(1, 20);
        transform.localScale = new Vector3(1, 1, 1) * (0.5f + Mathf.Log(Body.mass));
    }

    private IEnumerator WaitForSpace()
    {
        while (Space.Instance == null) {
            yield return null;
        }

        Space.Instance.Bodies.Add(this);
    }

    private Vector3 WeightedMidpoint(Attractor other)
    {
        Vector3 direction = (transform.position - other.transform.position).normalized;
        float combinedMass = Body.mass + other.Body.mass;

        return transform.position += direction * (combinedMass - Body.mass) / combinedMass;
    }
}
