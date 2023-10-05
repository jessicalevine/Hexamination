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

    private CharacterDamageEvent opponentDamageEvent;
    private CharacterIncreaseDispelEvent playerIncreaseDispelEvent;
    private CardEvent attemptPlayCardEvent;

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

    public void AttemptPlay() {
        attemptPlayCardEvent.Raise(this);
    }

    public void Play() {
        if (Damage > 0) {
            if (opponentDamageEvent == null)
                Debug.LogError("No opponent damage event for card!");
            else
                opponentDamageEvent.Raise(Damage);
        }
        if (Dispel > 0) {
            if (playerIncreaseDispelEvent == null)
                Debug.LogError("No playerIncreaseDispelEvent for card!");
            else
                playerIncreaseDispelEvent.Raise(Dispel);
        }
        Discard();
    }

    internal void RegisterListeners(CharacterDamageEvent opponentDamageEventArg, CharacterIncreaseDispelEvent playerIncreaseDispelEventArg, CardEvent attemptPlayCardEventArg) {
        opponentDamageEvent = opponentDamageEventArg;
        playerIncreaseDispelEvent = playerIncreaseDispelEventArg;
        attemptPlayCardEvent = attemptPlayCardEventArg;
    }
}
