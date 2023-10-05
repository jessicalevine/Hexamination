using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    public TurnManager TurnManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBeginEncounterClick() {
        TurnManager.BeginEncounter();
    }

    public void OnEndTurnClick() {
        TurnManager.EndTurn();
    }
}
