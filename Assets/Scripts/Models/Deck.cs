using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Ability> Decklist;
    public SortedList<int, Ability> DrawPile;
    public List<Ability> DiscardPile;


    public static Deck Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
        Decklist = new List<Ability>();
        DrawPile = new SortedList<int, Ability>();
        DiscardPile = new List<Ability>();
    }

    public void BeginEncounter() {
        for (int i = 0; i < Decklist.Count; i++) {
            DrawPile[i] = Decklist[i];
        }
        Shuffle();
    }

    public void Shuffle() {
        int count = DrawPile.Count;
        for (int i = 0; i < (count - 1); i++) {
            int r = i + UnityEngine.Random.Range(0, count - i);
            Ability a = DrawPile[r];
            DrawPile[r] = DrawPile[i];
            DrawPile[i] = a;
        }
    }

    public Card Draw() {
        Ability drawnAbility = DrawPile[0];
        if (drawnAbility == null) {
            Shuffle();
            if (DrawPile[0] == null) {
                Debug.LogError("No cards in deck after shuffle");
                return null;
            }
            else {
                return Draw();
            }
        }

        for (int i = 0; i < DrawPile.Count - 1; i++) {
            DrawPile[i] = DrawPile[i + 1];
        }
        DrawPile[DrawPile.Count - 1] = null;

        //return new Card(drawnAbility);
        return null;
    }
}
