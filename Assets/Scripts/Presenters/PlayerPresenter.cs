using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerView))]
public class PlayerPresenter : CharacterPresenter {
    [SerializeField] private GeneralEvent beginTurnEvent;

    private new void Start() {
        base.Start();

        if (beginTurnEvent == null)
            Debug.LogError("No beginTurnEvent on PlayerPresenter!");
        else
            beginTurnEvent.Action += OnBeginTurn;
    }

    private void OnBeginTurn() {
        base.model.SetDispel(0);
    }
}
