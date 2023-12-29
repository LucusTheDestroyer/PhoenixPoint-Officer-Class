using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;
using Base.Defs;
using PhoenixPoint.Geoscape.View.ViewControllers.BaseRecruits;
using PhoenixPoint.Geoscape;

namespace Officer.Harmony
{
    [HarmonyPatch(typeof(SpecializationSelectorController), "SetSpecializations", new Type[]{typeof(List<SpecializationDef>), typeof(List<int>), typeof(Sprite)})]
    public static class RemoveFromMutoid
    {
        public static readonly DefRepository Repo = ModHandler.Repo;
        public static void Prefix(List<SpecializationDef> specs, List<int> costs, Sprite resourceImage)
        {
            ResourcesUIDataDef ResUI = (ResourcesUIDataDef)Repo.GetDef("62f06399-56b4-3054-8b64-4938dd4cd329"); //"ResourcesUIDataDef"
            if(resourceImage == ResUI.GetViewForResource(ResourceType.Mutagen).Visual)
            {
                OfficerMain.Main.Logger.LogInfo($"Mutagen resource image means this is a Mutoid");
                for (int i=0; i < specs.Count; i++)
                {
                    if(specs[i].ClassTag == Misc.Tags.OfficerClassTag())
                    {
                        OfficerMain.Main.Logger.LogInfo($"Removing Officer!");
                        specs.RemoveAt(i);
                        costs.RemoveAt(i);
                    }
                }
            }
            
        }
    }
}