using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    private const int _maxMana = 3;
    public int mana;
    public Hand Hand;
    public Deck Deck;
    [SerializeField] private Opponent opponent;

    public CardEvent attemptPlayCardEvent;

    // move to view
    public TMP_Text manaText;


    void Start() {
        if (attemptPlayCardEvent == null)
            Debug.LogError("No attemptPlayCardEvent on TurnManager!");
        else
            attemptPlayCardEvent.Action += OnAttemptPlayCard;

        if (manaText == null)
            Debug.LogError("No manaText on TurnManager!");
    }

    private void OnDestroy() {
        attemptPlayCardEvent.Action -= OnAttemptPlayCard;
    }

    public void BeginEncounter() {
        Deck.BeginEncounter();
        BeginTurn();
    }

    public void BeginTurn() {
        mana = _maxMana;
        UpdateView();
        Hand.DrawFull();
    }

    public void EndTurn() {
        opponent.UseAbility(); // TODO go through presenter
        Hand.DiscardHand();

        // TODO opponent attacks
        BeginTurn();
    }

    private void OnAttemptPlayCard(Card card) {
        if (card.Ability.ManaCost <= mana) {
            mana -= card.Ability.ManaCost;
            UpdateView();
            card.Play();
            Debug.Log("Playing card [" + card.Ability.AbilityName + "]");
        }
        else {
            Debug.Log("Not enough mana for card [" + card.Ability.AbilityName + "]");
        }
    }

    private void UpdateView() {
        manaText.text = mana.ToString() + "/3"; // TODO move to view
    }
}
