using System;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    private const int _maxMana = 3;
    public int mana;
    public Deck Deck;
    [SerializeField] private Opponent opponent;
    [SerializeField] private HandPresenter handPresenter;

    [SerializeField] private GeneralEvent attemptCastEvent;
    [SerializeField] private GeneralEvent attemptRitualizeEvent;
    [SerializeField] private GeneralEvent beginTurnEvent;
    [SerializeField] private GeneralEvent endTurnEvent;


    // move to view
    public TMP_Text manaText;

    void Start() {
        if (attemptCastEvent == null)
            Debug.LogError("No attemptCastEvent on TurnManager!");
        else
            attemptCastEvent.Action += OnAttemptCast;

        if (attemptRitualizeEvent == null)
            Debug.LogError("No attemptRitualizeEvent on TurnManager!");
        else
            attemptRitualizeEvent.Action += OnAttemptRitualize;

        if (beginTurnEvent == null)
            Debug.LogError("No beginTurnEvent on TurnManager!");

        if (endTurnEvent == null)
            Debug.LogError("No endTurnEvent on TurnManager!");

        if (handPresenter == null)
            Debug.LogError("No handPresenter on TurnManager!");

        if (manaText == null)
            Debug.LogError("No manaText on TurnManager!");
    }

    private void OnDestroy() {
        attemptCastEvent.Action -= OnAttemptCast;
        attemptRitualizeEvent.Action -= OnAttemptRitualize;
    }

    public void BeginEncounter() {
        Deck.BeginEncounter();
        BeginTurn();
    }

    public void BeginTurn() {
        mana = _maxMana;
        UpdateView();
        // TODO Mana as own MVP

        beginTurnEvent.Raise();
    }

    public void EndTurn() {
        endTurnEvent.Raise();
        BeginTurn();
    }

    private void OnAttemptCast() {
        Card card = handPresenter.ZoomedCardPresenter().Model();

        if (card.Ability.ManaCost <= mana) {
            mana -= card.Ability.ManaCost;
            UpdateView();

            handPresenter.CastZoomedCard();
            Debug.Log("Casting card [" + card.Ability.AbilityName + "]");
        }
        else {
            Debug.Log("Not enough mana to cast card [" + card.Ability.AbilityName + "]");
        }
    }

    private void OnAttemptRitualize() {
        Card card = handPresenter.ZoomedCardPresenter().Model();

        if (card.Ability.ManaCost <= mana) {
            mana -= card.Ability.ManaCost;
            UpdateView();

            handPresenter.RitualizeZoomedCard();
            Debug.Log("Ritualizing card [" + card.Ability.AbilityName + "]");
        }
        else {
            Debug.Log("Not enough mana to ritualize card [" + card.Ability.AbilityName + "]");
        }
    }

    private void UpdateView() {
        manaText.text = mana.ToString() + "/3"; // TODO move to view
    }
}
