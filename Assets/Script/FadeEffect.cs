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
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color;
        if(color.a > 0)
        {
            color.a -= Time.deltaTime;
        }
        image.color = color;
    }
}
