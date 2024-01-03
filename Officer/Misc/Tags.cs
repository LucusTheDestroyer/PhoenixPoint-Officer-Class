using System.Linq;
using Base.Core;
using Base.Defs;
using HarmonyLib;
using Officer.Abilities;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsSharedData;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Tactical.Entities.Weapons;

namespace Officer.Misc
{
    public static class Tags
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        
        public static ClassTagDef OfficerClassTag()
        {
            ClassTagDef OfficerTag = (ClassTagDef)Repo.GetDef("637a5db1-ba13-4cfd-a988-c332878bb36c");
            if (OfficerTag == null)
            {
                OfficerTag = Repo.CreateDef<ClassTagDef>("637a5db1-ba13-4cfd-a988-c332878bb36c");
                OfficerTag.name = "Officer_ClassTagDef";
                OfficerTag.ResourcePath = "Defs/GameTags/Classes/Officer_ClassTagDef";
            }
            return OfficerTag;
        }

        public static void OfficerTagOnWeapons()
        {
            GameTagDef HandgunTag = (GameTagDef)Repo.GetDef("7a8a0a76-deb6-c004-3b5b-712eae0ad4a5"); //"HandgunItem_TagDef"
            GameTagDef PDWTag = (GameTagDef)Repo.GetDef("87b91929-c816-97d4-4877-20b00fdf37b3"); //"PDWItem_TagDef"
			foreach (WeaponDef weapon in Repo.GetAllDefs<WeaponDef>().Where(w => w.Tags.Contains(HandgunTag) || w.Tags.Contains(PDWTag))) 
            {
                weapon.Tags.Add(OfficerClassTag());
				if (weapon.CompatibleAmmunition.Count() > 0) 
                {
                    weapon.CompatibleAmmunition[0].Tags.Add(OfficerClassTag());
				}
			}	
        }

        public static void OfficerToSharedGameTags()
        {
            // "SharedGameTagsDataDef"
            SharedGameTagsDataDef SharedGameTags = GameUtl.GameComponent<SharedData>().SharedGameTags;
            SharedGameTags.Specializations = SharedGameTags.Specializations.AddToArray(OfficerSpecialisation.GetOrCreate());
        }
    }
}