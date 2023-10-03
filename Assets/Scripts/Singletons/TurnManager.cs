using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private const int _maxMana = 3;
    public int mana;

    public static TurnManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void BeginTurn() {
        mana = _maxMana;
        Hand.Instance.DrawFull();
    }
    private void EndTurn() {
        for (int i = 0; i < Hand.MaxHandSize; i++) {
            Hand.Instance.Discard(i);
            // TODO make more efficient after implementing rituals
        }

        // TODO opponent attacks
        BeginTurn();
    }
}
