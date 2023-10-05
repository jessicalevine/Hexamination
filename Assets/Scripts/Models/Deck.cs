using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    public List<Ability> Decklist;
    public List<Ability> DrawPile;
    public List<Ability> DiscardPile;

    private void Start() {
        DrawPile = new List<Ability>();
        DiscardPile = new List<Ability>();
    }

    public void BeginEncounter() {
        Debug.Log("Preparing drawpile for encounter");
        for (int i = 0; i < Decklist.Count; i++) {
            DrawPile.Add(Decklist[i]);
        }
        Debug.Log("Drawpile now has [" + DrawPile.Count + "] abilities");
        Shuffle();
    }

    public void Shuffle() {
        // Add discard pile into draw pile
        for (int i = 0; i < DiscardPile.Count; i++) {
            DrawPile.Add(DiscardPile[i]);
        }
        DiscardPile = new List<Ability>();

        // Shuffle draw pile
        int count = DrawPile.Count;
        for (int i = 0; i < (count - 1); i++) {
            int r = i + UnityEngine.Random.Range(0, count - i);
            Ability a = DrawPile[r];
            DrawPile[r] = DrawPile[i];
            DrawPile[i] = a;
        }
    }

    public Card Draw() {
        // Shuffle if it's empty
        if (DrawPile.Count < 1) {
            if (DiscardPile.Count < 1) {
                Debug.LogError("Draw and discard piles are both empty!");
                return null;
            }
            else {
                Shuffle();
                if (DrawPile.Count < 1) {
                    Debug.LogError("No cards in deck after shuffle!");
                    return null;
                }
                else {
                    return Draw();
                }
            }
        }

        Ability drawnAbility = DrawPile[0];
        DrawPile.RemoveAt(0);

        return new Card(drawnAbility);
    }

    public void AddDiscard(Ability ability) {
        DiscardPile.Add(ability);
    }
}
