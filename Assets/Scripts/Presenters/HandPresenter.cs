using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hand))]
public class HandPresenter : MonoBehaviour {
    // [SerializeField] private HandView view;
    [SerializeField] private Hand model;
    [SerializeField] private GameObject[] cardLocations;
    [SerializeField] private GameObject cardPrefab;

    private Dictionary<Card, CardView> cardViews;

    private void Start() {
        cardViews = new Dictionary<Card, CardView>();

        model = GetComponent<Hand>();
        if (model != null) {
            model.CardCreated += OnCardCreated;
            model.CardMoved += OnCardMoved;
        }
    }

    private void OnDestroy() {
        if (model != null) {
            model.CardCreated -= OnCardCreated;
            model.CardMoved -= OnCardMoved;
        }
    }

    private void OnCardCreated(int loc, Card card) {
        GameObject cardObject = Instantiate(cardPrefab, cardLocations[loc].transform.position, Quaternion.identity);

        CardPresenter cardPresenter = cardObject.GetComponent<CardPresenter>();
        cardPresenter.Setup(card);

        CardView cardView = cardPrefab.GetComponent<CardView>();
        cardViews[card] = cardView;
    }

    private void OnCardMoved(int loc, Card card) {
        cardViews[card].SetPosition(cardLocations[loc].transform.position);
    }
}
