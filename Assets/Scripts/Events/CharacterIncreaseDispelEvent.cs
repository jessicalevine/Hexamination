using System;
using UnityEngine;

[CreateAssetMenu]
public class CharacterIncreaseDispelEvent : ScriptableObject {
    public event Action<int> Action;

    internal void Raise(int dispel) {
        Action.Invoke(dispel);
    }
}
