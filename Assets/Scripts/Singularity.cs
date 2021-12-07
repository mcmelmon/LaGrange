using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : MonoBehaviour
{
    // Properties

    public Body Body { get; set; }
    public bool Merging { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void OnCollisionEnter(Collision other) {
        Singularity singularity = other.transform.GetComponent<Singularity>();

        if (singularity != null) {
            if (!Merging) Merge(singularity);
        }
    }

    private void Start() {
        ScaleHorizon();
        Body.StartWithRandomForce();
    }


    // Private

    private void Merge(Singularity other)
    {
        if (Body.GetMass() > other.Body.GetMass()) {
            other.Merging = true;
            Body.IncreaseMass(other.Body.GetMass());
            transform.position = Body.WeightedMidpoint(other.Body);
            other.Body.RemoveFromSpace();
        } else {
            Merging = true;
            other.Body.IncreaseMass(Body.GetMass());
            transform.position = Body.WeightedMidpoint(other.Body);
            Body.RemoveFromSpace();
        }

        ScaleHorizon();
    }

    private void ScaleHorizon()
    {
        transform.localScale = new Vector3(1, 1, 1) * (0.5f + (Mathf.Log(6f * Body.GetMass() / Space.Instance.G)));
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
        Merging = false;

        Body.SetMass(Random.Range(1, 10));
    }
}
