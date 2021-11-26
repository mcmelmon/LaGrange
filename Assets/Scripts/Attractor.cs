using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Properties

    public Rigidbody Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();

        StartCoroutine(WaitForSpace());
    }

    private void FixedUpdate() {
        foreach (var attractor in Space.Instance.GetOtherAttractors(this)) {
            Attract(attractor);
        }
    }


    // Private

    private void Attract(Attractor other)
    {
        Vector3 direction = transform.position - other.transform.position;
        float distance = direction.magnitude;
        float gravitation = Space.Instance.G * (Body.mass * other.Body.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * gravitation;

        other.Body.AddForce(force);
    }

    private void SetComponents()
    {
        Body = GetComponent<Rigidbody>();
    }

    private IEnumerator WaitForSpace()
    {
        while (Space.Instance == null) {
            yield return null;
        }

        Space.Instance.Attractors.Add(this);
    }
}
