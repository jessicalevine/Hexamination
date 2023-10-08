using System;
using TMPro;
using UnityEngine;

public class OpponentView : CharacterView {
    [SerializeField] private TMP_Text intentText;

    void Start() {
        if (intentText == null)
            Debug.LogError("No intentText on OpponentView!");
    }

    internal void SetIntent(OpponentAbility opponentAbility) {
        if (opponentAbility.MinDamage > 0) {
            intentText.text = String.Format("Attacking for {0}-{1}", opponentAbility.MinDamage, opponentAbility.MaxDamage);
        } else if (opponentAbility.MinDispel > 0) {
            intentText.text = String.Format("Dispeling for {0}-{1}", opponentAbility.MinDispel, opponentAbility.MaxDispel);
        } else {
            Debug.LogWarning("Opponent ability doesn't do anything in view?");
        }
    }
}
