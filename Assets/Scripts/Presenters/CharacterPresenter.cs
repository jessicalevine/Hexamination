using UnityEngine;

[RequireComponent(typeof(Character), typeof(CharacterView))]
public class CharacterPresenter : MonoBehaviour {
    protected Character model;
    protected CharacterView view;

    public void Start() {
        model = GetComponent<Character>();
        view = GetComponent<CharacterView>();

        model.HealthChanged += OnHealthChanged;
        OnHealthChanged();
    }

    private void OnDestroy() {
        model.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged() {
        view.SetHealthUI(model.CurrentHealth);
    }
}
