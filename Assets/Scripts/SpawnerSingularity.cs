using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSingularity : MonoBehaviour
{
    // Inspector

    public GameObject singularityPrefab;
    public GameObject prizePrefab;


    // Properties

    private Singularity Singularity { get; set; }


    // Unity

    private void Start() {
        StartCoroutine(Spawn());
    }


    // Private

    private void InstantiateSpawn(Vector3 position)
    {
        int coinflip = Random.Range(0, 10);
        GameObject type;

        type = (coinflip <= 8) ? singularityPrefab : prizePrefab;

        GameObject prefab = Instantiate(type, position, Quaternion.identity);
        prefab.transform.SetParent(Space.Instance.spawnPlane);
        prefab.layer = 6;
        Singularity = prefab.GetComponent<Singularity>();
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitFor = new WaitForSeconds(Space.Instance.SpawnTime);

        while (true) {
            if (Singularity == null) {
                int coinflip = Random.Range(0, 10);
                if (coinflip <= 9) InstantiateSpawn(transform.position);
            }

            yield return waitFor;
        }
    }
}
