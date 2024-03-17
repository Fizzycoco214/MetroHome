using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackGroundSwitcher : MonoBehaviour
{
    public float fadeSpeed = 2.0f;
    float t = 0.0f;

    public Sprite endBackground;

    public bool fade = false;

    bool isBlack = false;

    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        FadeToBlack();
    }

    // Update is called once per frame
    void Update()
    {
        isBlack = renderer.color == Color.black;

        if (fade)
        {
            if (isBlack)
            {
                renderer.sprite = endBackground;
            }

            t += fadeSpeed * Time.deltaTime;

            
        }
        else
        {
            t -= fadeSpeed * Time.deltaTime;
        }

        t = Mathf.Clamp(t, 0.0f, 1.0f);
        renderer.color = Color.Lerp(Color.white,Color.black, t);
    }

    public void FadeToBlack()
    {
        fade = true;
    }

    public void Unfade()
    {
        fade = false;
    }
}
