using System;
using UnityEngine;

public class CardStringFormatter : MonoBehaviour {
    private const string RITUAL_WRAP = "<color=\"purple\">{0}</color>";

    public static string FormatCardDescription(Card card) {
        Ability ability = card.Ability;

        string dmgStr = card.DamageDirty ? String.Format(RITUAL_WRAP, card.Damage().ToString()) : card.Damage().ToString();
        string parenDmgStr = card.DamageDirty ? String.Format(RITUAL_WRAP, "(" + card.Damage().ToString() + ")") : "(" + card.Damage().ToString() + ")";
        string dspStr = card.DispelDirty ? String.Format(RITUAL_WRAP, card.Dispel().ToString()) : card.Dispel().ToString();
        string resStr = card.ResourcesDirty ? String.Format(RITUAL_WRAP, card.Resources().ToString()) : card.Resources().ToString();

        string castDesc = ability.CastDescription;
        castDesc = castDesc.Replace("%DMG%", dmgStr);
        castDesc = castDesc.Replace("%PAREN_DMG%", parenDmgStr);
        castDesc = castDesc.Replace("%DSP%", dspStr);
        castDesc = castDesc.Replace("%RES%", resStr);

        string ritualDesc = ability.RitualDescription;
        ritualDesc = ritualDesc.Replace("%PAREN_DMG_IMM%", card.ResourcesDirty ? String.Format(RITUAL_WRAP, "(" + card.RitualImmediateDamage().ToString() + ")") : "(" + card.RitualImmediateDamage().ToString() + ")");
        ritualDesc = ritualDesc.Replace("%PAREN_DSP_IMM%", card.ResourcesDirty ? String.Format(RITUAL_WRAP, "(" + card.RitualImmediateDispel().ToString() + ")") : "(" + card.RitualImmediateDispel().ToString() + ")");

        return "<line-height=75%>" + castDesc + "\n\n<b>Ritual:</b> " + ritualDesc + "</line-height>";
    }
}
