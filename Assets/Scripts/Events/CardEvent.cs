using System;
using UnityEngine;

[CreateAssetMenu]
public class CardEvent : ScriptableObject {
    public event Action<CardPresenter> Action;

    internal void Raise(CardPresenter cardPresenter) {
        Action.Invoke(cardPresenter);
    }
}
