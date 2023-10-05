using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    public Deck Deck;
    public TurnManager TurnManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDebugButtonClick() {
        TurnManager.BeginEncounter();
        TurnManager.EndTurn();
    }
}
