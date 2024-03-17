using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song")]
public class Song : ScriptableObject
{
    public enum Notes
    {
        C = 0,
        Db = 1,
        D = 2,
        Eb = 3,
        E = 4,
        F = 5,
        FSharp = 6,
        G = 7,
        Ab = 8,
        A = 9,
        Bb = 10,
        B = 11
    }

    [System.Serializable]
    public class Note
    {
        public float time;
        public float duration;
        public Notes note;

        [HideInInspector]
        public bool havePlayed = false;
    }

    public AudioClip music;
    public Note[] songNotes;
}
