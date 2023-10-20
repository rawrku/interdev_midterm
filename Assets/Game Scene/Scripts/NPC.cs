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

    private void Start()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Initially, hide the dialogue box
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasSpoken)
        {
            hasSpoken = true;
            ShowDialogue("Hello, I have something to say...");
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
        if (notepadManager != null)
        {
            notepadManager.CrossOutObjective();
            ShowDialogue("I am blah blah");
            //HideDialogue(); // Hide the dialogue box after the interaction
        }
    }

    // Called when the NPC gains a point
    public void OnNPCGainPoint()
    {
        ShowDialogue("NPC gained a point!");
    }
}