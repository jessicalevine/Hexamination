using System;
using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour {
    [SerializeField] private TMP_Text characterHealthText;
    [SerializeField] private TMP_Text characterDispelText;


    public void SetHealthUI(int currentHealth) {
        characterHealthText.text = currentHealth.ToString();
    }

    internal void SetDispelUI(int currentDispel) {
        characterDispelText.text = currentDispel.ToString();
    }
}
