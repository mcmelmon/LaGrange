using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Inspector

    public bool centerOfUniverse = false;
    public GameObject linePrefab;


    // Properties

    public Attractor Attractor { get; set; }
    public LineRenderer Line { get; set; }
    public bool Merging { get; set; }
    public Repeller Repeller { get; set; }

    private Rigidbody Physics { get; set; }


    private void Awake() {
        SetComponents();
        StartCoroutine(WaitForSpace());
    }

    private void OnCollisionEnter(Collision other) {
        Body partner = other.transform.GetComponent<Body>();
        if (!Merging) Merge(partner);
    }

    private void Start() {
        ScaleHorizon();
        if (!centerOfUniverse) StartWithRandomForce();
    }

    private void Update() {
        float distanceFromCenter = Vector3.Distance(transform.position, Space.Instance.singularity.transform.position);
        if (distanceFromCenter > 250) RemoveFromSpace();
    }


    // Public

    public void AddForce(Vector3 force)
    {
        Physics.AddForce(force);
    }

    public float GetMass()
    {
        return Physics.mass;
    }

    public void IncreaseMass(float increase)
    {
        Physics.mass += increase;
    }

    public void RemoveFromSpace()
    {
        LineRenderer[] lines = GetComponentsInChildren<LineRenderer>();

        foreach (KeyValuePair<Body, LineRenderer> pair in Attractor.Lines) {
            Destroy(pair.Value.transform.gameObject);

            if (pair.Key.Attractor.Lines.ContainsKey(this)) {
                Destroy(pair.Key.Attractor.Lines[this]);
                pair.Key.Attractor.Lines.Remove(this);
            }
        }

        Space.Instance.Bodies.Remove(this);
        Destroy(this.transform.gameObject);
    }

    public void SetMass(float mass)
    {
        Physics.mass = mass;
    }


    // Private

    private void Merge(Body other)
    {
        if (GetMass() > other.GetMass()) {
            other.Merging = true;
            IncreaseMass(other.GetMass());
            transform.position = WeightedMidpoint(other);
            other.RemoveFromSpace();
        } else {
            Merging = true;
            other.IncreaseMass(GetMass());
            transform.position = WeightedMidpoint(other);
            RemoveFromSpace();
        }

        ScaleHorizon();
    }

    private void ScaleHorizon()
    {
        transform.localScale = new Vector3(1, 1, 1) * (0.5f + (Mathf.Log(6f * GetMass() / Space.Instance.G)));
    }

    private void SetComponents()
    {
        Attractor = GetComponent<Attractor>();
        Line = GetComponent<LineRenderer>();
        Merging = false;
        Physics = GetComponent<Rigidbody>();
        Repeller = GetComponent<Repeller>();

        if (!centerOfUniverse) Physics.mass = Random.Range(1, 10);
    }

    private void StartWithRandomForce()
    {
        int choice = Random.Range(0,4);
        Vector3 direction = new Vector3();

        switch (choice) {
            case 0:
                direction = Vector3.left;
                break;
            case 1:
                direction = Vector3.right;
                break;
            case 2:
                direction = Vector3.forward;
                break;
            case 3:
                direction = Vector3.back;
                break;
        }

        float expansion =  Random.Range(1,4);
        Vector3 force = direction * expansion;

        Physics.AddForce(force, ForceMode.VelocityChange);
    }

    private IEnumerator WaitForSpace()
    {
        while (Space.Instance == null) {
            yield return null;
        }

        Space.Instance.Bodies.Add(this);
    }

    private Vector3 WeightedMidpoint(Body other)
    {
        Vector3 direction = (transform.position - other.transform.position).normalized;
        float combinedMass = GetMass() + other.GetMass();

        return transform.position += direction * (combinedMass - GetMass()) / combinedMass;
    }
}
