using System;
using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour {
    [SerializeField] private TMP_Text characterHealthText;

    public void SetHealthUI(int currentHealth) {
        characterHealthText.text = currentHealth.ToString();
    }
}
