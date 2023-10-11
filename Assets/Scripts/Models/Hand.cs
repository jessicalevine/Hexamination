using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public const int MaxHandSize = 5;
    public Card[] Cards;
    public Deck Deck;

    public event Action<int, Card> CardCreated;
    public event Action<int, Card> CardMoved;

    [SerializeField] private GeneralEvent cardDrawnEvent;


    private void Start() {
        Cards = new Card[MaxHandSize];

        if (cardDrawnEvent == null)
            Debug.LogError("No cardDrawnEvent on Hand!");
    }

    public void DrawFull() {
        Card drawnCard;
        for (int i = 0; i < MaxHandSize; i++) {
            if (Cards[i] == null) {
                drawnCard = new Card(Deck.DrawAbility());
                CardCreated.Invoke(i, drawnCard);
                Cards[i] = drawnCard;
                Debug.Log("DREW card [" + drawnCard.Ability.AbilityName + "] into slot [" + i + "]");

                // Inform anyone listening that a card was just drawn
                cardDrawnEvent.Raise();
            }
        }
    }

    public void DiscardHand() {
        for(int i = 0; i < MaxHandSize; i++) {
            if (!(Cards[i] == null)) {
                if (Cards[i].retainThisTurn) {
                    Cards[i].retainThisTurn = false;
                } else {
                    DiscardCard(i);
                }
            }
        }
    }

    public void Discard(int cardNumber = 0) {
        
        if (cardNumber > MaxHandSize) {
            Debug.LogError("Card number [" + cardNumber + "] greater than MaxHandSize [" + MaxHandSize + "]");
        } else {

            if (Cards[cardNumber] == null) {
                Debug.LogWarning("Card number [" + cardNumber + "] is already null before Discard");
            } else {
                DiscardCard(cardNumber);
                ConsolidateHand();
            }
        }
    }
    private void ConsolidateHand() {
        Card[] newHand = new Card[MaxHandSize];
        int newHandIndex = 0;

        for (int oldHandIndex = 0; oldHandIndex < MaxHandSize; oldHandIndex++) {
            if (Cards[oldHandIndex] != null) {
                newHand[newHandIndex] = Cards[oldHandIndex];
                CardMoved.Invoke(newHandIndex, newHand[newHandIndex]);
                newHandIndex++;
            }
        }

        Cards = newHand;
    }

    private void DiscardCard(int loc) {
        Deck.AddDiscard(Cards[loc].Ability);
        Cards[loc].Discard();
        Cards[loc] = null;
        Debug.Log("DISCARDING card number [" + loc + "]");
    }
}
