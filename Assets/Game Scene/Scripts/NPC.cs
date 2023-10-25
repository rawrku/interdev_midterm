using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public NotepadManager notepadManager; // Reference to the NotepadManager
    public TMP_Text dialogueText; // Reference to the TextMeshPro Text component for dialogue
    public GameObject dialogueBox; // Reference to the dialogue box UI GameObject

    private bool hasSpoken = false; // To prevent repeated dialogue

    private int pScore;

    private float timer;

    // Array of pre-written dialogue lines when the NPC gains a point
    private string[] gainPointDialogues = {
        "Nice try",
        "My lips are sealed",
        "Better luck next time...",
        "Looks like Oshawott is not going home...",
        "Oh man, this is looking bad for you..."
    };

    private void Start()
    {
        pScore = FindObjectOfType<Score>().playerScore;

        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Initially, hide the dialogue box
        }
    }

    // Show the dialogue in the dialogue box
    private void ShowDialogue(string text)
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
            dialogueText.text = text;
        }
    }

    // Hide the dialogue box
    private void HideDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    // Called when the player interacts with the NPC
    public void Interact()
    {
        timer++;

        if (notepadManager != null)
        {

            if (pScore == 1)
            {
                ShowDialogue("The hideout is in the cave.");
                notepadManager.CrossOutObjective();
            }
            if (pScore == 2)
            {
                ShowDialogue("Oshawott is being used for information about your agency.");
                notepadManager.CrossOutObjective();
            }
            else if (pScore == 3)
            {
                ShowDialogue("The boss is Spiritomb.");
                notepadManager.CrossOutObjective();
            }

            if (timer >= 15f)
            {
                HideDialogue();
            }
        }
    }

    // Called when the NPC gains a point
    public void OnNPCGainPoint()
    {
        timer++;

        if (gainPointDialogues.Length > 0)
        {
            // Randomly select a dialogue from the array
            int randomIndex = Random.Range(0, gainPointDialogues.Length);
            ShowDialogue(gainPointDialogues[randomIndex]);
        }

        if (timer >= 15f)
        {
            HideDialogue();
        }
    }
}