using Base.Defs;
using Base.Entities.Abilities;
using Base.UI;
using Officer.Misc;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class OfficerClassProficiency
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        public static ClassProficiencyAbilityDef GetOrCreate()
        {
            ClassProficiencyAbilityDef OfficerProficiency = (ClassProficiencyAbilityDef)Repo.GetDef("8f15bc0b-7f21-43fd-97e6-761ce151c3e6");
            if (OfficerProficiency == null)
            {
                //"Sniper_ClassProficiency_AbilityDef"
                ClassProficiencyAbilityDef SniperProficiency = (ClassProficiencyAbilityDef)Repo.GetDef("54328f21-e01a-4364-0aa7-4507affd2ccf");

                OfficerProficiency = Repo.CreateDef<ClassProficiencyAbilityDef>("8f15bc0b-7f21-43fd-97e6-761ce151c3e6", SniperProficiency);
                OfficerProficiency.CharacterProgressionData = Repo.CreateDef<AbilityCharacterProgressionDef>("e73227ed-1d8d-41a1-b82f-290d138e1db0", SniperProficiency.CharacterProgressionData);
                OfficerProficiency.CharacterProgressionData.name = "E_CharacterProgressionData [Officer_ClassProficiency_AbilityDef]";
                OfficerProficiency.ViewElementDef = ProficiencyVED(SniperProficiency.ViewElementDef);
                OfficerProficiency.ClassTags = new GameTagsList
                {
                    NewTags.OfficerClassTag(),
                    (ItemTypeTagDef)Repo.GetDef("7a8a0a76-deb6-c004-3b5b-712eae0ad4a5") //"HandgunItem_TagDef"
                };
                OfficerProficiency.AbilityDefs = new AbilityDef[] 
                {
                    BonusWillPower(),
                };
            }
            return OfficerProficiency;
        }

        private static TacticalAbilityViewElementDef ProficiencyVED(TacticalAbilityViewElementDef template)
        {
            TacticalAbilityViewElementDef VED = (TacticalAbilityViewElementDef)Repo.GetDef("3f8f0272-fafd-4bd3-90cb-4be35c381025");
            if (VED == null)
            {
                VED = Repo.CreateDef<TacticalAbilityViewElementDef>("3f8f0272-fafd-4bd3-90cb-4be35c381025", template);
                VED.name = "E_ViewElement [Officer_ClassProficiency_AbilityDef]";
                VED.Name = "OfficerProficiency";
                VED.DisplayName1 = new LocalizedTextBind("OFFICER_PROFICIENCY_NAME");
                VED.DisplayName2 = new LocalizedTextBind("CLASS_OFFICER_NAME");
                VED.Description = new LocalizedTextBind("OFFICER_PROFICIENCY_DESCRIPTION");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("OfficerIcon_NoOutline.png");
            }
            return VED;
        }

        private static PassiveModifierAbilityDef BonusWillPower()
        {
            PassiveModifierAbilityDef Bonus = (PassiveModifierAbilityDef)Repo.GetDef("e39bc789-21fc-4788-9b53-11677e06cfa7");
            if (Bonus == null)
            {
                //"Devoted_AbilityDef"
                PassiveModifierAbilityDef Devoted = (PassiveModifierAbilityDef)Repo.GetDef("52dde58b-5782-f804-38fe-78e7353941b2");

                Bonus = Repo.CreateDef<PassiveModifierAbilityDef>("e39bc789-21fc-4788-9b53-11677e06cfa7", Devoted);
                Bonus.name = "E_PassiveModifier [Officer_ClassProficiency_AbilityDef]";
                Bonus.CharacterProgressionData = null;
                Bonus.ViewElementDef = null;
                Bonus.StatModifications[0].Value = 5f;
            }
            return Bonus;
        }
    }
}