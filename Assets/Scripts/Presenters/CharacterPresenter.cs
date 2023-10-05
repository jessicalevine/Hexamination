using UnityEngine;

[RequireComponent(typeof(Character), typeof(CharacterView))]
public class CharacterPresenter : MonoBehaviour {
    private Character model;
    private CharacterView view;

    void Start() {
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
