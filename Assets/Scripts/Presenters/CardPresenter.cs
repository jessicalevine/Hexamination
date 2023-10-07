using System;
using UnityEngine;

[RequireComponent(typeof(CardView))]
public class CardPresenter : MonoBehaviour {
    private Card model;
    private CardView view;

    [SerializeField] private CharacterDamageEvent opponentDamageEvent;
    [SerializeField] private CharacterIncreaseDispelEvent playerIncreaseDispelEvent;
    [SerializeField] private CardEvent attemptPlayCardEvent;

    void Start() {
        if (view == null)
            view = GetComponent<CardView>();

        if (model != null)
            model.Discarded += OnDiscarded;

        if (opponentDamageEvent == null)
            Debug.LogError("No opponent damage event on CardPresenter!");
        if (playerIncreaseDispelEvent == null)
            Debug.LogError("No playerIncreaseDispelEvent on CardPresenter!");
        if (attemptPlayCardEvent == null)
            Debug.LogError("No attemptPlayCardEvent on CardPresenter!");
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

    public void AttemptPlay() {
        attemptPlayCardEvent.Raise(this);
    }

    public void Zoom() {
        view.Zoom();
    }

    public Card Model() {
        return model;
    }

    public CardView View() {
        return view;
    }
}
