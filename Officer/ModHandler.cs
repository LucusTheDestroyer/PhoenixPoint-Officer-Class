using PhoenixPoint.Modding;
using Officer;
using System.IO;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using Base.Core;
using PhoenixPoint.Common.Core;
using Base.Defs;
using UsefulMethods;

namespace Officer
{
    public static class ModHandler
    {
        internal static string LocalizationFileName = "OfficerMod.csv";
        internal static SharedDamageKeywordsDataDef keywords = GameUtl.GameComponent<SharedData>().SharedDamageKeywords;
		internal static readonly DefRepository Repo = GameUtl.GameComponent<DefRepository>();
        public static void ApplyChanges(ModMain instance)
        {
            Helper.Initialize(instance);
            //Get Path for Localisation;
            string LocalizationDirectory = Path.Combine(instance.Instance.Entry.Directory, "Assets", "Localization");
            if(File.Exists(Path.Combine(LocalizationDirectory, LocalizationFileName)))
            {
                Helper.AddLocalizationFromCSV(LocalizationFileName, null);
            }

            //Patch all Harmonies:
            HarmonyLib.Harmony harmony = (HarmonyLib.Harmony)instance.HarmonyInstance;
            harmony.PatchAll();
        }
    }
}