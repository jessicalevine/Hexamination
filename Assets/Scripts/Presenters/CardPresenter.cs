using System;
using UnityEngine;

[RequireComponent(typeof(CardView))]
public class CardPresenter : MonoBehaviour {
    private CardView view;
    private Card model;

    public GameEvent playerDamageEvent;

    void Start() {
        if (view == null)
            view = GetComponent<CardView>();

        if (model != null)
            model.Discarded += OnDiscarded;

        if (playerDamageEvent == null)
            Debug.LogError("No player damage event on CardPresenter!");
    }

    private void OnDestroy() {
        if (model != null) {
            model.Discarded -= OnDiscarded;
        }
    }

    public void Setup(Card card) {
        view = GetComponent<CardView>();

        model = card;
        model.RegisterListeners(playerDamageEvent);

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
