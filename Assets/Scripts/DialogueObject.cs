using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "New Dialogue Event")]
public class DialogueObject : ScriptableObject
{

    
    public Dialogue[] dialogue;

    [Serializable]
    public class Dialogue
    {
        public enum Pose
        {
            playerCharacter = 0,
            otherCharacter = 1
        }

        public Pose pose;
        [TextArea(5,20)]
        public String dialogue;
    }
}
