using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Space : MonoBehaviour
{
    // Inspector

    public GameObject bodyPrefab;
    public float G = 5f;
    public float C = 1.5f;
    public float separation = 10f;


    // Properties

    public static Space Instance { get; set; }

    public List<Body> Bodies { get; set; }


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

    private void Start() {
        InstantiateBodies();
    }


    // Public

    public List<Body> GetBodies(Body body)
    {
        return Bodies.Where(other => other != body).ToList();
    }


    // Private

    private void InstantiateBody(Vector3 position)
    {
        GameObject prefab = Instantiate(bodyPrefab, position, Quaternion.identity);
        prefab.transform.SetParent(transform);
    }

    private void InstantiateBodies()
    {
        for (int x = 2; x < 5; x++) {
            for (int z = 2; z < 5; z++) {
                InstantiateBody(new Vector3(x * separation, 0, z * separation ));
                InstantiateBody(new Vector3(x * separation, 0, -z * separation ));
                InstantiateBody(new Vector3(-x * separation, 0, z * separation ));
                InstantiateBody(new Vector3(-x * separation, 0, -z * separation ));
            }
        }
    }

    private void SetComponents()
    {
        Bodies = new List<Body>();
    }
}
