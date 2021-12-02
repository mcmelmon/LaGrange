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


    // Public

    public void EvaporateSingularity()
    {
        Vector3 position = (transform.position + Space.Instance.singularity.transform.position) / 5f;
        Body = InstantiateSpawn(position);

        Vector3 direction = (position - Space.Instance.singularity.transform.position).normalized;
        float oomph =  Random.Range(800, 2000) * Body.GetMass();
        Vector3 force = direction * oomph;

        Body.AddForce(force);
        Space.Instance.singularity.ReduceMass(Body.GetMass());
    }


    // Private

    private Body InstantiateSpawn(Vector3 position)
    {
        GameObject prefab = Instantiate(bodyPrefab, position, Quaternion.identity);
        prefab.transform.SetParent(transform);
        prefab.layer = 6;
        return prefab.GetComponent<Body>();
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitFor = new WaitForSeconds(45f);

        while (true) {
            int random = Random.Range(0, 10);
            if (Body == null && random > 7) Body = InstantiateSpawn(transform.position);

            yield return waitFor;
        }
    }
}
