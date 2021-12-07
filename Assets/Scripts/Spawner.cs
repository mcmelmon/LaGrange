using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Inspector

    public GameObject singularityPrefab;

    // Properties

    private Body Body { get; set; }


    // Unity

    private void Start() {
        StartCoroutine(Spawn());
    }


    // Private

    private Body InstantiateSpawn(Vector3 position)
    {
        GameObject prefab = Instantiate(singularityPrefab, position, Quaternion.identity);
        prefab.transform.SetParent(Space.Instance.spawnPlane);
        prefab.layer = 6;
        return prefab.GetComponent<Body>();
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitFor = new WaitForSeconds(15f);

        while (true) {
            InstantiateSpawn(transform.position);

            yield return waitFor;
        }
    }
}
