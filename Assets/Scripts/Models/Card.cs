using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card //: MonoBehaviour
{
    public Ability Ability { get; internal set; }
    public int Damage;
    public int Dispel;
    public int Resources;

    public event Action Discarded;

    public Card(Ability ability) {
        Ability = ability;
        Damage = ability.InitialDamage;
        Dispel = ability.InitialDispel;
        Resources = ability.InitialResources;
    }

    public void Discard() {
        Discarded.Invoke();
    }
}
