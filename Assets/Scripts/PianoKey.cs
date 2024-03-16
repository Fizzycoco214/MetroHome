using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public PianoInput inputManager;
    public KeyCode key;
    SpriteRenderer sprite;
    Color originalColor;
    bool hitCurrentNote = false;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(key))
        {
            sprite.color = Color.gray;
        }
        else {
            sprite.color = originalColor;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(key) && !hitCurrentNote)
        {
            inputManager.HitNote();
            hitCurrentNote = true;
        }

        if(Input.GetKey(key))
        {
            inputManager.HoldNote();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!hitCurrentNote)    
            inputManager.MissedNote();
        hitCurrentNote = false;
    }
}
