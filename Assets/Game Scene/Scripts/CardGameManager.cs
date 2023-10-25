using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// this is so i can use the .Last() function
using System.Linq;

public class CardGameManager : MonoBehaviour
{

    public enum GameState
    {
        COMPDEAL,
        PLAYERDEAL,
        COMPCHOOSE,
        PLAYERCHOOSE,
        RESOLVE,
        DISCARD,
        RESHUFFLE
    }

    // game state setting
    public static GameState state;

    //discrad pile and shuffling vars
    [Header("DISCARD PILE AND SHUFFLING")]
    public Transform discardPile;
    public static List<GameObject> discardDeck = new List<GameObject>();
    public Transform deckPos;
    private Vector2 discardPos;
    private float discardPileSpacing = 0.05f; // Adjust this value to control spacing between cards in the discard pile

    //player hand vars
    [Header("PLAYER HAND")]
    public List<GameObject> playerHand = new List<GameObject>();
    public int playerHandCount;
    public Transform playerPos;
    public Transform playerCard;
    GameObject playerPlayed;

    //copmuter hand vars
    [Header("COMPUTER HAND")]
    public List<GameObject> computerHand = new List<GameObject>();
    public int computerHandCount;
    public Transform computerPos;
    public Transform compCard;
    GameObject compPlayed;

    //timer vars
    [Header("TIMERS")]
    int maxTimer = 20;
    int timer = 20;
    int revealTimer;

    //hover
    float hoverAmount = 0.5f;

    //eval vars
    bool eval;

    //getting scripts
    [Header("SCRIPTS")]
    public Score scoreMan;
    public Card card;
    public DeckManager deckMan;
    public CardHoverEffect handHoverEffect;

    // audio vars
    [Header("AUDIO")]
    public AudioSource source;
    public AudioClip win;
    public AudioClip loose;
    public AudioClip place;
    public AudioClip shuffle;

