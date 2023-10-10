using System;
using TMPro;
using UnityEngine;


public class CardView : MonoBehaviour {
    private const float scaleZoom = 1.2f;
    private const float scaleRegular = 0.8f;
    const float zoomX = -0.15f;
    const float zoomY = 2.07f;

    [SerializeField] private TMP_Text cardTitle;
    [SerializeField] private TMP_Text manaCost;
    [SerializeField] private TMP_Text cardDesc;
    [SerializeField] private GameObject ritualText;
    [SerializeField] private GameObject ritualCountText;
    [SerializeField] private SpriteRenderer cardArtSpriteRenderer;

    private bool zoomed = false;
    private Vector3 lastPosition;

    void Start() {
        if (cardTitle == null)
            Debug.LogError("No cardTitle");
        if (manaCost == null)
            Debug.LogError("No manaCost");
        if (cardDesc == null)
            Debug.LogError("No cardDesc");

        if (cardArtSpriteRenderer == null)
            Debug.LogError("No cardArtSpriteRenderer");

        if (ritualText == null)
            Debug.LogError("No ritualText");
        if (ritualCountText == null)
            Debug.LogError("No ritualCountText");
    }

    public void SetAll(string newTitle, int newManaCost, string newCardDesc, Texture2D newCardArt) {
        cardTitle.text = newTitle;
        manaCost.text = newManaCost.ToString();
        cardDesc.text = newCardDesc;

        Vector2 centerOfSpriteBounds = new Vector2(0.5f, 0.5f);
        cardArtSpriteRenderer.sprite = Sprite.Create(newCardArt, new Rect(0, 0, newCardArt.width, newCardArt.height), centerOfSpriteBounds);
    }

    public void SetPosition(Vector3 position) {
        transform.position = position;
    }

    public bool IsZoomed() {
        return zoomed;
    }

    public void Zoom() {
        Vector3 oldPosition = transform.position;
        lastPosition = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z);

        transform.position = new Vector3(zoomX, zoomY, 0);
        transform.localScale = new Vector3(scaleZoom, scaleZoom, scaleZoom);
    }

    public void Unzoom() {
        if (lastPosition != null)
            transform.position = lastPosition;
        else
            Debug.LogError("Tried to unzoom a card without a lastPosition. Was it never zoomed?");
        transform.localScale = new Vector3(scaleRegular, scaleRegular, scaleRegular);
    }

    public void UpdateRitualText(bool ritualizedThisTurn) {
        ritualText.SetActive(ritualizedThisTurn);
    }

    public void UpdateRitualCount(int ritualCount) {
        if (ritualCountText.activeSelf != true) {
            ritualCountText.SetActive(true);
        }

        ritualCountText.GetComponent<TMP_Text>().text = "RITUALS: " + ritualCount;
    }
}
