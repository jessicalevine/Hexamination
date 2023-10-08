using System;
using UnityEngine;

public class Card {
    public Ability Ability { get; internal set; }

    private int naturalDamage;
    public bool DamageDirty = false;

    private int naturalDispel;
    public bool DispelDirty = false;

    private int resources;
    public bool ResourcesDirty = false;

    public event Action Discarded;

    private CharacterDamageEvent opponentDamageEvent;
    private CharacterIncreaseDispelEvent playerIncreaseDispelEvent;

    private int ritualCount = 0;
    public bool ritualizedThisTurn = false;

    public bool retainThisTurn = false;

    public event Action CardUpdated;

    public Card(Ability ability) {
        Ability = ability;
        naturalDamage = ability.InitialDamage;
        naturalDispel = ability.InitialDispel;
        resources = ability.InitialResources;
    }

    public void Discard() {
        Discarded?.Invoke();
        // TODO Refactor to better event system so that I don't need ?. Cards should be MonoBehaviours.
    }

    private void DamageOpponent(int damage) {
        if (opponentDamageEvent == null)
            Debug.LogError("No opponent damage event for card!");
        else
            opponentDamageEvent.Raise(damage);
    }

    private void IncreaseDispel(int increase) {
        if (playerIncreaseDispelEvent == null)
            Debug.LogError("No playerIncreaseDispelEvent for card!");
        else
            playerIncreaseDispelEvent.Raise(increase);
    }

    public void Cast() {
        if (Damage() > 0) {
            DamageOpponent(Damage());
        }
        if (Dispel() > 0) {
            IncreaseDispel(Dispel());
        }
        Discard();
    }
    public void Ritualize() {
        ritualizedThisTurn = true;
        retainThisTurn = true;

        if (RitualImmediateDamage() > 0) {
            DamageOpponent(RitualImmediateDamage());
        }

        if (RitualImmediateDispel() > 0) {
            IncreaseDispel(RitualImmediateDispel());
        }
    }

    internal void RegisterListeners(CharacterDamageEvent opponentDamageEventArg, CharacterIncreaseDispelEvent playerIncreaseDispelEventArg) {
        opponentDamageEvent = opponentDamageEventArg;
        playerIncreaseDispelEvent = playerIncreaseDispelEventArg;
    }

    public void AdvanceRitual() {
        bool changed = false;
        if (Ability.RitualDamageRamp > 0) {
            naturalDamage += Ability.RitualDamageRamp;
            changed = true;
            DamageDirty = true;
        }
        if (Ability.RitualDispelRamp > 0) {
            naturalDispel += Ability.RitualDispelRamp;
            changed = true;
            DispelDirty = true;
        }
        if (Ability.RitualResourceRamp > 0) {
            resources += Ability.RitualResourceRamp;
            changed = true;
            ResourcesDirty = true;
            if (Ability.PerResourceDamage > 0) {
                DamageDirty = true;
            }
            if (Ability.PerResourceDispel > 0) {
                DispelDirty = true;
            }
        }

        if (changed) {
            CardUpdated.Invoke();
        }

        ritualCount++;
    }

    public int RitualImmediateDamage() {
        return Ability.RitualDamageImmediate + Ability.RitualPerResourceDamageImmediate * Resources();
    }
    public int RitualImmediateDispel() {
        return Ability.RitualDispelImmediate + Ability.RitualPerResourceDispelImmediate * Resources();
    }

    public int Damage() {
        return naturalDamage + Ability.PerResourceDamage * Resources();
    }

    public int Dispel() {
        return naturalDispel + Ability.PerResourceDispel * Resources();
    }

    public int Resources() {
        return resources;
    }
    public int RitualCount() {
        return ritualCount;
    }
}
