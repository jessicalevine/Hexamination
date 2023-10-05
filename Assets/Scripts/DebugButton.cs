using UnityEngine;

public class DebugButton : MonoBehaviour {
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private Player player;

    public void OnBeginEncounterClick() {
        turnManager.BeginEncounter();
        Destroy(this.gameObject);
    }

    public void OnEndTurnClick() {
        turnManager.EndTurn();
    }

    public void OnDamageSelfClick() {
        player.DecreaseHealth(10);
    }
}
