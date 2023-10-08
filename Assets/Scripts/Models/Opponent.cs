using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : Character {
    [SerializeField] private List<OpponentAbility> abilityRotation;
    [SerializeField] private CharacterDamageEvent playerDamageEvent;

    public void UseAbility() {
        OpponentAbility nextAbility = abilityRotation[0];
        abilityRotation.RemoveAt(0);

        if (nextAbility.Damage > 0) {
            if (playerDamageEvent == null)
                Debug.LogError("No player damage event for opponent!");
            else
                playerDamageEvent.Raise(nextAbility.Damage);
        }

        abilityRotation.Add(nextAbility);
    }
}
