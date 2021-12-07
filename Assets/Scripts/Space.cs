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
    public GameObject linePrefab;
    public Transform movementPlane;
    public Transform spawnPlane;


    // Properties

    public static Space Instance { get; set; }

    public List<Spawner> Spawners { get; set; }

    public List<Body> Bodies { get; set; }

    private IEnumerator MoveCameraRoutine { get; set; }


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

    private void Update() {
        spawnPlane.transform.Rotate(0, -0.25f * Time.deltaTime, 0);
    }


    // Public

    public List<Body> GetBodies(Body body)
    {
        return Bodies.Where(other => other != body).ToList();
    }

    public void ResetCamera()
    {
        eyeOfGod.transform.position = Player.Instance.transform.position + new Vector3(0, 0, eyeOfGod.transform.position.z);
    }


    // Private

    private void InstantiateSpawner(Vector3 position)
    {
        GameObject prefab = Instantiate(spawnerPrefab, position, Quaternion.identity);
        Spawners.Add(prefab.GetComponent<Spawner>());
    }

    private void InstantiateSpawners()
    {
        for (int x = 0; x < 2; x++) {
            for (int y = 0; y < 2; y++) {
                InstantiateSpawner(new Vector3(x * separation + 15, y * separation + 55, 0 ));
                InstantiateSpawner(new Vector3(-1 * (x * separation + 15), y * separation + 55, 0 ));
            }
        }
    }

    private void SetComponents()
    {
        Bodies = new List<Body>();
        Spawners = new List<Spawner>();
    }
}
