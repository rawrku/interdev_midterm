using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite faceSprite;

    Sprite backSprite;
 
    SpriteRenderer myRenderer;

    public CardGameManager gameMan;

    public DeckManager deckMan;

    private Vector3 targetPos;
    float moveSpeed = 0.05f;

    public bool hovered;
    public bool picked;

    public enum CardValues
    {
        ROCK,
        PAPER,
        SCISSORS,
    }

    public CardValues cardValue;

    private void Start()
    {

        myRenderer = GetComponent<SpriteRenderer>();
        gameMan = GetComponent<CardGameManager>();
        deckMan = GetComponent<DeckManager>();


        backSprite = myRenderer.sprite;

        targetPos = transform.position;

    }

    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;


    public void FlipCards()
    {
        myRenderer.sprite = faceSprite;
    }

    public void CardBacks()
    {
        myRenderer.sprite = backSprite;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed);

        if (cardValue == CardValues.ROCK)
        {
            faceSprite = rockSprite;
        }

        if (cardValue == CardValues.PAPER)
        {
            faceSprite = paperSprite;
        }

        if (cardValue == CardValues.SCISSORS)
        {
            faceSprite = scissorsSprite;
        }
    }
    public CardValues GetValue()
    {
        return cardValue;
    }

    public void SetTargetPos(Vector3 newPos)
    {
        targetPos = newPos;
        targetPos.z = 0;
    }

    public void OnMouseEnter()
    {
        hovered = true;
    }

    public void OnMouseExit()
    {
        hovered = false;    
    }

    public void OnMouseDown()
    {
        picked = true;
    }
}
