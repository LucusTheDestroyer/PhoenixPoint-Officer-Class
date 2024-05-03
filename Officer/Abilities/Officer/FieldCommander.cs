using Base.Defs;
using Base.Entities.Abilities;
using Base.UI;
using Officer.NewDefs;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class FieldCommander
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        private static readonly ApplyStatusAbilityDef BloodLust = (ApplyStatusAbilityDef)Repo.GetDef("dfe93630-87f7-2774-1bc5-169deb082f7b"); //"BloodLust_AbilityDef"

        public static ApplyStatusAbilityDef GetOrCreate()
        {
            ApplyStatusAbilityDef FC = (ApplyStatusAbilityDef)Repo.GetDef("d64a0706-0867-4138-9632-9f0ea082bfdc");
            if (FC == null)
            {
                FC = Repo.CreateDef<ApplyStatusAbilityDef>("d64a0706-0867-4138-9632-9f0ea082bfdc", BloodLust);
                FC.name = "FieldCommander_AbilityDef";
                FC.CharacterProgressionData = FCProgression();
                FC.ViewElementDef = FCVED();
                FC.StatusDef = FCStatus();
            }
            return FC;
        }

        private static EditBattleSummaryRewardsStatusDef FCStatus()
        {
            EditBattleSummaryRewardsStatusDef status = (EditBattleSummaryRewardsStatusDef)Repo.GetDef("c7b58f8d-cc50-40b4-9026-dbef5675221b");
            if(status == null)
            {
                status = Repo.CreateDef<EditBattleSummaryRewardsStatusDef>("c7b58f8d-cc50-40b4-9026-dbef5675221b");
                Helper.CopyFieldsByReflection(BloodLust.StatusDef, status);
                status.name = "E_Status [FieldCommander_AbilityDef]";
                status.Visuals = FCVED();
                status.Experience = 50;
                status.SkillPoints = 1;
            }
            return status;
        }

        private static AbilityCharacterProgressionDef FCProgression()
        {
            AbilityCharacterProgressionDef Progression = (AbilityCharacterProgressionDef)Repo.GetDef("4a2ec0cb-4f45-4ad9-8696-1b261d007fd8");
            if (Progression == null)
            {
                Progression = Repo.CreateDef<AbilityCharacterProgressionDef>("4a2ec0cb-4f45-4ad9-8696-1b261d007fd8");
                Progression.name = "E_CharacterProgressionData [FieldCommander_AbilityDef]";
                Progression.RequiredStrength = 0;
                Progression.RequiredWill = 0;
                Progression.RequiredSpeed = 0;
                Progression.SkillPointCost = 30;
                Progression.MutagenCost = 30;
                Progression.PersonalTrackTags = new GameTagDef[]{};
            }
            return Progression;
        }

        private static TacticalAbilityViewElementDef FCVED()
        {
            TacticalAbilityViewElementDef VED = (TacticalAbilityViewElementDef)Repo.GetDef("9a4fd241-a9ea-4ce0-9de4-4af181147088");
            if (VED == null)
            {
                VED = Repo.CreateDef<TacticalAbilityViewElementDef>("9a4fd241-a9ea-4ce0-9de4-4af181147088", BloodLust.ViewElementDef);
                VED.name = "E_ViewElement [FieldCommander_AbilityDef]";
                VED.Name = "FieldCommander";
                VED.DisplayName1 = new LocalizedTextBind("FIELDCOMMANDER_NAME");
                VED.Description = new LocalizedTextBind("FIELDCOMMANDER_DESC");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("FieldCommander.png");
            }
            return VED;
        }
    }
}