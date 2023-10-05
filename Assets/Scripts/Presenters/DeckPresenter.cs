using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Deck))]
public class DeckPresenter : MonoBehaviour
{
    // [SerializeField] private HandView view;
    [SerializeField] private Deck model;

    private void Start() {
        model = GetComponent<Deck>();

    }
}
