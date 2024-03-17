using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class NoteManager : MonoBehaviour
{
    PianoInput inputManager;
    public GameObject notePrefab;
    public Song currentSong;
    public float startFlowTime = 0;
    public float endFlowTime = 0;


    public bool songStarted = false;
    AudioSource audioSource;
    public float noteSpeed = 2;

    public UnityEvent onFlowStart;
    public UnityEvent onFlowEnd;
    public UnityEvent onSongEnd;

    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inputManager = FindObjectOfType<PianoInput>();
        audioSource.clip = currentSong.music;
        spawnNotes();
    }

    // Update is called once per frame
    void Update()
    {

        if (!songStarted)
            return;

        if(songStarted && !audioSource.isPlaying && Time.time - startTime > 1)
        {
            print("song ended");
            onSongEnd.Invoke();
            songStarted = false;
        }
        else
        {
            if(Time.time - startTime > startFlowTime && !inputManager.inFlow)
            {
                inputManager.inFlow = true;
                onFlowStart.Invoke();
            }
            if(Time.time - startTime > endFlowTime && inputManager.inFlow)
            {
                inputManager.inFlow = false;
                onFlowEnd.Invoke();
            }
        }
    }

    public void SpawnNote(Song.Notes note, float time, float duration)
    {
       GameObject curNote = Instantiate(notePrefab, inputManager.keys[(int)note].keyObject.transform.position + new Vector3(0,time * noteSpeed + (2.56f / 2f),0), Quaternion.identity);
       curNote.GetComponent<SpriteRenderer>().size = new Vector2(0.3f, duration * noteSpeed);
       curNote.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, duration * noteSpeed);
       curNote.GetComponent<SpawnedNote>().speed = noteSpeed;
       curNote.GetComponent<SpawnedNote>().noteManager = this;
       curNote.transform.position += new Vector3(0, duration * noteSpeed / 2f, 0);

        curNote.transform.position += new Vector3(0, noteSpeed / 2, 0);
       
    }

    void spawnNotes()
    {
        foreach (Song.Note note in currentSong.songNotes)
        {
            SpawnNote(note.note, note.time, note.duration);
        }
    }

    public void startSong()
    {
        startTime = Time.time;
        songStarted = true;
        audioSource.Play();
    }
}