using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : Character {
    [SerializeField] private List<OpponentAbility> abilityRotation;
    [SerializeField] private CharacterDamageEvent playerDamageEvent;

    private new void Awake() {
        maxHealth = 45;
        base.Awake();
    }

    public void UseAbility() {
        OpponentAbility nextAbility = PeekIntent();
        abilityRotation.RemoveAt(0);

        if (nextAbility.MinDamage > 0) {
            if (playerDamageEvent == null)
                Debug.LogError("No player damage event for opponent!");
            else
                playerDamageEvent.Raise(Random.Range(nextAbility.MinDamage, nextAbility.MaxDamage + 1));
        } else if (nextAbility.MinDispel > 0) {
            IncreaseDispel(Random.Range(nextAbility.MinDispel, nextAbility.MaxDispel + 1));
        } else {
            Debug.LogWarning("Opponent ability doesn't do anything in model?");
        }

        abilityRotation.Add(nextAbility);
    }

    public OpponentAbility PeekIntent() {
        return abilityRotation[0];
    }
}
