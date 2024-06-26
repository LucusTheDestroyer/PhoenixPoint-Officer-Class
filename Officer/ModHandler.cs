using Base.Core;
using Base.Defs;
using Base.Entities.Statuses;
using HarmonyLib;
using Officer.Abilities;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using System.IO;
using UsefulMethods;

namespace Officer
{
    public static class ModHandler
    {
        internal static string LocalizationFileName = "OfficerMod.csv";
        internal static SharedDamageKeywordsDataDef keywords = GameUtl.GameComponent<SharedData>().SharedDamageKeywords;
		internal static readonly DefRepository Repo = GameUtl.GameComponent<DefRepository>();
        internal static OfficerConfig Config;
        public static void ApplyChanges(ModMain instance)
        {
            Config = instance.Config as OfficerConfig;
            Helper.Initialize(instance);
            if(File.Exists(Path.Combine(Helper.LocalizationDirectory, LocalizationFileName)))
            {
                Helper.AddLocalizationFromCSV(LocalizationFileName, null);
            }

            
            Misc.Poseidon90Ammo.UpdateP90Ammo();
            Misc.Poseidon90Ammo.UpdateItemWeaponDatabase();
            Misc.Poseidon90.AddToRiflesList();
            Misc.OfficerResearch.UpdatePXResearchDB();
            Misc.Tags.AddNecessaryTags();
            Misc.Tags.OfficerToSharedGameTags();
            UpdateExistingClasses();
            ImplementConfig();

            //Patch all Harmonies:
            HarmonyLib.Harmony harmony = (HarmonyLib.Harmony)instance.HarmonyInstance;
            harmony.PatchAll();
            instance.Logger.LogInfo("Officer class mod enabled.");
        }

        public static void ImplementConfig()
        {
            if(Config.OfficerSophia)
            {
                Misc.OfficerSophia.Implement();
            }
            else
            {
                Misc.OfficerSophia.Revert();
            }
            if(Config.DominantClass)
            {
                OfficerClassProficiency.GetOrCreate().IsDominantClass = true;
            }
            else
            {
                OfficerClassProficiency.GetOrCreate().IsDominantClass = false;
            }
            OfficerMain.Main.Logger.LogInfo("Config selection updated.");
        }

        public static void UpdateExistingClasses()
        {
            AbilityTrackDef AssaultTrack = (AbilityTrackDef)Repo.GetDef("eb88136e-e0dd-c3c2-16d5-a4c0b95d3a67"); //"E_AbilityTrack [AssaultSpecializationDef]"
            AssaultTrack.AbilitiesByLevel[5].Ability = CoolUnderPressure.GetOrCreate();
            AbilityTrackDef SniperTrack = (AbilityTrackDef)Repo.GetDef("a9b75670-ddec-5222-b4c3-08124bb8751e"); //"E_AbilityTrack [SniperSpecializationDef]"
            SniperTrack.AbilitiesByLevel[6].Ability = Deadeye.GetOrCreate();
            //Get BC QuickAim and add it to Deadeye disabling statuses in case TFTV is active:
            StatusDef BC_QAStatus = (StatusDef)Repo.GetDef("c60511db-2785-4932-8654-086adc8e9e1b"); //Status for "BC_QuickAim_AbilityDef"
            if(BC_QAStatus != null) //Status for "BC_QuickAim_AbilityDef"
            {
                Deadeye.GetOrCreate().DisablingStatuses = Deadeye.GetOrCreate().DisablingStatuses.AddToArray(BC_QAStatus);
            }
            //Ensure both Onslaught and Marked for Death maintain their changes with TFTV;
            Onslaught.GetAndUpdate();
            OfficerSpecialisation.MarkedForDeath();
        }
    }
}