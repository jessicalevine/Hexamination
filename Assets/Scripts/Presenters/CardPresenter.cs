using System;
using UnityEngine;

[RequireComponent(typeof(CardView))]
public class CardPresenter : MonoBehaviour {
    [SerializeField] private CardView view;
    [SerializeField] private Card model;

    void Start() {
        if (view == null) {
            view = GetComponent<CardView>();
        }

        if (model != null) {
            model.Discarded += OnDiscarded;
        }
    }

    private void OnDestroy() {
        if (model != null) {
            model.Discarded -= OnDiscarded;
        }
    }

    public void Setup(Card card) {
        view = GetComponent<CardView>();

        model = card;
        Ability a = model.Ability;
        view.SetAll(a.AbilityName, a.ManaCost, a);
    }

    private void OnDiscarded() {
        Destroy(this.gameObject);
    }
}
