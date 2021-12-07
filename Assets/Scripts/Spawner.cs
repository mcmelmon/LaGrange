using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Inspector

    public GameObject singularityPrefab;
    public GameObject prizePrefab;


    // Unity

    private void Start() {
        StartCoroutine(Spawn());
    }


    // Private

    private void InstantiateSpawn(Vector3 position)
    {
        int coinflip = Random.Range(0, 10);
        GameObject type;

        type = (coinflip <= 7) ? singularityPrefab : prizePrefab;

        GameObject prefab = Instantiate(type, position, Quaternion.identity);
        prefab.transform.SetParent(Space.Instance.spawnPlane);
        prefab.layer = 6;
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitFor = new WaitForSeconds(10f);

        while (true) {
            int coinflip = Random.Range(0, 10);
            if (coinflip <= 9) InstantiateSpawn(transform.position);

            yield return waitFor;
        }
    }
}
