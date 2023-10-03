using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    public string AbilityName;
    public int ManaCost;
    public string CastDescription;
    public string RitualDescription;

    public int Damage;
    public int Dispel;
}
