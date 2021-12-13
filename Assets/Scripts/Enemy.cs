using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Inspector

    public Projectile projectilePrefab;


    // Properties

    public Body Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void Start() {
        StartCoroutine(Attack());
    }

    // Public


    // Private

    private IEnumerator Attack()
    {
        // spawn projectile

        WaitForSeconds waitFor = new WaitForSeconds(3f);

        while (true) {
            yield return waitFor;
        }
    }

    private IEnumerator Move()
    {
        // track Player

        while (true) {
            yield return null;
        }
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }

}
