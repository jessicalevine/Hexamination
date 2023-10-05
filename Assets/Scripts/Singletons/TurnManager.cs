using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private const int _maxMana = 3;
    public int mana;
    public Hand Hand;
    public Deck Deck;

    public void BeginEncounter() {
        Deck.BeginEncounter();
        BeginTurn();
    }

    public void BeginTurn() {
        mana = _maxMana;
        Hand.DrawFull();
    }

    public void EndTurn() {
        Hand.DiscardHand();

        // TODO opponent attacks
        BeginTurn();
    }
}
