using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Space : MonoBehaviour
{
    // Inspector

    public GameObject singularitySpawnerPrefab;
    public GameObject enemySpawnerPrefab;
    public float G = 5f;
    public float E = 1.5f;
    public float separation = 10f;
    public CinemachineVirtualCamera eyeOfGod;
    public GameObject linePrefab;
    public Transform movementPlane;
    public Transform spawnPlane;
    public float rotationSpeed = -1f;


    // Properties

    public static Space Instance { get; set; }

    public List<Body> Bodies { get; set; }
    public List<SpawnerEnemy> EnemySpawners { get; set; }
    public List<SpawnerSingularity> SingularitySpawners { get; set; }
    public float SpawnTime { get; set; }

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
        InstantiateEnemySpawners();
        InstantiateSingularitySpawners();
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

    private void InstantiateEnemySpawner(Vector3 position)
    {
        GameObject prefab = Instantiate(enemySpawnerPrefab, position, Quaternion.identity);
        EnemySpawners.Add(prefab.GetComponent<SpawnerEnemy>());
    }

    private void InstantiateEnemySpawners()
    {
        for (int x = 0; x < 1; x++) {
            for (int y = 0; y < 2; y++) {
                InstantiateEnemySpawner(new Vector3((x * separation) + 90, (y * separation + 5), 0 ));
                InstantiateEnemySpawner(new Vector3(((x * separation) + 90), -1 * (y * separation + 5), 0 ));
                InstantiateEnemySpawner(new Vector3(-1 * (x * separation + 90), (y * separation + 5), 0 ));
                InstantiateEnemySpawner(new Vector3(-1 * ((x * separation) + 90), -1 * (y * separation + 5), 0 ));
            }
        }
    }


    private void InstantiateSingularitySpawner(Vector3 position)
    {
        GameObject prefab = Instantiate(singularitySpawnerPrefab, position, Quaternion.identity);
        SingularitySpawners.Add(prefab.GetComponent<SpawnerSingularity>());
    }

    private void InstantiateSingularitySpawners()
    {
        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < 1; y++) {
                InstantiateSingularitySpawner(new Vector3((x * separation) + 5, y * separation + 90, 0 ));
                InstantiateSingularitySpawner(new Vector3(-1 * ((x * separation) + 5), y * separation + 90, 0 ));
                InstantiateSingularitySpawner(new Vector3((x * separation) + 5, -1 * (y * separation + 90), 0 ));
                InstantiateSingularitySpawner(new Vector3(-1 * ((x * separation) + 5), -1 * (y * separation + 90), 0 ));
            }
        }
    }

    private void SetComponents()
    {
        Bodies = new List<Body>();
        EnemySpawners = new List<SpawnerEnemy>();
        SingularitySpawners = new List<SpawnerSingularity>();
        SpawnTime = 4f;
    }
}
