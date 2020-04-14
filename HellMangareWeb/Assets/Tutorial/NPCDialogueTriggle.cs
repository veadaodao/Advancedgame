using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTriggle : MonoBehaviour
{
    public Dialogue dialogue;
    public NPCTutorial NPCtutorial;
    public NPCTutorialTwo NPCTutorialTwo;
    public NPCTutorialThree NPCTutorialThree;
    public NPCTutorialFour NPCTutorialFour;
    private void Update()
    {
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        if (NPCtutorial.BeginDialogue)
        {
            FindObjectOfType<DialogManager>().StartDialogue(dialogue);
            NPCtutorial.BeginDialogue = false;
        }
        if (NPCTutorialTwo.BeginDialogueTwo)
        {
            FindObjectOfType<DialogManager>().StartDialogue(dialogue);
            NPCTutorialTwo.BeginDialogueTwo = false;
        }
        if (NPCTutorialThree.BeginDialogueThree)
        {
            FindObjectOfType<DialogManager>().StartDialogue(dialogue);
            NPCTutorialThree.BeginDialogueThree = false;
        }
        if (NPCTutorialFour.BeginDialogueFour)
        {
            FindObjectOfType<DialogManager>().StartDialogue(dialogue);
            NPCTutorialFour.BeginDialogueFour = false;
        }
    }
}
