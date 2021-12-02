using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Inspector

    public GameObject bodyPrefab;

    // Properties

    private Body Body { get; set; }


    // Unity

    private void Start() {
        StartCoroutine(Spawn());
    }


    // Private

    private IEnumerator Spawn()
    {
        WaitForSeconds waitFor = new WaitForSeconds(40f);

        while (true) {
            int random = Random.Range(0, 10);

            if (Body == null && random > 3) {
                GameObject prefab = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
                prefab.transform.SetParent(transform);
                prefab.layer = 6;
                Body = prefab.GetComponent<Body>();
            }

            yield return waitFor;
        }
    }
}
