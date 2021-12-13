using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Inspector

    public GameObject projectilePrefab;
    public float enginePower = 2.5f;


    // Properties

    public Body Body { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void Start() {
        StartCoroutine(Attack());
        StartCoroutine(Move());
    }

    // Public


    // Private

    private IEnumerator Attack()
    {
        // spawn projectile

        WaitForSeconds waitFor = new WaitForSeconds(3f);

        while (true) {
            InstantiateProjectile();
            yield return waitFor;
        }
    }

    private void InstantiateProjectile()
    {
        Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;
        Vector3 point = transform.position + direction * 4.5f;
        GameObject prefab = Instantiate(projectilePrefab, point, Quaternion.identity);
    }

    private IEnumerator Move()
    {
        Vector3 direction = (Player.Instance.transform.position - transform.position).normalized;

        while (true) {
            if (Vector3.Distance(transform.position, Player.Instance.transform.position) > 10f) {
                direction = (Player.Instance.transform.position - transform.position).normalized;
                Body.AddForce(direction * enginePower);
            }

            yield return null;
        }
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
    }

}
