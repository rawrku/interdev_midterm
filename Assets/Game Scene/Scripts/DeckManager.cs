using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    //hold prefab of the card
    public GameObject cardPrefab;

    //Array of the card faces
    public Sprite[] cardFaces;

    //the count in the deck
    public int deckCount;

    //list for the deck
    public static List<GameObject> deck = new List<GameObject>();

    private void Start()
    {
        //card = GetComponent<Card>();

       for (int i = 0; i < deckCount; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, gameObject.transform);
            Card newCardScript = newCard.GetComponent<Card>();
            newCardScript.faceSprite = cardFaces[i % 3];
            deck.Add(newCard);

            if (i < 8)
            {
                newCardScript.cardValue = Card.CardValues.ROCK;

            } else if (i < 16)
            {
                newCardScript.cardValue = Card.CardValues.PAPER;
            } else
            {
                newCardScript.cardValue = Card.CardValues.SCISSORS;
            }
        }

        for (int i = 0; i < deck.Count; i++)
        {
            GameObject temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
