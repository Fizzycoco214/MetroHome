using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackGroundSwitcher : MonoBehaviour
{
    public float blackSpeed = 0.2f;
    public float alphaSpeed = 0.2f;
    float t = 0.0f;

    private State state = State.background1;

    public bool shouldSwitch = false;

    float initR, initG, initB, initAlpha;
    SpriteRenderer renderer;

    enum State
    {
        background1,
        blackScreen,
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        initR = renderer.color.r;
        initG = renderer.color.g;
        initB = renderer.color.b;
        initAlpha = renderer.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.background1)
        {

            float lerpVal = Mathf.Lerp(0, 1, t);
            float newG = initG - (lerpVal * initG);
            float newR = initR - (lerpVal * initR);
            float newB = initB - (lerpVal * initB);

            renderer.color = new Color(newR, newG, newB);
            t += blackSpeed * Time.deltaTime;

            if (t > 1.0f)
            {
                state = State.blackScreen;
                t = 0.0f;
            }
        }
        
        if (state == State.blackScreen && shouldSwitch)
        {
            float lerpVal = Mathf.Lerp(0, 1, t);
            print(initAlpha - (lerpVal * initAlpha));
            renderer.color = new Color(0, 0, 0, initAlpha - (lerpVal * initAlpha));

            t += alphaSpeed * Time.deltaTime;
        }

    }
}
