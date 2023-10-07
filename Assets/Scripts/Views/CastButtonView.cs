using UnityEngine;

public class CastButtonView : MonoBehaviour {
    [SerializeField] private GeneralEvent attemptCastEvent;
    void Start() {
        if (attemptCastEvent == null)
            Debug.LogError("No attemptCastEvent on CastButtonView!");
    }

    public void OnCastClick() {
        attemptCastEvent.Raise();
    }
}
