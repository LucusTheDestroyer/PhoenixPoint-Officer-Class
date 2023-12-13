using AK.Wwise;
using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects;
using Base.Entities.Effects.ApplicationConditions;
using Base.Entities.Statuses;
using Base.UI;
using Officer.Misc;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Effects.ApplicationConditions;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class RallyAbility
    {
        private static readonly DefRepository Repo = EnableMain.Repo;
        public static ApplyStatusAbilityDef GetOrCreate()
        {
            ApplyStatusAbilityDef Rally = (ApplyStatusAbilityDef)Repo.GetDef("0b2ed8a0-9349-49ad-862d-11ad0ef0958f");
            if (Rally == null)
            {
                //"DeterminedAdvance_AbilityDef"
                ApplyStatusAbilityDef Onslaught = (ApplyStatusAbilityDef)Repo.GetDef("175744da-5e1d-d1d4-58fb-b08d226b58f6");

                Rally = Repo.CreateDef<ApplyStatusAbilityDef>("0b2ed8a0-9349-49ad-862d-11ad0ef0958f", Onslaught);
                Rally.name = "Rally_ApplyStatusAbilityDef";
                Rally.CharacterProgressionData = RallyProgression();
                Rally.ViewElementDef = RallyVED();
                Rally.ActionPointCost = 0.25f;
                Rally.WillPointCost = 3f;
                Rally.StatusDef = RallyStatus(Onslaught.StatusDef as TacEffectStatusDef);
                Rally.TargetApplicationConditions = new EffectConditionDef[]
                {
                    RallyConditions()
                };
            }
            return Rally;
        }

        private static TacEffectStatusDef RallyStatus(TacEffectStatusDef template)
        {
            TacEffectStatusDef Status = (TacEffectStatusDef)Repo.GetDef("be672117-c89e-4628-8af5-3c93a3a1ce9d");
            if (Status == null)
            {
                Status = Repo.CreateDef<TacEffectStatusDef>("be672117-c89e-4628-8af5-3c93a3a1ce9d", template);
                Status.name = "E_Status [Rally_ApplyStatusAbilityDef]";
                Status.Visuals = RallyVED();
                Status.EffectDef = RallyMultiEffect();
            }
            return Status;
        }

        private static MultiEffectDef RallyMultiEffect()
        {
            MultiEffectDef Multi = (MultiEffectDef)Repo.GetDef("75ee2be0-6d75-4291-9edf-4e5a7c6945d4");
            if (Multi == null)
            {
                Multi = Repo.CreateDef<MultiEffectDef>("75ee2be0-6d75-4291-9edf-4e5a7c6945d4");
                Multi.name = "E_MultiEffectDef [Rally_ApplyStatusAbilityDef]";
                Multi.ApplicationConditions = new EffectConditionDef[]{};
                Multi.EffectDefs = new EffectDef[]
                {
                    RallyEffect(),
                    PanicImmunity(),
                };
            }
            return Multi;
        }

        private static ModifyStatusStatRatioEffectDef RallyEffect()
        {
            ModifyStatusStatRatioEffectDef Effect = (ModifyStatusStatRatioEffectDef)Repo.GetDef("c4ba8eb3-ea30-49a1-9d70-07747baad975");
            if (Effect == null)
            {
                Effect = Repo.CreateDef<ModifyStatusStatRatioEffectDef>("c4ba8eb3-ea30-49a1-9d70-07747baad975");
                Effect.name = "E_RallyEffect [Rally_ApplyStatusAbilityDef]";
                Effect.ApplicationConditions = new EffectConditionDef[]{};
                Effect.Modifications = new ModifyStatusStatRatioEffectDef.StatusStatRatioModification[]
                {
                    new ModifyStatusStatRatioEffectDef.StatusStatRatioModification
                    {
                        StatName = "WillPoints",
                        ModificationValue = 0.25f
                    }
                };
            }
            return Effect;
        }

        private static StatusEffectDef PanicImmunity()
        {
            StatusEffectDef Immunity = (StatusEffectDef)Repo.GetDef("122d1fd9-7849-421f-927e-2552d2a7fcd8");
            if (Immunity == null)
            {
                Immunity = Repo.CreateDef<StatusEffectDef>("122d1fd9-7849-421f-927e-2552d2a7fcd8");
                Immunity.name = "E_ApplyEffect [Rally_ApplyStatusAbilityDef]";
                Immunity.ApplicationConditions = new EffectConditionDef[]{};
                Immunity.StatusDef = (DamageMultiplierStatusDef)Repo.GetDef("c8a493c7-e0cb-87a4-c97f-9c1ec28dbf09"); //"PanicImmunity_StatusDef"
            }
            return Immunity;
        }

        private static StatThresholdEffectConditionDef RallyConditions()
        {
            StatThresholdEffectConditionDef Condition = (StatThresholdEffectConditionDef)Repo.GetDef("3095db18-3091-4530-8b5d-137c7ecbe8dd");
            if (Condition == null)
            {
                Condition = Repo.CreateDef<StatThresholdEffectConditionDef>("3095db18-3091-4530-8b5d-137c7ecbe8dd");
                Condition.name = "E_ApplicationConditions [Rally_ApplyStatusAbilityDef]";
                Condition.ThresholdCondition = ThresholdCondition.LesserThan;
                Condition.StatName = "WillPoints";
                Condition.Value = 1f;
                Condition.ValueAsFractionOfMax = true;
            }
            return Condition;
        }


        private static AbilityCharacterProgressionDef RallyProgression()
        {
            AbilityCharacterProgressionDef Progression = (AbilityCharacterProgressionDef)Repo.GetDef("e66dc46c-758f-476c-adb2-68351cf008c7");
            if (Progression == null)
            {
                Progression = Repo.CreateDef<AbilityCharacterProgressionDef>("e66dc46c-758f-476c-adb2-68351cf008c7");
                Progression.name = "E_CharacterProgressionData [Rally_ApplyStatusAbilityDef]";
                Progression.RequiredStrength = 0;
                Progression.RequiredWill = 0;
                Progression.RequiredSpeed = 0;
                Progression.SkillPointCost = 15;
                Progression.MutagenCost = 15;
                Progression.PersonalTrackTags = new GameTagDef[]{};
            }
            return Progression;
        }

        private static TacticalAbilityViewElementDef RallyVED()
        {
            TacticalAbilityViewElementDef VED = (TacticalAbilityViewElementDef)Repo.GetDef("06737aab-7ce7-4197-b1e7-9ee19fba8996");
            if (VED == null)
            {
                TacticalAbilityViewElementDef WarCryVED = (TacticalAbilityViewElementDef)Repo.GetDef("2b9ccba4-da27-3a43-92ab-511ef1dc4e11");

                VED = Repo.CreateDef<TacticalAbilityViewElementDef>("06737aab-7ce7-4197-b1e7-9ee19fba8996", WarCryVED);
                VED.name = "E_ViewElement [Rally_ApplyStatusAbilityDef]";
                VED.Name = "Rally";
                VED.DisplayName1 = new LocalizedTextBind("RALLY_NAME");
                VED.Description = new LocalizedTextBind("RALLY_DESC");
            }
            return VED;
        }
    }
}