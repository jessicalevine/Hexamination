using System;
using UnityEngine;

[RequireComponent(typeof(CardView))]
public class CardPresenter : MonoBehaviour {
    private Card model;
    private CardView view;
    public AnimationManager animationManager;

    [SerializeField] private CharacterDamageEvent opponentDamageEvent;
    [SerializeField] private CharacterIncreaseDispelEvent playerIncreaseDispelEvent;
    [SerializeField] private CardEvent attemptPlayCardEvent;
    [SerializeField] private GeneralEvent beginTurnEvent;

    [SerializeField] private GameObject cardVisualsGameObject;

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
        if (beginTurnEvent == null)
            Debug.LogError("No beginTurnEvent on CardPresenter!");
        else
            beginTurnEvent.Action += OnBeginTurn;

        if (animationManager == null)
            animationManager = new AnimationManager(gameObject);
    }

    private void Update() {
        animationManager.WhenUpdate();
    }

    private void OnDestroy() {
        if (model != null) {
            model.Discarded -= OnDiscarded;
        }
        if (animationManager.IsAnimating()) {
            animationManager.CompleteAnimation();
        }
    }

    public void Setup(Card card) {
        view = GetComponent<CardView>();

        model = card;
        model.RegisterListeners(opponentDamageEvent, playerIncreaseDispelEvent);

        animationManager = new AnimationManager(gameObject);

        model.CardUpdated += OnCardUpdate;

        OnCardUpdate();
    }

    public void SetupAnimation(Vector3 location, bool activate = false) {
        animationManager.Setup(location, activate ? cardVisualsGameObject : null); ;
    }

    public void Ritualize() {
        model.Ritualize();
        view.UpdateRitualText(true);
        view.Unzoom();
    }

    private void OnDiscarded() {
        Destroy(this.gameObject);
    }

    private void OnCardUpdate() {
        Ability a = model.Ability;
        Debug.Log("Card [" + a.AbilityName + "] updated");
        view.SetAll(a.AbilityName, a.ManaCost, CardStringFormatter.FormatCardDescription(model), a.CardArt);
    }

    private void OnBeginTurn() {
        if (model.ritualizedThisTurn) {
            model.AdvanceRitual();
            model.ritualizedThisTurn = false;
            view.UpdateRitualText(false);
            view.UpdateRitualCount(model.RitualCount());
        }
    }

    public bool IsRitualizedThisTurn() {
        return model.ritualizedThisTurn;
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

    public void Cast() {
        model.Cast();
    }
}
