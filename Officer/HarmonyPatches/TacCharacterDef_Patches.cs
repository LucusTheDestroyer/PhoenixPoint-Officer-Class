using HarmonyLib;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Tactical.Entities;
using System.Collections.Generic;

namespace Officer.Harmony 
{
    [HarmonyPatch(typeof(TacCharacterDef), "ClassTags", MethodType.Getter)]
    public static class ClassTags_Patch
    {
        public static readonly ClassTagDef OfficerTag = Misc.Tags.OfficerClassTag();
        public static void Postfix(TacCharacterDef __instance, ref List<ClassTagDef> ____classTags)
        {
            if(__instance.name.Contains("Sophia"))
            {
                if(ModHandler.Config.OfficerSophia)
                {
                    if(!____classTags.Contains(OfficerTag))
                    {
                        ____classTags.Add(OfficerTag);
                    }
                }
                else
                {
                    if(____classTags.Contains(OfficerTag))
                    {
                        ____classTags.Remove(OfficerTag);
                    }
                }
            }
        }
    }
}