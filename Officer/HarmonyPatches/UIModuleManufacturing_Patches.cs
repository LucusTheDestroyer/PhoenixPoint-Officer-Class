using HarmonyLib;
using PhoenixPoint.Geoscape.View.ViewModules;
using System;

namespace Officer.Harmony 
{
    [HarmonyPatch(typeof(UIModuleManufacturing), "SetupClassFilter", new Type[]{})]
    public static class SetupClassFilter_Patch
    {
        public static void Prefix(UIModuleManufacturing __instance)
        {
            __instance.ClassFilterPrefab = __instance.ClassFilterContainer.GetComponentInChildren<ToggleButton>(true);
        }
    }
}