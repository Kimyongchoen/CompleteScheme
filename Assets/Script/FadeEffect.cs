using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();

        Color color = image.color;
        color.a = 1;
        image.color = color;
        StartCoroutine("FadeEffectStart");

    }

    private IEnumerator FadeEffectStart()
    {
        
        Color color = image.color;
        while (color.a >= 0)
        {
            color.a -= Time.deltaTime;
            image.color = color;

            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

}
