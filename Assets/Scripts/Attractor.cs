using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    // Properties

    public Body Body { get; set; }
    public Dictionary<Body, LineRenderer> Lines { get; set; }


    // Unity

    private void Awake() {
        SetComponents();
    }

    private void FixedUpdate() {
        List<Body> otherBodies = Space.Instance.GetBodies(Body);
        foreach (var body in otherBodies) {
            if (!Body.Merging()) Attract(body);
        }
    }


    // Private

    private void Attract(Body other)
    {
        Vector3 direction = transform.position - other.transform.position;
        float distance = direction.magnitude;

        float gravitation = Space.Instance.G * (Body.GetMass() * other.GetMass()) / Mathf.Pow(2f * distance, 2);
        Vector3 force = direction.normalized * gravitation;

        DrawForceTo(other, gravitation);

        if (!float.IsNaN(force.x)) other.AddForce(force);
    }

    public void DrawForceTo(Body other, float gravitation)
    {
        // We end up drawing two lines between each body, but this ends up
        // saving Body's RemoveFromSpace the trouble of searching for
        // unconnected lines

        if (Body.IsShip() || other.IsShip()) return;

        if (Lines.ContainsKey(other)) {
            Lines[other].enabled = true;
            float scaledForce = Mathf.Sqrt(gravitation);
            Lines[other].startWidth = scaledForce > 0.3f ? Mathf.Min(scaledForce, 2) : 0;
            Lines[other].endWidth = scaledForce > 0.3f ? Mathf.Min(scaledForce, 2) : 0;
            Lines[other].SetPosition(0, transform.position);
            Lines[other].SetPosition(1, other.transform.position);
        } else {
            GameObject prefab = Instantiate(Space.Instance.linePrefab, transform.position, Quaternion.identity);
            prefab.transform.SetParent(transform);
            Lines[other] = prefab.GetComponent<LineRenderer>();
            Lines[other].positionCount = 2;
            Lines[other].enabled = false;
        }
    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
        Lines = new Dictionary<Body, LineRenderer>();
    }
}
