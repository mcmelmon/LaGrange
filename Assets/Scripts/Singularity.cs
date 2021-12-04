using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : MonoBehaviour
{
    // Properties

    public Body Body { get; set; }
    public bool Merging { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void OnCollisionEnter(Collision other) {
        Singularity partner = other.transform.GetComponent<Singularity>();

        if (partner != null) {
            if (!Merging) Merge(partner);
        }
    }

    private void Start() {
        Space.Instance.Bodies.Add(Body);
        ScaleHorizon();
        StartWithRandomForce();
    }

    private void Update() {
        float distanceFromPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (distanceFromPlayer > 250) RemoveFromSpace();
    }


    // Public

    public void RemoveFromSpace()
    {
        LineRenderer[] lines = GetComponentsInChildren<LineRenderer>();

        foreach (KeyValuePair<Body, LineRenderer> pair in Body.Attractor.Lines) {
            Destroy(pair.Value.transform.gameObject);

            if (pair.Key.Attractor.Lines.ContainsKey(Body)) {
                Destroy(pair.Key.Attractor.Lines[Body]);
                pair.Key.Attractor.Lines.Remove(Body);
            }
        }

        Space.Instance.Bodies.Remove(Body);
        Destroy(this.transform.gameObject);
    }


    // Private

    private void Merge(Singularity other)
    {
        if (Body.GetMass() > other.Body.GetMass()) {
            other.Merging = true;
            Body.IncreaseMass(other.Body.GetMass());
            transform.position = Body.WeightedMidpoint(other.Body);
            other.RemoveFromSpace();
        } else {
            Merging = true;
            other.Body.IncreaseMass(Body.GetMass());
            transform.position = Body.WeightedMidpoint(other.Body);
            RemoveFromSpace();
        }

        ScaleHorizon();
    }

    private void ScaleHorizon()
    {
        transform.localScale = new Vector3(1, 1, 1) * (0.5f + (Mathf.Log(6f * Body.GetMass() / Space.Instance.G)));
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
        Merging = false;

        Body.SetMass(Random.Range(1, 10));
    }

    private void StartWithRandomForce()
    {
        Vector3 direction = new Vector3(Random.Range(-359, 359),Random.Range(-359, 359),Random.Range(-359, 359)).normalized;
        float oomph =  Random.Range(7, 11);
        Vector3 force = direction * oomph;

        Body.AddForce(force, ForceMode.VelocityChange);
    }
}
