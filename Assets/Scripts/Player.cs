using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Inspector

    public TextMeshProUGUI timeTextElement;


    // Properties

    public static Player Instance { get; set; }
    public Body Body { get; set; }
    public int ElapsedTime { get; set; }
    public float SpawnTime { get; set; }

    private Shields Shields { get; set; }



    // Unity

    private void Awake() {
        if (Instance != null) {
            Debug.Log("More than one Player instance");
            Destroy(this);
            return;
        }

        Instance = this;
        SetComponents();
    }

    private void OnCollisionEnter(Collision other) {
        Singularity singularity = other.transform.GetComponent<Singularity>();
        Prize prize = other.transform.GetComponent<Prize>();

        if (singularity != null) {
            Shields.ChangeShields(-10f);
        } else if (prize != null) {
            Shields.ChangeShields(+10f);
        }
    }

    private void Start() {
        StartCoroutine(IncrementTime());
    }


    // Private

    private IEnumerator IncrementTime()
    {
        WaitForSeconds waitFor = new WaitForSeconds(6f);

        while (true) {
            yield return waitFor;
            if (ElapsedTime % 5 == 0) {
                Space.Instance.rotationSpeed -= 0.3f; // rotation direction is clockwise, so negative speed
                SpawnTime = Mathf.Max(SpawnTime - Mathf.Log(-Space.Instance.rotationSpeed) / 2f, 1.3f);
            }
            ElapsedTime++;
            timeTextElement.text = ElapsedTime.ToString();
        }

    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
        ElapsedTime = 0;
        Shields = GetComponent<Shields>();
        SpawnTime = 4f;
    }
}
