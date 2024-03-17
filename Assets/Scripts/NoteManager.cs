using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteManager : MonoBehaviour
{
    PianoInput inputManager;
    public GameObject notePrefab;
    public Song currentSong;
    public float startFlowTime = 0;
    public float endFlowTime = 0;


    bool songStarted = false;
    AudioSource audioSource;
    public float noteSpeed = 2;

    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inputManager = FindObjectOfType<PianoInput>();
        startSong();
    }

    // Update is called once per frame
    void Update()
    {

        if (!songStarted)
            return;

        if(!audioSource.isPlaying)
        {
            print("song ended");
            songStarted = false;
        }
        else 
        {
            if(Time.time - startTime > startFlowTime && !inputManager.inFlow)
            {
                inputManager.inFlow = true;
            }
            if(Time.time - startTime > endFlowTime && inputManager.inFlow)
            {
                inputManager.inFlow = false;
            }
        }
    }

    public void SpawnNote(Song.Notes note, float time, float duration)
    {
       GameObject curNote = Instantiate(notePrefab, inputManager.keys[(int)note].keyObject.transform.position + new Vector3(0,time * noteSpeed + (2.706f / 2f),0), Quaternion.identity);
       curNote.GetComponent<SpriteRenderer>().size = new Vector2(0.3f, duration * noteSpeed);
       curNote.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, duration * noteSpeed);
       curNote.GetComponent<SpawnedNote>().speed = noteSpeed;
       curNote.transform.position += new Vector3(0, duration * noteSpeed / 2f, 0);

         curNote.transform.position -= new Vector3(0, noteSpeed / 2, 0);
       
    }

    public void startSong()
    {
        startTime = Time.time;
        songStarted = true;
        audioSource.clip = currentSong.music;
        audioSource.Play();
        foreach (Song.Note note in currentSong.songNotes)
        {
            SpawnNote(note.note, note.time, note.duration);
        }
    }
}
