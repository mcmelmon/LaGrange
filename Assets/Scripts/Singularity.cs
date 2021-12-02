using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : MonoBehaviour
{
    // Inspector

    public GameObject spacePlane;


    // Properties

    public Body Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void Start() {
        StartCoroutine(Evaporate());
    }

    void Update()
    {
        spacePlane.transform.position = new Vector3(transform.position.x, -10, transform.position.z);
    }


    // Public

    public void ReduceMass(float reduction)
    {
        Body.SetMass(Body.GetMass() - reduction);
    }


    // Private

    private IEnumerator Evaporate()
    {
        WaitForSeconds waitFor = new WaitForSeconds(4f);

        while (true) {
            yield return waitFor;

            int index = Random.Range(0, Space.Instance.Spawners.Count);
            if (Body.GetMass() > 500) Space.Instance.Spawners[index].EvaporateSingularity();
        }
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }
}
