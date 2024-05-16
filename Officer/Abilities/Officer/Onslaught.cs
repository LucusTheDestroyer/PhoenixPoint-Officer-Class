using Base.Defs;
using Base.Entities.Effects.ApplicationConditions;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Effects.ApplicationConditions;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class Onslaught
    {
        private static readonly DefRepository Repo = ModHandler.Repo;

        public static ApplyStatusAbilityDef DeterminedAdvance
        {
            get
            {
                return (ApplyStatusAbilityDef)Repo.GetDef("175744da-5e1d-d1d4-58fb-b08d226b58f6"); //"DeterminedAdvance_AbilityDef"
            }
        }

        public static ApplyStatusAbilityDef GetAndUpdate()
        {
            DeterminedAdvance.CharacterProgressionData.SkillPointCost = 20;
            DeterminedAdvance.TargetingDataDef.Origin.Range = 10.1f;
            DeterminedAdvance.ViewElementDef.Description = new Base.UI.LocalizedTextBind("ONSLAUGHT_DESC");
            DeterminedAdvance.ViewElementDef.SmallIcon = DeterminedAdvance.ViewElementDef.LargeIcon = Helper.CreateSpriteFromImageFile("NewOnslaught.png");
            DeterminedAdvance.UsesPerTurn = -1;
            DeterminedAdvance.TargetApplicationConditions = new EffectConditionDef[]
            {
                OnslaughtStatusCondition(),
                ActionPointCondition(),
            };
            return DeterminedAdvance;
        }

        private static ActorHasStatusEffectConditionDef OnslaughtStatusCondition()
        {
            ActorHasStatusEffectConditionDef Condition = (ActorHasStatusEffectConditionDef)Repo.GetDef("b5eec90d-aa00-4f4d-bf70-cc3bacc72f46");
            if (Condition == null)
            {
                Condition = Repo.CreateDef<ActorHasStatusEffectConditionDef>("b5eec90d-aa00-4f4d-bf70-cc3bacc72f46");
                Condition.name = "E_NoRallyStatus_ApplicationCondition [Rally_ApplyStatusAbilityDef]";
                Condition.StatusDef = DeterminedAdvance.StatusDef;
                Condition.HasStatus = false;
            }
            return Condition;
        }
        private static StatThresholdEffectConditionDef ActionPointCondition()
        {
            StatThresholdEffectConditionDef Condition = (StatThresholdEffectConditionDef)Repo.GetDef("5387e199-4083-4048-b46e-3d183a4c6c90");
            if (Condition == null)
            {
                Condition = Repo.CreateDef<StatThresholdEffectConditionDef>("5387e199-4083-4048-b46e-3d183a4c6c90");
                Condition.name = "E_ApplicationConditions [DeterminedAdvance_AbilityDef]";
                Condition.ThresholdCondition = ThresholdCondition.LesserThan;
                Condition.StatName = "ActionPoints";
                Condition.Value = 1f;
                Condition.ValueAsFractionOfMax = true;
            }
            return Condition;
        }
    }
}