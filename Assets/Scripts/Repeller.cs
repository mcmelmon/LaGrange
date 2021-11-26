using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeller : MonoBehaviour
{
    // Properties

    public Attractor Attractor { get; set; }
    public Rigidbody Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void FixedUpdate() {
        foreach (var attractor in Space.Instance.GetOtherAttractors(Attractor)) {
            Repel(attractor);
        }
    }


    // Private

    private void Repel(Attractor other)
    {
        Vector3 direction = other.transform.position - transform.position;
        float distance = direction.magnitude;
        float expansion = Space.Instance.C / Mathf.Pow(distance, 3);
        Vector3 force = direction.normalized * expansion;

        other.Body.AddForce(force);
    }

    private void SetComponents()
    {
        Attractor = GetComponent<Attractor>();
        Body = GetComponent<Rigidbody>();
    }
}
