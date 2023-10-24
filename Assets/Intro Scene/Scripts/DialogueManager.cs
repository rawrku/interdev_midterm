using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    // i had to follow a lot of youtube videos for this code so thats why it doesnt look like my normal code

    public TMP_Text playerDialogueText;
    public TMP_Text npcDialogueText;

    private Queue<string> playerDialogues = new Queue<string>();
    private Queue<string> npcDialogues = new Queue<string>();

    private bool isPlayerSpeaking;
    private bool isDialogueCompleted;
    private bool canAdvanceDialogue;

    private void Start()
    {
        // Enqueue player and NPC dialogues
        playerDialogues.Enqueue("Well, Well, Well, we meet again Cofagrigus");
        playerDialogues.Enqueue("Let me cut to the chase, where is Oshawott");
        playerDialogues.Enqueue("And if I lose?");

        npcDialogues.Enqueue("Hello, Detective Pikachu.");
        npcDialogues.Enqueue("Why don't we play a game of cards. If you win, I'll tell you what you want.");
        npcDialogues.Enqueue("Well, let's just say Oshawott won't be so much of a problem anymore...");

        isPlayerSpeaking = true; // Player speaks first
        canAdvanceDialogue = true; // Allow the player to advance the dialogue
        DisplayNextDialogue();
    }

    private void Update()
    {
        // Check for player input to advance dialogues (by pressing Space)
        if (canAdvanceDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextDialogue();
        }

        // Check if all dialogues are completed, and load the scene
        if (!canAdvanceDialogue)
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
        }
    }

    private void DisplayNextDialogue()
    {
        if (isDialogueCompleted)
        {
            return; // All dialogues are completed, no need to proceed
        }

        // Check if there are more dialogues to show
        if (playerDialogues.Count > 0 || npcDialogues.Count > 0)
        {
            string currentDialogue = isPlayerSpeaking ? playerDialogues.Dequeue() : npcDialogues.Dequeue();

            if (isPlayerSpeaking)
            {
                playerDialogueText.text = currentDialogue;
            }
            else
            {
                npcDialogueText.text = currentDialogue;
            }

            isPlayerSpeaking = !isPlayerSpeaking;
        }
        else
        {
            // All dialogues for this cycle are done
            canAdvanceDialogue = false;
            isDialogueCompleted = true;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("CardGame"); // Load the next scene
    }
}