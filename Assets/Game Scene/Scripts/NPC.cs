using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public NotepadManager notepadManager; // Reference to the NotepadManager
    public TMP_Text dialogueText; // Reference to the TextMeshPro Text component for dialogue
    public GameObject dialogueBox; // Reference to the dialogue box UI GameObject

    [Header("AUDIO")]
    public AudioSource source;
    public AudioClip cofTalk;

    //dialogue lines when the NPC gains a point
    private string[] gainPointDialogues = {
        "Nice try",
        "My lips are sealed.",
        "Better luck next time...",
        "Looks like Oshawott is not going home...",
        "Oh man, this is looking bad for you...",
        "Yikes.",
        "Just leave already, you can't win.",
        "I'm in your head."
    };

    private float textTypeSpeed = 30f; // Characters per second

    private void Start()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false); // Initially, hide the dialogue box
        }
    }

    // Show the dialogue in the dialogue box with typing effect
    private IEnumerator ShowDialogueWithTyping(string text)
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
            dialogueText.text = "";

            for (int i = 0; i < text.Length; i++)
            {
                dialogueText.text += text[i];
                yield return new WaitForSeconds(1f / textTypeSpeed);
            }
        }
    }

    // Called when the player gains a point
    public void Interact()
    {
        if (notepadManager != null)
        {
            if (FindObjectOfType<Score>().playerScore == 1)
            {
                StartCoroutine(ShowDialogueWithTyping("The hideout is in the cave."));
                notepadManager.CrossOutObjective();
            }
            if (FindObjectOfType<Score>().playerScore == 2)
            {
                StartCoroutine(ShowDialogueWithTyping("Oshawott is being used for information about your agency."));
                notepadManager.CrossOutObjective();
            }
            if (FindObjectOfType<Score>().playerScore == 3)
            {
                StartCoroutine(ShowDialogueWithTyping("The boss is Spiritomb."));
                notepadManager.CrossOutObjective();
                Invoke("LoadEndScene", 3.5f); // Load the end scene after 2 seconds
            }
        }
    }

    // Function to load the end scene
    private void LoadEndScene()
    {
        SceneManager.LoadScene("End");
    }

    // Called when the NPC gains a point
    public void OnNPCGainPoint()
    {
        if (gainPointDialogues.Length > 0)
        {
            int randomIndex = Random.Range(0, gainPointDialogues.Length);
            StartCoroutine(ShowDialogueWithTyping(gainPointDialogues[randomIndex]));
            source.PlayOneShot(cofTalk);
        }
    }
}