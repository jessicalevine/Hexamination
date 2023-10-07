using UnityEngine;

public class HandView : MonoBehaviour {
    public const string CARD_TAG = "Card";
    [SerializeField] private CardEvent requestToggleZoomCardEvent;

    void Start() {
        if (requestToggleZoomCardEvent == null)
            Debug.LogError("No requestToggleZoomCardEvent on HandView!");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If you click a card, raise an event requesting to toggle the card's zoom
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit) {
                GameObject hitObj = hit.collider.gameObject;
                if (hitObj.tag == CARD_TAG) {
                    CardPresenter cardPresenter = hitObj.GetComponent<CardPresenter>();
                    if (cardPresenter == null) {
                        Debug.LogError("Object tagged [" + CARD_TAG + "] does not have a CardPresenter during click");
                        return;
                    }

                    requestToggleZoomCardEvent.Raise(cardPresenter);
                }
            }
        }
    }
}
