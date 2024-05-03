using HarmonyLib;
using PhoenixPoint.Common.View.ViewControllers;
using PhoenixPoint.Geoscape.View.ViewControllers;
using PhoenixPoint.Geoscape.View.ViewControllers.Modal;
using System;
using System.Linq;

namespace Officer.Harmony 
{
    [HarmonyPatch(typeof(SelectSpecializationDataBind), "ModalShowHandler", new Type[]{typeof(UIModal)})]
    public static class ModalShowHandler_Patch
    {
        public static void Prefix(SelectSpecializationDataBind __instance, UIModal modal)
        {
            SpecializationOptionElementController source = __instance.DualClassButtonContainer.GetComponentInChildren<SpecializationOptionElementController>(true);
            if (modal.Data is SelectSpecializationDataBind.Data specData)
            {
                if(specData.AvailableSpecs.Count() > __instance.DualClassButtonContainer.childCount)
                {
                    OfficerMain.Main.Logger.LogInfo($"Number of children in DualClassButtonContainer ({__instance.DualClassButtonContainer.childCount}) is less than Available classes ({specData.AvailableSpecs.Count()}). Adding more.");
                    int num = specData.AvailableSpecs.Count() - __instance.DualClassButtonContainer.childCount;
                    for(int i = 0; i < num; i++)
                    {
                        SpecializationOptionElementController newElement = UnityEngine.GameObject.Instantiate(source, __instance.DualClassButtonContainer, true);
                    }
                }
            }
        }
    }
}