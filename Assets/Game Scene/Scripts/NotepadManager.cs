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
        objectives.Add("Find Out Who This is");
        objectives.Add("Find Out Where the Hideout is");
        objectives.Add("Find Out What they are doing with Oshawott");
        objectives.Add("Find Out Who the Boss is");

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
        if (objectives.Count > 0)
        {
            // Apply a strikethrough style to the completed objective
            objectives[0] = "<s>" + objectives[0] + "</s>";

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