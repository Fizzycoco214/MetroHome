using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteManager : MonoBehaviour
{
    PianoInput inputManager;
    public GameObject notePrefab;
    public Song currentSong;
    bool songStarted = false;

    public float noteSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<PianoInput>();
        startSong();
    }

    // Update is called once per frame
    void Update()
    {
        if (!songStarted)
            return;
        
        foreach (Song.Note note in currentSong.songNotes)
        {
            if(note.time < Time.time && !note.havePlayed)
            {
                print("spawning note");
                SpawnNote(note.note, note.duration);
                note.havePlayed = true;
            }
        }
    }

    public void SpawnNote(Song.Notes note, float duration)
    {
       GameObject curNote = Instantiate(notePrefab, inputManager.keys[(int)note].keyObject.transform.position + new Vector3(0,10,0), Quaternion.identity);
       curNote.GetComponent<SpriteRenderer>().size = new Vector2(1, duration * noteSpeed);
       curNote.GetComponent<BoxCollider2D>().size = new Vector2(curNote.transform.localScale.x, duration * noteSpeed);
       curNote.GetComponent<SpawnedNote>().speed = noteSpeed;
       curNote.transform.position += new Vector3(0, duration / 2, 0);
    }

    public void startSong()
    {
        songStarted = true;
        foreach (Song.Note note in currentSong.songNotes)
        {
            note.havePlayed = false;
        }
    }
}
