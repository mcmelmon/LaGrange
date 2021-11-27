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
        if (other.centerOfUniverse) {
            Merging = true;
            other.Body.mass += Body.mass;
            RemoveFromSpace();
        } else {
            other.Merging = true;
            Body.mass += other.Body.mass;
            other.RemoveFromSpace();
        }
    }

    private void SetComponents()
    {
        Body = GetComponent<Rigidbody>();
        Merging = false;

        if (!centerOfUniverse) Body.mass = Random.Range(1, 10);
    }

    private IEnumerator WaitForSpace()
    {
        while (Space.Instance == null) {
            yield return null;
        }

        Space.Instance.Bodies.Add(this);
    }
}
