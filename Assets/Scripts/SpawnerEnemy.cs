using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    // Inspector

    public GameObject enemyPrefab;


    // Unity

    private void Start() {
        StartCoroutine(Spawn());
    }


    // Private

    private void InstantiateSpawn(Vector3 position)
    {
        int coinflip = Random.Range(0, 10);

        GameObject prefab = Instantiate(enemyPrefab, position, Quaternion.identity);
        prefab.layer = 6;
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitFor = new WaitForSeconds(Space.Instance.SpawnTime * 5f);

        while (true) {
            int coinflip = Random.Range(0, 10);
            if (coinflip <= 2) InstantiateSpawn(transform.position);

            yield return waitFor;
        }
    }
}
