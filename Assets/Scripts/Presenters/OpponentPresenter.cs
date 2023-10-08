using UnityEngine;

[RequireComponent(typeof(Opponent), typeof(OpponentView))]
public class OpponentPresenter : CharacterPresenter {
    [SerializeField] private GeneralEvent beginTurnEvent;
    [SerializeField] private GeneralEvent endTurnEvent;

    private Opponent opponentModel;
    private OpponentView opponentView;


    private new void Start() {
        base.Start();

        opponentModel = GetComponent<Opponent>();
        opponentView = GetComponent<OpponentView>();

        if (beginTurnEvent == null)
            Debug.LogError("No beginTurnEvent on OpponentPresenter!");
        else
            beginTurnEvent.Action += OnBeginTurn;

        if (endTurnEvent == null)
            Debug.LogError("No endTurnEvent on OpponentPresenter!");
        else
            endTurnEvent.Action += OnEndTurn;

    }
    private void OnBeginTurn() {
        opponentView.SetIntent(opponentModel.PeekIntent());
    }

    private void OnEndTurn() {
        base.model.SetDispel(0);
        opponentModel.UseAbility();
    }
}
