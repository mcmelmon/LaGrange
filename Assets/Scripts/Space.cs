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
        StartCoroutine(SmoothCameraFollowPlayer());
    }

    private void Update() {
        movementPlane.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, movementPlane.position.z);
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
        prefab.transform.SetParent(spawnPlane);
        Spawners.Add(prefab.GetComponent<Spawner>());
    }

    private void InstantiateSpawners()
    {
        for (int x = 1; x < 4; x++) {
            for (int y = 1; y < 4; y++) {
                InstantiateSpawner(new Vector3(x * separation, y * separation, 0 ));
                InstantiateSpawner(new Vector3(x * separation, -y * separation, 0 ));
                InstantiateSpawner(new Vector3(-x * separation, y * separation, 0 ));
                InstantiateSpawner(new Vector3(-x * separation, -y * separation, 0 ));
            }
        }
    }

    private void SetComponents()
    {
        Bodies = new List<Body>();
        Spawners = new List<Spawner>();
    }

    private IEnumerator SmoothCameraFollowPlayer()
    {
        float smoothSpeed = 1.5f;
        Vector3 smoothedPosition = new Vector3();
        Vector3 target = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, eyeOfGod.transform.position.z);
        float distance = Mathf.Abs(Vector3.Distance(target, eyeOfGod.transform.position));

        while (true) {
            if (distance > 0.1f) {
                smoothedPosition = Vector3.Lerp(eyeOfGod.transform.position, target, smoothSpeed * Time.deltaTime);
                eyeOfGod.transform.position = smoothedPosition;
            }

            target = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, eyeOfGod.transform.position.z);
            distance = Mathf.Abs(Vector3.Distance(target, eyeOfGod.transform.position));

            yield return null;
        }
    }
}
