using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card //: MonoBehaviour
{
    public Ability Ability { get; internal set; }

    public Card(Ability ability) {
        Ability = ability;
    }
}
