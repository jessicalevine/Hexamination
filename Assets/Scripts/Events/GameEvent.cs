using System;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject {
    public event Action<int> Action;

    internal void Raise(int damage) {
        Action.Invoke(damage);
    }
}
