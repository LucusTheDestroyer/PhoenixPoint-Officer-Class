using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Statuses;
using Base.UI;
using Officer.NewDefs;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class CoolUnderPressure
    {
        private static readonly DefRepository Repo = ModHandler.Repo;

        private static readonly ApplyStatusAbilityDef Bloodlust = (ApplyStatusAbilityDef)Repo.GetDef("dfe93630-87f7-2774-1bc5-169deb082f7b");
        public static ApplyStatusAbilityDef GetOrCreate()
        {
            ApplyStatusAbilityDef CUP = (ApplyStatusAbilityDef)Repo.GetDef("9e6e8f11-e653-464f-b080-d1789a66dc1f");
            if(CUP == null)
            {
                CUP = Repo.CreateDef<ApplyStatusAbilityDef>("9e6e8f11-e653-464f-b080-d1789a66dc1f", Bloodlust);
                CUP.name = "CoolUnderPressure_AbilityDef";
                CUP.CharacterProgressionData = CUPProgression();
                CUP.ViewElementDef = CUPVED();
                CUP.StatusDef = CUPStatus();
            }
            return CUP;
        }

        private static CoolUnderPressureStatusDef CUPStatus()
        {
            CoolUnderPressureStatusDef Status = (CoolUnderPressureStatusDef)Repo.GetDef("1d2b29f7-1cad-49dd-8aab-1ea0d484feaa");
            if (Status == null)
            {
                Status = Repo.CreateDef<CoolUnderPressureStatusDef>("1d2b29f7-1cad-49dd-8aab-1ea0d484feaa");
                Helper.CopyFieldsByReflection(Bloodlust.StatusDef, Status);
                Status.name = "E_Status [CoolUnderPressure_AbilityDef]";
                Status.EffectName = "CoolUnderPressure";
                Status.DurationTurns = -1;
                Status.ExpireOnEndOfTurn = true;
                Status.Visuals = CUPVED();
                Status.MaxStacks = 15;
                Status.StatsToBuff = new StatModification[]
                {
                    new StatModification(StatModificationType.Add, "Accuracy", 0.02f, Status, 0.02f),
                    new StatModification(StatModificationType.MultiplyRestrictedToBounds, "BonusAttackDamage", 0.01f, Status, 0.01f),
                };
            }
            return Status;
        }

        private static AbilityCharacterProgressionDef CUPProgression()
        {
            AbilityCharacterProgressionDef Progression = (AbilityCharacterProgressionDef)Repo.GetDef("66dfb53b-4ed5-4485-9800-36b514d40e1a");
            if (Progression == null)
            {
                Progression = Repo.CreateDef<AbilityCharacterProgressionDef>("66dfb53b-4ed5-4485-9800-36b514d40e1a");
                Progression.name = "E_CharacterProgressionData [CoolUnderPressure_AbilityDef]";
                Progression.RequiredStrength = 0;
                Progression.RequiredWill = 0;
                Progression.RequiredSpeed = 0;
                Progression.SkillPointCost = 25;
                Progression.MutagenCost = 25;
                Progression.PersonalTrackTags = new GameTagDef[]{};
            }
            return Progression;
        }

        private static TacticalAbilityViewElementDef CUPVED()
        {
            TacticalAbilityViewElementDef VED = (TacticalAbilityViewElementDef)Repo.GetDef("52470d0b-37e3-4744-a2ad-d5cbae171ff2");
            if (VED == null)
            {
                VED = Repo.CreateDef<TacticalAbilityViewElementDef>("52470d0b-37e3-4744-a2ad-d5cbae171ff2", Bloodlust.ViewElementDef);
                VED.name = "E_ViewElement [CoolUnderPressure_AbilityDef]";
                VED.Name = "CoolUnderPressure";
                VED.DisplayName1 = new LocalizedTextBind("COOLUNDERPRESSURE_NAME");
                VED.Description = new LocalizedTextBind("COOLUNDERPRESSURE_DESC");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("CoolUnderPressure.png");
            }
            return VED;
        }
    }
}