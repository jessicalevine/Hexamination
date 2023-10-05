using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour {
    [SerializeField] private TMP_Text cardTitle;
    [SerializeField] private TMP_Text manaCost;
    [SerializeField] private TMP_Text cardDesc;

    void Start() {
        if (cardTitle == null)
            Debug.LogError("No cardTitle");
        if (manaCost == null)
            Debug.LogError("No manaCost");
        if (cardDesc == null)
            Debug.LogError("No cardDesc");
    }

    public void SetAll(string newTitle, int newManaCost, Ability ability) {
        cardTitle.text = newTitle;
        manaCost.text = newManaCost.ToString();
        cardDesc.text = "<line-height=75%>" + ability.CastDescription + "\n\n<b>Ritual:</b> " + ability.RitualDescription + "</line-height>";
    }

    public void SetPosition(Vector3 position) {
        transform.position = position;
    }
}
