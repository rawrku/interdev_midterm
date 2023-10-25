using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text playerScoreText;
    public TMP_Text computerScoreText;
    public NotepadManager notepadManager; 
    public NPC npc; 

    public int playerScore;
    public int compScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScoreText.text = playerScore.ToString();
        computerScoreText.text = compScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayerPoint()
    {
        playerScore += 1;
        playerScoreText.text = playerScore.ToString();
        npc.Interact(); // Notify the NPC when the player gains a point
    }

    public void AddCompPoint()
    {
        compScore += 1;
        computerScoreText.text = compScore.ToString();
        npc.OnNPCGainPoint(); // Notify the NPC when it gains a point
    }
}