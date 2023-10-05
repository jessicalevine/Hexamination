using System;
using UnityEngine;

[RequireComponent(typeof(CardView))]
public class CardPresenter : MonoBehaviour {
    private CardView view;
    private Card model;

    public CharacterDamageEvent opponentDamageEvent;
    public CharacterIncreaseDispelEvent playerIncreaseDispelEvent;

    void Start() {
        if (view == null)
            view = GetComponent<CardView>();

        if (model != null)
            model.Discarded += OnDiscarded;

        if (opponentDamageEvent == null)
            Debug.LogError("No opponent damage event on CardPresenter!");
    }

    private void OnDestroy() {
        if (model != null) {
            model.Discarded -= OnDiscarded;
        }
    }

    public void Setup(Card card) {
        view = GetComponent<CardView>();

        model = card;
        model.RegisterListeners(opponentDamageEvent, playerIncreaseDispelEvent);

        Ability a = model.Ability;
        view.SetAll(a.AbilityName, a.ManaCost, a);

    }

    private void OnDiscarded() {
        Destroy(this.gameObject);
    }

    public void Play() {
        model.Play();
    }
}
