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
    public int Score { get; set; }

    private float ElapsedTime { get; set; }
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
        Projectile projectile = other.transform.GetComponent<Projectile>();

        if (singularity != null) {
            Shields.ChangeShields(singularity.Damage());
        } else if (prize != null) {
            Score++;
            scoreTextElement.text = Score.ToString();
            Shields.ChangeShields(prize.shieldBoost);
        } else if (projectile != null) {
            Shields.ChangeShields(-projectile.damage);
            projectile.Body.RemoveFromSpace();
        }

        if (Shields.Empty()) {
            Space.Instance.EndGame();
        }
    }

    private void Start() {
        StartCoroutine(IncrementTime());
    }


    // Public

    public void Hide()
    {
        transform.gameObject.SetActive(false);
    }

    public void Reset()
    {
        Shields.ChangeShields(100);
    }


    // Private

    private IEnumerator IncrementTime()
    {
        WaitForSeconds waitFor = new WaitForSeconds(6f);

        while (true) {
            yield return waitFor;
            if (ElapsedTime % 5 == 0) {
                Space.Instance.SpawnTime = Mathf.Max(Space.Instance.SpawnTime - Mathf.Log(-Space.Instance.rotationSpeed) / 2f, 1.3f);
            }
            ElapsedTime++;
        }

    }

    private void SetComponents()
    {
        Body = GetComponent<Body>();
        ElapsedTime = 0f;
        Score = 0;
        Shields = GetComponent<Shields>();
    }
}
