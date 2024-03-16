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
            normal = 0,
            laugh = 1,
            sad = 2,
            thumbsUp = 3,
        }

        public Pose pose;
        [TextArea(5,20)]
        public String dialogue;
    }
}
