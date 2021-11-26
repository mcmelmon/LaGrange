using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Space : MonoBehaviour
{
    // Inspector

    public GameObject repellerPrefab;
    public float G = 10f;
    public float C = 5f;


    // Properties

    public static Space Instance { get; set; }

    public List<Attractor> Attractors { get; set; }
    public List<Repeller> Repellers { get; set; }


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
        InstantiateRepellers();
    }

    private void Update() {

    }


    // Public

    public List<Attractor> GetOtherAttractors(Attractor attractor)
    {
        return Attractors.Where(other => other != attractor).ToList();
    }


    // Private

    private void InstantiateRepeller(Vector3 position)
    {
        GameObject prefab = Instantiate(repellerPrefab, position, Quaternion.identity);
        prefab.transform.SetParent(transform);
    }

    private void InstantiateRepellers()
    {
        for (int x = 1; x < 3; x++) {
            for (int z = 1; z < 3; z++) {
                InstantiateRepeller(new Vector3(x * 20, 0, z * 20 ));
                InstantiateRepeller(new Vector3(x * 20, 0, -z * 20 ));
                InstantiateRepeller(new Vector3(-x * 20, 0, z * 20 ));
                InstantiateRepeller(new Vector3(-x * 20, 0, -z * 20 ));
            }
        }
    }

    private void SetComponents()
    {
        Attractors = new List<Attractor>();
        Repellers = new List<Repeller>();
    }
}
