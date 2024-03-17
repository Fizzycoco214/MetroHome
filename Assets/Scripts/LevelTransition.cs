using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    String sceneName;
    public float fadeSpeed = 1.0f;

    bool isfading = false;
    float t = 0.0f;
    RawImage blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen = GetComponent<RawImage>();
        t = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isfading)
        {
            t += fadeSpeed * Time.deltaTime;
        }
        else
        {
            t -= fadeSpeed * Time.deltaTime;
        }

        if(isfading && t > 1.0f)
        {
            SceneManager.LoadScene(sceneName);
        }

        t = Mathf.Clamp(t, 0.0f, 1.0f);
        blackScreen.color = Color.Lerp(Color.clear, Color.black, t);
    }

    public void nextScene(String name)
    {
        this.sceneName = name;
        isfading = true;
    }
}
