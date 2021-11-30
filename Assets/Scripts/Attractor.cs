using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Properties

    public Body Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void FixedUpdate() {
        foreach (var body in Space.Instance.GetBodies(Body)) {
            if (!Body.Merging) Attract(body);
        }
    }


    // Private

    private void Attract(Body other)
    {
        Vector3 direction = transform.position - other.transform.position;
        float distance = direction.magnitude;

        float gravitation = Space.Instance.G * (Body.GetMass() * other.GetMass()) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * gravitation;

        if (!float.IsNaN(force.x)) other.AddForce(force);
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }
}
