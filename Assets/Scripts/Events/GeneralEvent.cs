using System;
using UnityEngine;

[CreateAssetMenu]
public class GeneralEvent : ScriptableObject {
    public event Action Action;

    internal void Raise() {
        Action.Invoke();
    }
}