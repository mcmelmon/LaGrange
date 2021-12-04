using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Properties

    public static Player Instance { get; set; }

    public Body Body { get; set; }


    // Unity

    private void Awake() {
        if (Instance != null) {
            Debug.Log("More than one Player instance");
            Destroy(this);
            return;
        }

        Instance = this;
        SetComponents();
    }


    // Private

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }
}
