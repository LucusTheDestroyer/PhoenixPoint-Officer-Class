using HarmonyLib;
using Officer.Abilities;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using System;
using System.Reflection;

namespace Officer.Harmony 
{
    [HarmonyPatch(typeof(RecoverWillAbility), "GetDisabledStateInternal", new Type[]{typeof(IgnoredAbilityDisabledStatesFilter)})]
    public static class DisabledState_Patch
    {
        public static void Postfix(RecoverWillAbility __instance, IgnoredAbilityDisabledStatesFilter filter, ref AbilityDisabledState __result)
        {
            MethodInfo baseMethod = typeof(TacticalAbility).GetMethod("GetDisabledStateDefaults", BindingFlags.NonPublic | BindingFlags.Instance);
            if (baseMethod != null)
            {
                if(__result == AbilityDisabledState.NoWillpowerRecover)
                {
                    if(__instance.TacticalActor.HasStatus(ResilienceTraining.OnRecoverActivatedStatus()))
                    {
                        IgnoredAbilityDisabledStatesFilter filter2 = new IgnoredAbilityDisabledStatesFilter(filter, new AbilityDisabledState[]
                        {
                            AbilityDisabledState.OffMap,
                        });
                        __result = (AbilityDisabledState)baseMethod.Invoke(__instance as TacticalAbility, new object[] {filter2} );
                    }
                }
            }
        }
    }
}