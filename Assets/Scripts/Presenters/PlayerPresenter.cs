using UnityEngine;

public class PlayerPresenter : CharacterPresenter {
    [SerializeField] private GameEvent playerDamageEvent;

    public new void Start() {
        base.Start();

        if (playerDamageEvent == null)
            Debug.LogError("No player damage event on PlayerPresenter!");
        else
            playerDamageEvent.Action += OnPlayerDamaged;
    }

    private void OnDestroy() {
        playerDamageEvent.Action -= OnPlayerDamaged;
    }

    private void OnPlayerDamaged(int damage) {
        model.DecreaseHealth(damage);
    }
}
