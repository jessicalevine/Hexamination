using System;
using TMPro;
using UnityEngine;


public class CardView : MonoBehaviour {
    private const float scaleZoom = 1.2f;
    private const float scaleRegular = 0.8f;
    const float zoomX = -0.15f;
    const float zoomY = 2.07f;

    public float highlightScaleFactor = 1.2f;  // The scale factor when highlighted
    private Vector3 originalScale;  // The original scale of the object

    [SerializeField] private TMP_Text cardTitle;
    [SerializeField] private TMP_Text manaCost;
    [SerializeField] private TMP_Text cardDesc;
    [SerializeField] private GameObject ritualText;
    [SerializeField] private GameObject ritualCountText;
    [SerializeField] private GameObject ritualImage;
    [SerializeField] private GameObject hoverImage;

    private bool zoomed = false;
    private bool isZoomed = false;
    private Vector3 lastPosition;

    void Start() {
        if (cardTitle == null)
            Debug.LogError("No cardTitle");
        if (manaCost == null)
            Debug.LogError("No manaCost");
        if (cardDesc == null)
            Debug.LogError("No cardDesc");

        if (ritualText == null)
            Debug.LogError("No ritualText");
        if (ritualCountText == null)
            Debug.LogError("No ritualCountText");

        originalScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        if (isZoomed != true)
        {
            // Scale the object when highlighted
            transform.localScale = originalScale * highlightScaleFactor;
            hoverImage.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (isZoomed != true)
        {
            // Restore the original scale when the highlight is removed
            transform.localScale = originalScale;
            hoverImage.SetActive(false);
        } 
    }

    public void SetAll(string newTitle, int newManaCost, string newCardDesc) {
        cardTitle.text = newTitle;
        manaCost.text = newManaCost.ToString();
        cardDesc.text = newCardDesc;
    }

    public void SetPosition(Vector3 position) {
        transform.position = position;
    }

    public bool IsZoomed() {
        return zoomed;
    }

    public void Zoom() {
        hoverImage.SetActive(false);
        Vector3 oldPosition = transform.position;
        lastPosition = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z);

        transform.position = new Vector3(zoomX, zoomY, 0);
        transform.localScale = new Vector3(scaleZoom, scaleZoom, scaleZoom);
        isZoomed = true;
    }

    public void Unzoom() {
        if (lastPosition != null)
            transform.position = lastPosition;
        else
            Debug.LogError("Tried to unzoom a card without a lastPosition. Was it never zoomed?");
        transform.localScale = new Vector3(scaleRegular, scaleRegular, scaleRegular);
        isZoomed = false;
    }

    public void UpdateRitualText(bool ritualizedThisTurn) {
        ritualText.SetActive(ritualizedThisTurn);
        ritualImage.SetActive(ritualizedThisTurn);
    }

    public void UpdateRitualCount(int ritualCount) {
        if (ritualCountText.activeSelf != true) {
            ritualCountText.SetActive(true);
        }

        if (ritualImage.activeSelf != true)
        {
            ritualImage.SetActive(true);
        }

        ritualCountText.GetComponent<TMP_Text>().text = "Rituals: " + ritualCount;
    }
}
