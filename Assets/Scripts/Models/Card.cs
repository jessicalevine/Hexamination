using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public Ability Ability { get; internal set; }
    public int Damage;
    public int Dispel;
    public int Resources;

    public event Action Discarded;

    private GameEvent playerDamageEvent;

    public Card(Ability ability) {
        Ability = ability;
        Damage = ability.InitialDamage;
        Dispel = ability.InitialDispel;
        Resources = ability.InitialResources;
    }

    public void Discard() {
        Discarded?.Invoke();
        // TODO Refactor to better event system so that I don't need ?. Cards should be MonoBehaviours.
    }

    public void Play() {
        if (Damage > 0) {
            if (playerDamageEvent == null)
                Debug.LogError("No player damage event for card!");
            else
                playerDamageEvent.Raise(Damage);
        }
        Discard();
    }

    internal void RegisterListeners(GameEvent playerDamageEventArg) {
        playerDamageEvent = playerDamageEventArg;
    }
}
