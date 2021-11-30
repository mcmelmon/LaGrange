using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeller : MonoBehaviour
{
    // Properties

    public Body Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void FixedUpdate() {
        foreach (var body in Space.Instance.GetBodies(Body)) {
            if (!Body.Merging) Repel(body);
        }
    }


    // Private

    private void Repel(Body other)
    {
        Vector3 direction = other.transform.position - transform.position;
        float distance = direction.magnitude;
        float expansion =  Mathf.Pow(Body.GetMass(), Space.Instance.C) / (Space.Instance.G * Mathf.Pow(distance, 4));
        Vector3 force = direction.normalized * expansion;

        if (!float.IsNaN(force.x)) other.AddForce(force);
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }
}
