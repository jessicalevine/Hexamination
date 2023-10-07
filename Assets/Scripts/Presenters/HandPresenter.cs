using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hand), typeof(HandView))]
public class HandPresenter : MonoBehaviour {
    private HandView view;
    private Hand model;

    [SerializeField] private GameObject[] cardLocations;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private CardEvent requestToggleZoomCardEvent;
    [SerializeField] private CastButtonView castButtonView;
    [SerializeField] private RitualizeButtonView ritualizeButtonView;


    private Dictionary<Card, CardView> cardViews;
    private CardPresenter zoomedCardPresenter = null;

    private void Start() {
        cardViews = new Dictionary<Card, CardView>();

        if (view == null)
            view = GetComponent<HandView>();

        model = GetComponent<Hand>();
        if (model != null) {
            model.CardCreated += OnCardCreated;
            model.CardMoved += OnCardMoved;
        }

        if (requestToggleZoomCardEvent == null)
            Debug.LogError("No requestToggleZoomCardEvent on HandPresenter!");
        else
            requestToggleZoomCardEvent.Action += OnToggleZoomCard;

        if (castButtonView == null)
            Debug.LogError("No castButton on HandPresenter!");
        if (ritualizeButtonView == null)
            Debug.LogError("No RitualizeButtonView on HandPresenter!");
    }

    private void OnDestroy() {
        if (model != null) {
            model.CardCreated -= OnCardCreated;
            model.CardMoved -= OnCardMoved;
        }

        if (view != null) {
            requestToggleZoomCardEvent.Action -= OnToggleZoomCard;
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

    private void OnToggleZoomCard(CardPresenter cardPresenterToToggle) {
        if (zoomedCardPresenter == cardPresenterToToggle) {
            Debug.Log("Requested to toggle the zoomed card, unzooming it and finishing immediately");
            zoomedCardPresenter.View().Unzoom();
            ResetZoomState();
        }
        else {
            if (zoomedCardPresenter != null) {
                Debug.Log("Requested to toggle and there's already a zoomed card, unzooming it");
                zoomedCardPresenter.View().Unzoom();
            } else {
                // If there wasn't a card already zoomed, we need to activate the buttons
                castButtonView.transform.gameObject.SetActive(true);
                ritualizeButtonView.transform.gameObject.SetActive(true);
            }

            Debug.Log("Requested to toggle an unzoomed card, zooming it");
            cardPresenterToToggle.Zoom();
            zoomedCardPresenter = cardPresenterToToggle;
        }
    }

    private void ResetZoomState() {
        zoomedCardPresenter = null;
        castButtonView.transform.gameObject.SetActive(false);
        ritualizeButtonView.transform.gameObject.SetActive(false);
    }

    public void CastZoomedCard() {
        zoomedCardPresenter.Model().Play();
        ResetZoomState();
    }

    public Hand Model() {
        return model;
    }

    public HandView View() {
        return view;
    }

    public CardPresenter ZoomedCardPresenter() {
        return zoomedCardPresenter;
    }
}
