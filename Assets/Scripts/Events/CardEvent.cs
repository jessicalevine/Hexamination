using System;
using UnityEngine;

[CreateAssetMenu]
public class CardEvent : ScriptableObject {
    public event Action<Card> Action;

    internal void Raise(Card card) {
        Action.Invoke(card);
    }
}
