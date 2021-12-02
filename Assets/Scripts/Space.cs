using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Space : MonoBehaviour
{
    // Inspector

    public GameObject spawnerPrefab;
    public float G = 5f;
    public float E = 1.5f;
    public float separation = 10f;
    public CinemachineVirtualCamera eyeOfGod;
    public Singularity singularity;


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
        InstantiateSpawners();
    }


    // Public

    public List<Body> GetBodies(Body body)
    {
        return Bodies.Where(other => other != body).ToList();
    }


    // Private

    private void InstantiateSpawner(Vector3 position)
    {
        GameObject prefab = Instantiate(spawnerPrefab, position, Quaternion.identity);
        prefab.transform.SetParent(singularity.spacePlane.transform);
    }

    private void InstantiateSpawners()
    {
        for (int x = 1; x < 4; x++) {
            for (int z = 1; z < 4; z++) {
                InstantiateSpawner(new Vector3(x * separation, 0, z * separation ));
                InstantiateSpawner(new Vector3(x * separation, 0, -z * separation ));
                InstantiateSpawner(new Vector3(-x * separation, 0, z * separation ));
                InstantiateSpawner(new Vector3(-x * separation, 0, -z * separation ));
            }
        }
    }

    private void SetComponents()
    {
        Bodies = new List<Body>();
    }
}
