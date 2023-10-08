using UnityEngine;

[CreateAssetMenu]
public class OpponentAbility : ScriptableObject {
    public string AbilityName;
    public int MinDamage;
    public int MinDispel;

    public int MaxDamage;
    public int MaxDispel;
    // TODO Other effects
}
