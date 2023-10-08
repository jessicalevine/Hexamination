using System;
using UnityEngine;

[RequireComponent(typeof(Character), typeof(CharacterView))]
public class CharacterPresenter : MonoBehaviour {
    [SerializeField] private CharacterDamageEvent characterDamageEvent;
    [SerializeField] private CharacterIncreaseDispelEvent characterIncreaseDispelEvent;


    protected Character model;
    protected CharacterView view;

    protected void Start() {
        model = GetComponent<Character>();
        view = GetComponent<CharacterView>();

        model.HealthChanged += OnHealthChanged;
        OnHealthChanged();

        model.DispelChanged += OnDispelChanged;
        OnDispelChanged();

        if (characterDamageEvent == null)
            Debug.LogError("No character damage event on [" + this.name + "]");
        else
            characterDamageEvent.Action += OnCharacterDamaged;

        if (characterIncreaseDispelEvent == null)
            Debug.LogError("No characterIncreaseDispelEvent on [" + this.name + "]");
        else
            characterIncreaseDispelEvent.Action += OnCharacterIncreaseDispel;
    }

    private void OnDestroy() {
        model.HealthChanged -= OnHealthChanged;
        model.DispelChanged -= OnDispelChanged;
        characterDamageEvent.Action -= OnCharacterDamaged;
    }

    private void OnCharacterDamaged(int damage) {
        model.ApplyDamage(damage);
    }
    private void OnCharacterIncreaseDispel(int dispel) {
        model.IncreaseDispel(dispel);
    }
    private void OnHealthChanged() {
        view.SetHealthUI(model.CurrentHealth);
    }

    private void OnDispelChanged() {
        view.SetDispelUI(model.CurrentDispel);
    }
}
