using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public bool inDialogue = false;
    DialogueObject curDialogue;
    public DialogueObject[] dayStartDialogue;

    public Sprite[] poses;


    int dialogueNum = 0;

    float percentThroughDialogue = 0;
    public float textSpeed;


    CanvasGroup group;
    Image background;
    TMP_Text dialogueText;
    RawImage characterImage;

    void Start()
    {  
        characterImage = GetComponentInChildren<RawImage>();
        group = GetComponentInChildren<CanvasGroup>();
        background = GetComponentInChildren<Image>();
        dialogueText = GetComponentInChildren<TMP_Text>();

        group.alpha = 0;

        try{StartDialogue(dayStartDialogue[GameObject.Find("Player Data").GetComponent<PlayerData>().currentScene - 1]);}
        catch{print("NO DIALOGUE FOR THIS SCREEN TODAY, (maybe come back tommorow?)");}
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0))
        {
            if(percentThroughDialogue < 100)
            {
                percentThroughDialogue = 100;
            }
            else
            {
                percentThroughDialogue = 0;
                dialogueNum++;
            }

            if(inDialogue && dialogueNum >= curDialogue.dialogue.Length)
            {
                inDialogue = false;
            }
        }

        if(inDialogue)
        {
            characterImage.texture = poses[(int)curDialogue.dialogue[dialogueNum].pose].texture;
            group.alpha = Mathf.Lerp(group.alpha, 1, Time.deltaTime * 5f);
            dialogueText.text = curDialogue.dialogue[dialogueNum].dialogue.Substring(0,Math.Min(curDialogue.dialogue[dialogueNum].dialogue.Length, (int)(curDialogue.dialogue[dialogueNum].dialogue.Length * (percentThroughDialogue / 100.0))));
            

            if(percentThroughDialogue < 100)
            {
                percentThroughDialogue += textSpeed * Time.deltaTime / (curDialogue.dialogue[dialogueNum].dialogue.Length / 25.0f);
            }
            group.blocksRaycasts = true;
        }
        else
        {
            group.blocksRaycasts = false;
            group.alpha = Mathf.Lerp(group.alpha, 0, Time.deltaTime * 5f);
        }
    }

    public void StartDialogue(DialogueObject dialogue)
    {
        group.alpha = 1;

        curDialogue = dialogue;
        inDialogue = true;
        dialogueNum = 0;
        percentThroughDialogue = 0;
    }
}
