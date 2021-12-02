using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : MonoBehaviour
{
    // Inspector

    public GameObject spacePlane;


    // Unity
    void Update()
    {
        spacePlane.transform.position = new Vector3(transform.position.x, -10, transform.position.z);
    }
}
