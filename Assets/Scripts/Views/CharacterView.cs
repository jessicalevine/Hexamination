using System;
using TMPro;
using UnityEngine;

public class CharacterView : MonoBehaviour {
    [SerializeField] private TMP_Text characterHealthText;
    [SerializeField] private TMP_Text characterDispelText;

    private NumberAnimationManager healthAnimationManager;
    private NumberAnimationManager dispelAnimationManager;

    private void Awake() {
        healthAnimationManager = new NumberAnimationManager(characterHealthText);
        dispelAnimationManager = new NumberAnimationManager(characterDispelText);
    }

    private void Update() {
        healthAnimationManager.WhenUpdate();
        dispelAnimationManager.WhenUpdate();
    }

    public void AnimateHealthUI(int currentHealth) {
        healthAnimationManager.Setup(currentHealth);
        healthAnimationManager.Begin();
    }

    internal void AnimateDispelUI(int currentDispel) {
        dispelAnimationManager.Setup(currentDispel);
        dispelAnimationManager.Begin();
    }
}
