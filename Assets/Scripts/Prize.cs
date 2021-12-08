using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    // Properties

    public Body Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void OnCollisionEnter(Collision other) {
        Singularity singularity = other.transform.GetComponent<Singularity>();
        Player player = other.transform.GetComponent<Player>();

        if (singularity != null) {
            Body.RemoveFromSpace();
        } else if (player != null) {
            Body.RemoveFromSpace();
        }
    }

    private void Start() {
        Body.StartWithRandomForce();
    }


    // Private

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }
}
