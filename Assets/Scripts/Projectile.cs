using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Inspector

    public float speed = 3f;
    public float damage = 1f;

    // Properties

    public Body Body { get; set; }

    private void Awake()
    {
        SetComponents();
    }

    private void OnCollisionEnter(Collision other) {
        Singularity singularity = other.transform.GetComponent<Singularity>();

        if (singularity != null) {
            Body.RemoveFromSpace();
        }
    }

    private void Start() {
        StartCoroutine(Move());
    }


    // Private

    private IEnumerator Move()
    {
        Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;

        while (true) {
            if (Vector3.Distance(transform.position, Player.Instance.transform.position) > 10f) {
                direction = (Player.Instance.transform.position - transform.position).normalized;
                Body.AddForce(direction * speed);
            }

            yield return null;
        }
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }

}
