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

    private float typingSpeed = 25f;
    private bool isTyping;
    private bool instantDisplay; // To check if text should be displayed instantly

    private string currentPlayerDialogue;
    private string currentNpcDialogue;

    [Header("AUDIO")]
    public AudioSource source;
    public AudioClip cofTalk;
    public AudioClip pikaTalk;

    private void Start()
    {
        // Enqueue player and NPC dialogues
        playerDialogues.Enqueue("Well, Well, Well, we meet again Cofagrigus.");
        playerDialogues.Enqueue("Let me cut to the chase, where is Oshawott?");
        playerDialogues.Enqueue("And if I lose?");

        npcDialogues.Enqueue("Hello, Detective Pikachu.");
        npcDialogues.Enqueue("Why don't we play a game of cards. If you win, I'll tell you what you want.");
        npcDialogues.Enqueue("Well, let's just say Oshawott won't be so much of a problem anymore...");

        isPlayerSpeaking = true;
        canAdvanceDialogue = true;
        DisplayNextDialogue();
    }

    private void Update()
    {
        if (isDialogueCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextScene();
            }
        }
        else if (canAdvanceDialogue && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Skip typing and display text instantly
                instantDisplay = true;
            }
            else
            {
                DisplayNextDialogue();
            }
        }
    }

    private void DisplayNextDialogue()
    {
        if (isDialogueCompleted)
        {
            return;
        }

        if (playerDialogues.Count > 0 || npcDialogues.Count > 0)
        {
            if (isPlayerSpeaking)
            {
                source.PlayOneShot(pikaTalk);
                currentPlayerDialogue = playerDialogues.Dequeue();
                StartCoroutine(TypeDialogue(playerDialogueText, currentPlayerDialogue));
            }
            else
            {
                source.PlayOneShot(cofTalk);
                currentNpcDialogue = npcDialogues.Dequeue();
                StartCoroutine(TypeDialogue(npcDialogueText, currentNpcDialogue));
            }

            isPlayerSpeaking = !isPlayerSpeaking;
        }
        else
        {
            canAdvanceDialogue = false;
            isDialogueCompleted = true;
        }
    }

    private IEnumerator TypeDialogue(TMP_Text textComponent, string dialogue)
    {
        isTyping = true;
        textComponent.text = "";
        foreach (char letter in dialogue)
        {
            if (instantDisplay)
            {
                textComponent.text = dialogue; // Display text instantly
                instantDisplay = false;
                break;
            }
            textComponent.text += letter;
            yield return new WaitForSeconds(1 / typingSpeed);
        }
        isTyping = false;
        source.Stop();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("CardGame");
    }
}