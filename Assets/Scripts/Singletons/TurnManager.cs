using System;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    private const int _maxMana = 3;
    public int mana;
    public Deck Deck;
    [SerializeField] private Opponent opponent;
    [SerializeField] private HandPresenter handPresenter;

    public GeneralEvent attemptCastEvent;
    public GeneralEvent attemptRitualizeEvent;

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
        handPresenter.Model().DrawFull();
    }

    public void EndTurn() {
        opponent.UseAbility(); // TODO go through presenter
        handPresenter.Model().DiscardHand();

        // TODO opponent attacks
        BeginTurn();
    }

    private void OnAttemptCast() {
        Card card = handPresenter.ZoomedCardPresenter().Model();

        if (card.Ability.ManaCost <= mana) {
            mana -= card.Ability.ManaCost;
            UpdateView();

            handPresenter.CastZoomedCard();
            Debug.Log("Playing card [" + card.Ability.AbilityName + "]");
        }
        else {
            Debug.Log("Not enough mana for card [" + card.Ability.AbilityName + "]");
        }
    }

    private void OnAttemptRitualize() {
        throw new NotImplementedException();
    }

    private void UpdateView() {
        manaText.text = mana.ToString() + "/3"; // TODO move to view
    }
}
