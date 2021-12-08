using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Inspector

    public TextMeshProUGUI scoreTextElement;


    // Properties

    public static Player Instance { get; set; }
    public Body Body { get; set; }

    private int Score { get; set; }
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


    // Public

    public void RaiseScore()
    {
        Score++;
        scoreTextElement.text = Score.ToString();
    }


    // Private

    private void SetComponents()
    {
        Body = GetComponent<Body>();
        Score = 0;
        Shields = GetComponent<Shields>();
    }
}
