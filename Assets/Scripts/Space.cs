using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Space : MonoBehaviour
{
    // Inspector

    public float G = 100f;


    // Properties

    public static Space Instance { get; set; }

    public List<Attractor> Attractors { get; set; }


    // Unity

    private void Awake() {
        if (Instance != null) {
            Debug.Log("More than one Space instance");
            Destroy(this);
            return;
        }

        Instance = this;
        SetComponents();
    }


    // Public

    public List<Attractor> GetOtherAttractors(Attractor attractor)
    {
        return Attractors.Where(other => other != attractor).ToList();
    }


    // Private

    private void SetComponents()
    {
        Attractors = new List<Attractor>();
    }
}