    private void Start()
    {
        state = GameState.COMPDEAL;

        scoreMan = GetComponent<Score>();
        card = GetComponent<Card>();
        deckMan = GetComponent<DeckManager>();

        discardPos = discardPile.transform.position;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case GameState.COMPDEAL:
                //deckMan.ShuffleDeck();
                // re-setting the eval varaible to false so it can eval again after a shuffle
                eval = false;
                timer--;
                //after the set time has passed
                if (timer <= 0)
                {
                    //if the 3 or less cards in the opponents's hand
                    if (computerHand.Count < computerHandCount)
                    {
                        // run the function to deal the computer cards
                        CompDealCard();
                    }
                    else
                    {
                        // otherwise go to the next state
                        state = GameState.PLAYERDEAL;
                    }

                   timer = maxTimer;
                }
                break;
            case GameState.PLAYERDEAL:
                timer--;
                revealTimer++;
                if (timer <= 0)
                {
                    if (playerHand.Count < playerHandCount)
                    {
                        PlayerDealCard();
                        timer = maxTimer;
                    }
                }
                if (revealTimer >= 70)
                {
                    for (int i = 0; i < playerHand.Count; i++)
                    {
                        playerHand[i].GetComponent<Card>().FlipCards();
                        state = GameState.COMPCHOOSE;
                    }
                    revealTimer = 0;
                }
                break;
            case GameState.COMPCHOOSE:
                timer--;
                if (timer <= -35)
                {
                    if (computerHand.Count == 3)
                    {
                        CompChooseCard();
                        state = GameState.PLAYERCHOOSE;
                    }
                    timer = maxTimer;
                }
                break;
            case GameState.PLAYERCHOOSE:

                for (int i = 0; i < playerHand.Count; i++)
                {
                    if (playerHand[i].GetComponent<Card>().hovered)
                    {
                        playerHand[i].GetComponent<Card>().SetTargetPos(new Vector3(playerHand[i].transform.position.x, playerPos.position.y + hoverAmount));

                    }
                    else
                    {
                        playerHand[i].GetComponent<Card>().SetTargetPos(new Vector3(playerHand[i].transform.position.x, playerPos.position.y));
                    }
                    if (playerHand[i].GetComponent<Card>().picked)
                    {
                        GameObject pickedCard = playerHand[i];
                        Vector3 newPos = playerCard.transform.position;
                        pickedCard.GetComponent<Card>().SetTargetPos(newPos);
                        source.PlayOneShot(place);
                        playerPlayed = pickedCard;
                        state = GameState.RESOLVE;
                    }
                }
                
                break;
            case GameState.RESOLVE:
                timer--;
                if (timer <= -100)
                {
                    // check the comp hand of cards
                    for (int i = 0; i < computerHand.Count; i++)
                    {
                        // if the card in the comp hand was the played card
                        if (computerHand[i] == compPlayed)
                        {
                            handHoverEffect.ResetHoverSprite();
                            // reveal the face
                            computerHand[i].GetComponent<Card>().FlipCards();

                            if (eval == false)
                            {
                                Evaluate();
                            }
                            eval = true;
                        }
                    }

                    timer = maxTimer;
                }
                break;
            case GameState.DISCARD:
                timer--;
                if (timer <= -85)
                {
                    for (var i = 0; i < computerHand.Count; i++)
                    {
                        if (computerHand[i] == compPlayed)
                        {
                            GameObject card = computerHand[i];
                            card.GetComponent<SpriteRenderer>().sortingOrder = discardDeck.Count;
                            Vector2 newPos = discardPos + new Vector2(0, discardDeck.Count * discardPileSpacing);
                            card.GetComponent<Card>().SetTargetPos(newPos);
                            computerHand.Remove(card);
                            discardDeck.Add(card);
                            source.PlayOneShot(place);
                        }
                    }
                }

                if (timer <= -100)
                {
                    for(var i = 0; i < playerHand.Count; i++)
                    {
                        if (playerHand[i] == playerPlayed)
                        {
                            // add to discard pile and remove from hand
                            GameObject card = playerHand[i];
                            card.GetComponent<SpriteRenderer>().sortingOrder = discardDeck.Count;
                            Vector2 newPos = discardPos + new Vector2(0, discardDeck.Count * discardPileSpacing);
                            card.GetComponent<Card>().SetTargetPos(newPos);
                            playerHand.Remove(card);
                            discardDeck.Add(card);
                            source.PlayOneShot(place);
                        }
                    }
                }
                //15 sec later, if opponent hand has 2 cards
                if (timer <= -150 && computerHand.Count == 2)
                {
                    for (var i = 0; i < computerHand.Count; i++)
                    {
                        //reveal card
                        computerHand[i].GetComponent<Card>().FlipCards();
                        // add to discard pile and remove from hand
                        GameObject card = computerHand[i];
                        card.GetComponent<SpriteRenderer>().sortingOrder = discardDeck.Count;
                        Vector2 newPos = discardPos + new Vector2(0, discardDeck.Count * discardPileSpacing);
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        computerHand.Remove(card);
                        discardDeck.Add(card);
                        source.PlayOneShot(place);
                    }
                    
                }

                if (timer <= -130 && computerHand.Count == 1)
                {
                    for (var i = 0; i < computerHand.Count; i++)
                    {
                        //reveal card
                        computerHand[i].GetComponent<Card>().FlipCards();
                        // add to discard pile and remove from hand
                        GameObject card = computerHand[i];
                        card.GetComponent<SpriteRenderer>().sortingOrder = discardDeck.Count;
                        Vector2 newPos = discardPos + new Vector2(0, discardDeck.Count * discardPileSpacing);
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        computerHand.Remove(card);
                        discardDeck.Add(card);
                        source.PlayOneShot(place);
                    }
                }

                //15 sec later, if player hand has 2 cards
                if (timer <= -145 && playerHand.Count == 2)
                {
                    for (var i = 0; i < playerHand.Count; i++)
                    {
                        // add to discard pile and remove from hand
                        GameObject card = playerHand[i];
                        card.GetComponent<SpriteRenderer>().sortingOrder = discardDeck.Count;
                        Vector2 newPos = discardPos + new Vector2(0, discardDeck.Count * discardPileSpacing);
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        playerHand.Remove(card);
                        discardDeck.Add(card);
                        source.PlayOneShot(place);
                    }
                }

                //15 sec later, if player hand has 1 card
                if (timer <= -160 && playerHand.Count == 1)
                {
                    for (var i = 0; i < playerHand.Count; i++)
                    {
                        // add to discard pile and remove from hand
                        GameObject card = playerHand[i];
                        card.GetComponent<SpriteRenderer>().sortingOrder = discardDeck.Count;
                        Vector2 newPos = discardPos + new Vector2(0, discardDeck.Count * discardPileSpacing);
                        card.GetComponent<Card>().SetTargetPos(newPos);
                        playerHand.Remove(card);
                        discardDeck.Add(card);
                        source.PlayOneShot(place);
                    }
                }

                if (timer <= -175)
                {
                    if (DeckManager.deck.Count > 0)
                    {
                        state = GameState.COMPDEAL;
                    } else
                    {
                        state = GameState.RESHUFFLE;
                    }

                    timer = maxTimer;
                }

                for (int i = 0; i < discardDeck.Count; i++)
                {
                    // moving the last thing in the list to the front. 
                    var moveToFirst = discardDeck.Last();
                    discardDeck.RemoveAt(discardDeck.Count - 1);
                    discardDeck.Insert(0, moveToFirst);
                }
                break;
            case GameState.RESHUFFLE:
                timer--;
                if (timer == 5)
                {
                    source.PlayOneShot(shuffle);
                }
                for (var i = 0; i < discardDeck.Count; i++)
                {
                    if (timer < 0 - i * 2)
                    {
                        // flipping the cards to be the back
                        discardDeck[i].GetComponent<Card>().CardBacks();

                        // mmoving the cards to the place where the deck is
                        Vector3 newPos = deckPos.transform.position;
                        discardDeck[i].GetComponent<Card>().SetTargetPos(newPos);

                    }

                    if (timer < -90 - i)
                    {
                        // Shuffling the discard deck
                        GameObject temp = discardDeck[i];
                        int randomIndex = Random.Range(i, discardDeck.Count);
                        discardDeck[i] = discardDeck[randomIndex];
                        discardDeck[randomIndex] = temp;

                        // Adding it back to the deck
                        GameObject card = discardDeck[i];
                        DeckManager.deck.Add(card);
                        discardDeck.Remove(card);
                        Vector3 cardPosition = card.transform.position;
                        cardPosition.y += i * deckMan.cardSpacing;
                        card.transform.position = cardPosition;
                    }
                }
                // if the deck has 24 cards
                if (DeckManager.deck.Count == 24)
                {
                    if (timer < -240)
                    {
                        state = GameState.COMPDEAL;
                    }
                    
                }
                break;
        }
    }

    void CompDealCard()
    {
        // Computer cards
        GameObject nextCard = DeckManager.deck[0]; // Select the top card
        Vector3 newPos = computerPos.transform.position;
        newPos.x = newPos.x + (2f * computerHand.Count);
        handHoverEffect.HoverOverCard(nextCard);
        nextCard.GetComponent<Card>().SetTargetPos(newPos);
        computerHand.Add(nextCard);
        source.PlayOneShot(place);
        DeckManager.deck.RemoveAt(0); // Remove the top card from the deck
    }
    void PlayerDealCard()
    {
        // Player cards
        GameObject nextCard = DeckManager.deck[0]; // Select the top card
        Vector3 newPos = playerPos.transform.position;
        newPos.x = newPos.x + (2f * playerHand.Count);
        nextCard.GetComponent<Card>().SetTargetPos(newPos);
        playerHand.Add(nextCard);
        source.PlayOneShot(place);
        DeckManager.deck.RemoveAt(0); // Remove the top card from the deck

    }

    void CompChooseCard()
    {
        GameObject randomCard = computerHand[Random.Range(0, 2)];
        Vector3 newPos = compCard.transform.position;
        handHoverEffect.HoverOverCard(randomCard);
        randomCard.GetComponent<Card>().SetTargetPos(newPos);
        source.PlayOneShot(place);
        compPlayed = randomCard;
    }
    void Evaluate()
    {
        if (compPlayed.GetComponent<Card>().GetValue() == Card.CardValues.ROCK)
        {
            switch (playerPlayed.GetComponent<Card>().GetValue())
            {
                case Card.CardValues.ROCK:
                    Tie();
                    break;
                case Card.CardValues.PAPER:
                    Win();
                    break;
                case Card.CardValues.SCISSORS:
                    Loose();
                    break;
            }
        }
        if (compPlayed.GetComponent<Card>().GetValue() == Card.CardValues.PAPER)
        {
            switch (playerPlayed.GetComponent<Card>().GetValue())
            {
                case Card.CardValues.ROCK:
                    Loose();
                    break;
                case Card.CardValues.PAPER:;
                    Tie();
                    break;
                case Card.CardValues.SCISSORS:
                    Win();
                    break;
            }
        }

        if (compPlayed.GetComponent<Card>().GetValue() == Card.CardValues.SCISSORS)
        {
            switch (playerPlayed.GetComponent<Card>().GetValue())
            {
                case Card.CardValues.ROCK:
                    Win();
                    break;
                case Card.CardValues.PAPER:
                    Loose();
                    break;
                case Card.CardValues.SCISSORS:
                    Tie();
                    break;
            }
        }

    }
    void Win()
    {
        source.PlayOneShot(win);
        scoreMan.AddPlayerPoint();
        state = GameState.DISCARD;
    }
    void Tie()
    {
        FindObjectOfType<NPC>().OnNPCGainPoint();
        state = GameState.DISCARD;
    }
    void Loose()
    {
        source.PlayOneShot(loose);
        scoreMan.AddCompPoint();
        state = GameState.DISCARD;
    }

}
