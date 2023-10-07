using UnityEngine;

public class RitualizeButtonView : MonoBehaviour {
    [SerializeField] private GeneralEvent attemptRitualizeEvent;
    void Start() {
        if (attemptRitualizeEvent == null)
            Debug.LogError("No attemptPlayEvent on CastButtonView!");
    }

    public void OnRitualizeClick() {
        attemptRitualizeEvent.Raise();
    }
}
