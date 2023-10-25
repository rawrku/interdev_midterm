using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotepadManager : MonoBehaviour
{
    public TMP_Text notepadText;

    // List to store objectives
    public List<string> objectives = new List<string>();

    private void Start()
    {
        // Initialize the objectives when the game starts
        objectives.Add("Find Out where the hideout is");
        objectives.Add("Find Out what they are doing with Oshawott");
        objectives.Add("Find Out who the boss is");

        // Update the notepad display
        UpdateNotepadText();
    }

    public void AddObjective(string objective)
    {
        // Add a new objective to the list
        objectives.Add(objective);

        // Update the notepad display
        UpdateNotepadText();
    }

    public void CrossOutObjective()
    {
        if (FindObjectOfType<Score>().playerScore == 1)
        {
            // Apply a strikethrough style to the completed objective
            objectives[0] = "<mark=#FF0000aa>" + objectives[0] + "</mark>";

            // Update the notepad display
            UpdateNotepadText();
        }
        if (FindObjectOfType<Score>().playerScore == 2)
        {
            // Apply a strikethrough style to the completed objective
            objectives[1] = "<mark=#FF0000aa>" + objectives[1] + "</mark>";

            // Update the notepad display
            UpdateNotepadText();
        }
        if (FindObjectOfType<Score>().playerScore == 3)
        {
            // Apply a strikethrough style to the completed objective
            objectives[2] = "<mark=#FF0000aa>" + objectives[2] + "</mark>";

            // Update the notepad display
            UpdateNotepadText();
        }
    }

    private void UpdateNotepadText()
    {
        string notepadContent = "Objectives:\n";
        foreach (string objective in objectives)
        {
            notepadContent += " - " + objective + "\n";
        }

        // Set the notepad text with objectives, including any strikethrough styles
        notepadText.text = notepadContent;
    }
}