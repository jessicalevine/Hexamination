using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Opponent))]
public class OpponentPresenter : CharacterPresenter {
    [SerializeField] private GeneralEvent endTurnEvent;

    private Opponent opponentModel;

    private new void Start() {
        base.Start();

        opponentModel = GetComponent<Opponent>();

        if (endTurnEvent == null)
            Debug.LogError("No beginTurnEvent on OpponentPresenter!");
        else
            endTurnEvent.Action += OnEndTurn;
    }

    private void OnEndTurn() {
        opponentModel.UseAbility();
    }
}
