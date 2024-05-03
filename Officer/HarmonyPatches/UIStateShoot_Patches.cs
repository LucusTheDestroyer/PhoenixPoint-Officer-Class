using HarmonyLib;
using Officer.NewDefs;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.View.ViewStates;
using System;
using System.Reflection;

namespace Officer.Harmony 
{
    [HarmonyPatch(typeof(UIStateShoot), "EnterState", new Type[]{})]
    public static class EnterState_Patch
    {
        public static ChangeMultiplierShootAbility LastAppliedMultiplier = null;
        public static void Prefix(UIStateShoot __instance)
        {
            FieldInfo info = AccessTools.Field(typeof(UIStateShoot), "_ability");
            TacticalAbility value = (TacticalAbility)info.GetValue(__instance);
            if(LastAppliedMultiplier != null)
            {
                LastAppliedMultiplier.RemoveModifier();
                LastAppliedMultiplier = null;
            }
            if(value is ChangeMultiplierShootAbility MultiplierAbility)
            { 
                MultiplierAbility.ApplyModifier();
                LastAppliedMultiplier = MultiplierAbility;
            }
        }
    }
}