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

    public int InitialDamage;
    public int InitialDispel;
    public int InitialResources;

    public int RitualDamageImmediate;
    public int RitualDispelImmediate;

    public int RitualPerResourceDamageImmediate;
    public int RitualPerResourceDispelImmediate;

    public int RitualDamageRamp;
    public int RitualDispelRamp;
    public int RitualResourceRamp;

    public int PerResourceDamage;
    public int PerResourceDispel;
}
