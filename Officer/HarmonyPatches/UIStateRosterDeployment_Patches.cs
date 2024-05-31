using HarmonyLib;
using PhoenixPoint.Geoscape.Entities;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Base.Core;
using Base;
using PhoenixPoint.Geoscape.View.ViewStates;
using PhoenixPoint.Geoscape.View.ViewModules;
using Base.UI.MessageBox;
using Officer.Misc;

namespace Officer.Harmony 
{
    [HarmonyPatch(typeof(UIStateRosterDeployment), "CheckForDeployment", new Type[] {typeof(IEnumerable<GeoCharacter>)})]
    public static class PreventMissionLaunch_Patch
    {
        public static void Postfix(UIStateRosterDeployment __instance, IEnumerable<GeoCharacter> squad, MessageBox ____confirmationBox)
        {
            var propertyInfo = __instance.GetType().GetProperty("_missionBriefingModule", BindingFlags.NonPublic | BindingFlags.Instance);
            UIModuleDeploymentMissionBriefing property = propertyInfo.GetValue(__instance) as UIModuleDeploymentMissionBriefing;

            bool MultiOfficerFlag = squad.Count((GeoCharacter c) => c.ClassTags.Contains(Tags.OfficerClassTag())) >= 2;
            if(MultiOfficerFlag)
            {
                if(____confirmationBox == null)
                {
                    ____confirmationBox = GameUtl.GetMessageBox();
                }
                ____confirmationBox.ShowSimplePrompt("Only one Officer can be deployed into a mission", MessageBoxIcon.Information, MessageBoxButtons.OK, null, null, null);
                property.DeployButton.SetInteractable(false);
                property.DeployButton.ResetButtonAnimations();
            }
        }
    }
}