using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoverEffect : MonoBehaviour
{
    public Transform hoverSprite; // The hovering sprite (hand)
    public float hoverHeight = 0.5f; // Adjust the height for the hover effect
    public float hoverSpeed = 2.0f; // Adjust the speed of hovering

    private Vector3 initialHoverPosition;
    private Vector3 targetPosition;
    private GameObject currentCard; // Reference to the card being hovered over

    private void Start()
    {
        initialHoverPosition = hoverSprite.transform.position;
    }

    private void Update()
    {
        if (currentCard != null)
        {
            // Move the hover sprite to follow the card's movement
            targetPosition = currentCard.transform.position + Vector3.up * hoverHeight;
            hoverSprite.transform.position = Vector3.MoveTowards(hoverSprite.transform.position, targetPosition, hoverSpeed * Time.deltaTime);
        }
    }

    public void HoverOverCard(GameObject cardToHover)
    {
        // Set the current card
        currentCard = cardToHover;
    }

    public void ResetHoverSprite()
    {
        // Clear the reference to the current card
        currentCard = null;

        // Move the hover sprite back to its initial position
        hoverSprite.transform.position = initialHoverPosition;
    }
}