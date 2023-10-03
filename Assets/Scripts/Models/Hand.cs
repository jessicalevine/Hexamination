using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public const int MaxHandSize = 5;
    public Card[] Cards;

    public static Hand Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
        Cards = new Card[MaxHandSize];
    }

    public void DrawFull() {
        Card drawnCard;
        for (int i = 0; i < MaxHandSize; i++) {
            if (Cards[i] == null) {
                drawnCard = Deck.Instance.Draw();
                Cards[i] = drawnCard;
                Debug.Log("Drew card [" + drawnCard.Ability.AbilityName + "] into slot [" + i + "]");
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
                Cards[cardNumber] = null;
                ConsolidateHand();
            }
        }
    }

    public void ConsolidateHand() {
        Card[] newHand = new Card[MaxHandSize];
        int newHandIndex = 0;

        for (int oldHandIndex = 0; oldHandIndex < MaxHandSize; oldHandIndex++) {
            if (Cards[oldHandIndex] != null) {
                newHand[newHandIndex] = Cards[oldHandIndex];
                newHandIndex++;
            }
        }

        Cards = newHand;
    }
}
