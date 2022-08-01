using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    Text texto;
    public float blinkFadeIn = 0.3f;
    public float blinkStay = 0.4f;
    public float blinkFadeOut = 0.5f;
    float timeChecker = 0;
    Color color;

    void Start()
    {
        texto = this.GetComponent<Text>();
        color = texto.color;
    }

    void Update()
    {
        timeChecker += Time.deltaTime;
        if(timeChecker < blinkFadeIn)
        {
            texto.color = new Color(color.r, color.g, color.b, timeChecker / blinkFadeIn);
        }else if(timeChecker < blinkFadeIn + blinkStay)
        {
            texto.color = new Color(color.r, color.g, color.b, 1);
        }else if(timeChecker < blinkFadeIn + blinkStay + blinkFadeOut)
        {
            texto.color = new Color(color.r, color.g, color.b, 1 - (timeChecker - (blinkFadeIn + blinkStay))/blinkFadeOut);
        }
        else
        {
            timeChecker = 0;
        }
    }
}
