using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoInput : MonoBehaviour
{
    public float score = 0;
    public float scoreForNote = 2;
    public float scoreForHold = 0.2f;
    public float penaltyForMissedNote = -1;


    public key[] keys;

    [Serializable]
    public class key {
        public bool pressed = false;
        public KeyCode keyCode;
        public GameObject keyObject;
    }

    private void Awake() {
        foreach (key k in keys)
        {
            k.keyObject.GetComponent<PianoKey>().key = k.keyCode;
            k.keyObject.GetComponent<PianoKey>().inputManager = this;
        }
    
    }

    void Update()
    {
        foreach (key k in keys)
        {
            if (Input.GetKeyDown(k.keyCode))
            {
                k.pressed = true;
            }
            if (Input.GetKeyUp(k.keyCode))
            {
                k.pressed = false;
            }
        }
    }

    public void HitNote()
    {
        score += scoreForNote;
    }

    public void HoldNote()
    {
        score += scoreForHold * Time.deltaTime;
    }

    public void MissedNote()
    {
        score -= penaltyForMissedNote;
    }
}
