using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    // Inspector

    public float changeSpeed = 0.5f;

    public Slider shieldSlider;


    // Unity

    private void Awake() {
        shieldSlider.value = 1f;
        GetComponentInParent<Shields>().OnShieldChanged += HandleShieldChanged;
    }


    // Private

    private IEnumerator ChangePercentage(float percentage)
    {
        float counter = 0f;

        while (counter < changeSpeed) {
            counter += Time.deltaTime;
            shieldSlider.value = Mathf.Lerp(shieldSlider.value, percentage, counter / changeSpeed);
            yield return null;
        }
    }

    private void HandleShieldChanged(float percentage)
    {
        StartCoroutine(ChangePercentage(percentage));
    }
}
