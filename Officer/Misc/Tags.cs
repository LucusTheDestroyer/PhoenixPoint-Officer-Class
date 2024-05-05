using System.Collections.Generic;
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

        public static SkillTagDef RecoverSkillTag()
        {
            SkillTagDef RecoverTag = (SkillTagDef)Repo.GetDef("07c9d32d-4b69-4598-b483-2f4f98d73a2d");
            if (RecoverTag == null)
            {
                RecoverTag = Repo.CreateDef<SkillTagDef>("07c9d32d-4b69-4598-b483-2f4f98d73a2d");
                RecoverTag.name = "Recover_SkillTagDef";
                RecoverTag.ResourcePath = "Defs/GameTags/Skills/Recover_SkillTagDef";
            }
            return RecoverTag;
        }

        public static GameTagDef SingleShotWeaponTag()
        {
            GameTagDef SingleShotTag = (GameTagDef)Repo.GetDef("9b1ba9c9-3c20-4379-acba-ea0a78217da3");
            if (SingleShotTag == null)
            {
                SingleShotTag = Repo.CreateDef<GameTagDef>("9b1ba9c9-3c20-4379-acba-ea0a78217da3");
                SingleShotTag.name = "SingleShotWeapon_GameTagDef";
                SingleShotTag.ResourcePath = "Defs/GameTags/SingleShotWeapon_GameTagDef";
            }
            return SingleShotTag;
        }

        public static void AddNecessaryTags()
        {
            ItemTypeTagDef HandgunTag = (ItemTypeTagDef)Repo.GetDef("7a8a0a76-deb6-c004-3b5b-712eae0ad4a5"); //"HandgunItem_TagDef"
            ItemTypeTagDef PDWTag = (ItemTypeTagDef)Repo.GetDef("87b91929-c816-97d4-4877-20b00fdf37b3"); //"PDWItem_TagDef"
            GameTagDef GunWeaponTag = (GameTagDef)Repo.GetDef("251a376f-e4e1-04c4-f9ec-10ba205b1ebe"); //"GunWeapon_TagDef"
            GameTagDef SingleShotTag = SingleShotWeaponTag();
            foreach(WeaponDef weapon in Repo.GetAllDefs<WeaponDef>())
            {
                if(weapon.Tags.Contains(HandgunTag) || weapon.Tags.Contains(PDWTag))
                {
                    if(!weapon.Tags.Contains(OfficerClassTag()))
                    {
                        weapon.Tags.Add(OfficerClassTag());
				        if (weapon.CompatibleAmmunition.Count() > 0) 
                        {
                            weapon.CompatibleAmmunition[0].Tags.Add(OfficerClassTag());
				        }
                    }
                }
                if(weapon.Tags.Contains(GunWeaponTag) && weapon.DamagePayload.ProjectilesPerShot == 1 && weapon.DamagePayload.AutoFireShotCount == 1)
                {
                    if(!weapon.Tags.Contains(SingleShotTag))
                    {
                        weapon.Tags.Add(SingleShotTag);
                    }
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