using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public ParticleSystem keyPressParticles;
    public ParticleSystem keyHoldParticles;
    public ParticleSystemRenderer keyPressParticlesRenderer;
    ParticleSystem spotParticles;
    public PianoInput inputManager;
    public KeyCode key;
    SpriteRenderer sprite;
    Color originalColor;
    bool hitCurrentNote = false;
    public float particleSpinSpeed = 1.0f;
    float particlePivot = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        spotParticles = GameObject.Find("Spot Particles").GetComponent<ParticleSystem>();
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
            keyHoldParticles.Stop();
        }

        if(keyPressParticles.isEmitting) {
            keyPressParticlesRenderer.pivot = new Vector2(particlePivot,0);
            particlePivot += particleSpinSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(Input.GetKey(key))
        {
            if (inputManager.inFlow)
                keyHoldParticles.Play();
            if(!hitCurrentNote)
            {
                print("hit note");
                if(inputManager.inFlow)
                {
                    if(Random.Range(0.0f,1) >= 0.3)
                    {
                        spotParticles.Play();
                    }
                    keyPressParticles.Play();
                    particlePivot = 0.0f;
                }
                inputManager.HitNote();
                hitCurrentNote = true;
            }
            inputManager.HoldNote();
        }
        else
        {
            if(inputManager.inFlow)
                keyHoldParticles.Stop();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!hitCurrentNote)    
            inputManager.MissedNote();
        hitCurrentNote = false;
    }
}
