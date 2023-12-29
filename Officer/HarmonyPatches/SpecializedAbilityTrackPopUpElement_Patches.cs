using HarmonyLib;
using PhoenixPoint.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using PhoenixPoint.Geoscape.View.ViewControllers;
using System.Reflection.Emit;

namespace Officer.Harmony
{
    [HarmonyPatch(typeof(SpecializedAbilityTrackPopupElement), "Init")]
    class Init_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> listInstructions = new List<CodeInstruction>(instructions);
            IEnumerable<CodeInstruction> insert = new List<CodeInstruction>
            {
                new CodeInstruction(OpCodes.Ldloc_S, 6),
                new CodeInstruction(OpCodes.Call, typeof(Init_Patch).GetMethod("RemoveOfficer"))
            };

            for (int i=0; i < instructions.Count(); i++)
            {
                if(listInstructions[i].opcode == OpCodes.Pop)
                {
                    listInstructions.InsertRange(i+1, insert);
                    return listInstructions;
                }
            }
            return instructions;
        }

        public static void RemoveOfficer(List<SpecializationDef> list)
        {
            SpecializationDef specializationDef = list.SingleOrDefault((SpecializationDef spec) => spec.ClassTag == Misc.Tags.OfficerClassTag());
            if(specializationDef != null)
            {
                list.Remove(specializationDef);
            }
        }
    }
}