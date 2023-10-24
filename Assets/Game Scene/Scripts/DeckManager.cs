using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    // Hold prefab of the card
    public GameObject cardPrefab;

    // Array of the card faces
    public Sprite[] cardFaces;

    // The count in the deck
    public int deckCount;

    // Spacing between cards
    public float cardSpacing = 1f;

    // List for the deck
    public static List<GameObject> deck = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < deckCount; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, gameObject.transform);

            // Add spacing between cards
            Vector3 cardPosition = newCard.transform.position;
            cardPosition.y += i * cardSpacing;
            newCard.transform.position = cardPosition;

            Card newCardScript = newCard.GetComponent<Card>();
            newCardScript.faceSprite = cardFaces[i % 3];
            deck.Add(newCard);

            if (i < 8)
            {
                newCardScript.cardValue = Card.CardValues.ROCK;
            }
            else if (i < 16)
            {
                newCardScript.cardValue = Card.CardValues.PAPER;
            }
            else
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

    //public void ShuffleDeck()
    //{
    //    // Create a list of shuffled indices
    //    List<int> shuffledIndices = new List<int>();
    //    for (int i = 0; i < deckCount; i++)
    //    {
    //        shuffledIndices.Add(i);
    //    }

    //    // Fisher-Yates shuffle the indices
    //    for (int i = shuffledIndices.Count - 1; i > 0; i--)
    //    {
    //        int randomIndex = Random.Range(0, i + 1);
    //        int temp = shuffledIndices[i];
    //        shuffledIndices[i] = shuffledIndices[randomIndex];
    //        shuffledIndices[randomIndex] = temp;
    //    }

    //    // Clear the current deck
    //    deck.Clear();

    //    // Rebuild the deck based on the shuffled indices
    //    for (int i = 0; i < shuffledIndices.Count; i++)
    //    {
    //        GameObject newCard = Instantiate(cardPrefab, gameObject.transform);
    //        Card newCardScript = newCard.GetComponent<Card>();
    //        newCardScript.faceSprite = cardFaces[i];
    //        deck.Add(newCard);

    //        if (i < 8)
    //        {
    //            newCardScript.cardValue = Card.CardValues.ROCK;
    //        }
    //        else if (i < 16)
    //        {
    //            newCardScript.cardValue = Card.CardValues.PAPER;
    //        }
    //        else
    //        {
    //            newCardScript.cardValue = Card.CardValues.SCISSORS;
    //        }
    //    }
    //}
}