using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects.ApplicationConditions;
using Base.Entities.Statuses;
using Base.UI;
using Officer.Misc;
using Officer.NewDefs;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Statuses;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class ResilienceTraining
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        private static readonly ApplyStatusAbilityDef AspidaJammer = (ApplyStatusAbilityDef)Repo.GetDef("cf5b7bba-e467-7aa4-88e4-1dd54d24f630"); //"ApplyStatus_MindControlImmunity_AbilityDef"
        private static readonly ChangeAbilitiesCostStatusDef QAStatus = (ChangeAbilitiesCostStatusDef)Repo.GetDef("4fca98a7-843f-3743-0842-c219b75a7544"); //"E_AbilityCostModifier [QuickAim_AbilityDef]"
        public static ApplyStatusAbilityDef GetOrCreate()
        {
            //Passive Officer ability to apply MultiStatus
            ApplyStatusAbilityDef Ability = (ApplyStatusAbilityDef)Repo.GetDef("6f587596-3778-4a2d-a7e6-ac6c50b8d4cb");
            if(Ability == null)
            {
                Ability = Repo.CreateDef<ApplyStatusAbilityDef>("6f587596-3778-4a2d-a7e6-ac6c50b8d4cb", AspidaJammer);
                Ability.name = "ResilienceTraining_AbilityDef";
                Ability.CharacterProgressionData = ResilienceProgression();
                Ability.TargetingDataDef = ResilienceTargeting();
                Ability.ViewElementDef = ResilienceVED();
                Ability.SceneViewElementDef = null;
                Ability.SkillTags = new SkillTagDef[]{};
                Ability.UsableOnNonInteractableActor = true;
                Ability.StatusDef = ResilienceEffects();
                Ability.CanApplyToOffMapTarget = true;
                Ability.TargetApplicationConditions = new EffectConditionDef[]{};
                Ability.RemoveStatusOnAbilityRemoving = false;
            }
            return Ability;
        }

        private static AbilityCharacterProgressionDef ResilienceProgression()
        {
            AbilityCharacterProgressionDef Progression = (AbilityCharacterProgressionDef)Repo.GetDef("42231711-1b9a-43bd-835e-0e5d9ac66ba7");
            if (Progression == null)
            {
                Progression = Repo.CreateDef<AbilityCharacterProgressionDef>("42231711-1b9a-43bd-835e-0e5d9ac66ba7");
                Progression.name = "E_CharacterProgressionData [ResilienceTraining_AbilityDef]";
                Progression.RequiredStrength = 0;
                Progression.RequiredWill = 0;
                Progression.RequiredSpeed = 0;
                Progression.SkillPointCost = 30;
                Progression.MutagenCost = 25;
                Progression.PersonalTrackTags = new GameTagDef[]{};
            }
            return Progression;
        }

        private static TacticalTargetingDataDef ResilienceTargeting()
        {
            TacticalTargetingDataDef targeting = (TacticalTargetingDataDef)Repo.GetDef("209fc6f8-bfc4-48de-8da1-dd846f2466be");
            if (targeting == null)
            {
                targeting = Repo.CreateDef<TacticalTargetingDataDef>("209fc6f8-bfc4-48de-8da1-dd846f2466be", AspidaJammer.TargetingDataDef);
                targeting.name = "E_TargetingData [ResilienceTraining_AbilityDef]";
                targeting.Origin.Range = float.PositiveInfinity;
                targeting.Origin.LineOfSight = LineOfSightType.Ignore;
                targeting.Origin.FactionVisibility = LineOfSightType.Ignore;
            }
            return targeting;
        }

        private static TacticalAbilityViewElementDef ResilienceVED()
        {
            TacticalAbilityViewElementDef VED = (TacticalAbilityViewElementDef)Repo.GetDef("6a0f7e71-32fd-4865-86da-8b377ca1e1c1");
            if (VED == null)
            {
                VED = Repo.CreateDef<TacticalAbilityViewElementDef>("6a0f7e71-32fd-4865-86da-8b377ca1e1c1", AspidaJammer.ViewElementDef);
                VED.name = "E_ViewElementDef [ResilienceTraining_AbilityDef]";
                VED.Name = "Resilience Training";
                VED.DisplayName1 = new LocalizedTextBind("RESILIENCETRAINING_NAME");
                VED.Description = new LocalizedTextBind("RESILIENCETRAINING_DESC");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("ResilienceTraining.png");
            }
            return VED;
        }


        private static MultiStatusDef ResilienceEffects()
        {
            //MultiStatus which applies -1AP cost to Recover and the Status that applies other statuses when recover is activated
            MultiStatusDef statuses = (MultiStatusDef)Repo.GetDef("83ee00ba-ea44-45f8-b961-d46381288116");
            if (statuses == null)
            {
                statuses = Repo.CreateDef<MultiStatusDef>("83ee00ba-ea44-45f8-b961-d46381288116");
                statuses.name = "E_MultiStatus [ResilienceTraining_AbilityDef]";
                statuses.EffectName = "ResilienceTraining";
                statuses.Statuses = new StatusDef[]
                {
                    RecoverDiscountStatus(),
                    OnRecoverActivatedStatus(),
                };
            }
            return statuses;
        }

        private static ChangeAbilitiesCostStatusDef RecoverDiscountStatus()
        {
            ChangeAbilitiesCostStatusDef DiscountStatus = (ChangeAbilitiesCostStatusDef)Repo.GetDef("0382b222-3e99-4dc9-b2ac-e02f68b587ec");
            if (DiscountStatus == null)
            {
                
                DiscountStatus = Repo.CreateDef<ChangeAbilitiesCostStatusDef>("0382b222-3e99-4dc9-b2ac-e02f68b587ec", QAStatus);
                DiscountStatus.name = "E_AbilityCostModifier [ResilienceTraining_AbilityDef]";
                DiscountStatus.DurationTurns = -1;
                DiscountStatus.AbilityCostModification = new TacticalAbilityCostModification
                {
                    TargetAbilityTagDef = Tags.RecoverSkillTag(),
                    SkillTagCullFilter = new SkillTagDef[]{},
                    EquipmentTagDef = null,
                    AbilityCullFilter = null,
                    RequiresProficientEquipment = false,
                    ActionPointModType = TacticalAbilityModificationType.Add,
                    ActionPointMod = -0.25f,
                };
            }
            return DiscountStatus;
        }

        private static ApplyStatusAfterAbilityExecutedStatusDef OnRecoverActivatedStatus()
        {
            ApplyStatusAfterAbilityExecutedStatusDef OnRecover = (ApplyStatusAfterAbilityExecutedStatusDef)Repo.GetDef("72a02af0-25aa-4a22-a4aa-fa5623abb7be");
            if (OnRecover == null)
            {
                RecoverWillAbilityDef Recover = (RecoverWillAbilityDef)Repo.GetDef("a022aa89-a484-9fe4-fa47-a8abd3368150"); //"RecoverWill_AbilityDef"
                Recover.SkillTags = new SkillTagDef[]
                {
                    Tags.RecoverSkillTag()
                };
                OnRecover = Repo.CreateDef<ApplyStatusAfterAbilityExecutedStatusDef>("72a02af0-25aa-4a22-a4aa-fa5623abb7be");
                Helper.CopyFieldsByReflection(QAStatus, OnRecover);
                OnRecover.name = "E_AbilityTrigger [ResilienceTraining]";
                OnRecover.DurationTurns = -1;
                // OnRecover.ShowNotification = false;
                // OnRecover.VisibleOnPassiveBar = false;
                // OnRecover.VisibleOnHealthbar = TacStatusDef.HealthBarVisibility.Hidden;
                // OnRecover.Visuals = null;
                OnRecover.RequiredAbility = Recover;
                // OnRecover.StatusToApply = AdditionalStatuses();
                OnRecover.StatusToApply = BodyPartHealingStatus();
            }
            return OnRecover;
        }

        private static MultiStatusDef AdditionalStatuses()
        {
             //MultiStatus which applies additional statuses when Recover is activated
            MultiStatusDef statuses = (MultiStatusDef)Repo.GetDef("3ea8112f-2106-4a75-8af6-180d675f9bed");
            if (statuses == null)
            {
                statuses = Repo.CreateDef<MultiStatusDef>("3ea8112f-2106-4a75-8af6-180d675f9bed");
                statuses.name = "E_AdditionalStatuses [ResilienceTraining_AbilityDef]";
                statuses.EffectName = "ResilienceTrainingExtras";
                statuses.Statuses = new StatusDef[]
                {
                    DamageResistanceStatus(),
                    BodyPartHealingStatus(),
                };
            }
            return statuses;
        }

        private static DamageMultiplierStatusDef DamageResistanceStatus()
        {
            DamageMultiplierStatusDef DRStatus = (DamageMultiplierStatusDef)Repo.GetDef("d6678c22-b417-4ed3-8413-368062a91390"); 
            if (DRStatus == null)
            {
                DamageMultiplierStatusDef CloseQuarterStatus = (DamageMultiplierStatusDef)Repo.GetDef("28e4d272-7f89-ebc3-34e7-502297898fe0"); //"E_CloseQuatersStatus [CloseQuarters_AbilityDef]"
                DRStatus = Repo.CreateDef<DamageMultiplierStatusDef>("d6678c22-b417-4ed3-8413-368062a91390", CloseQuarterStatus);
                DRStatus.name = "E_DamageResistanceStatus [ResilienceTraining_AbilityDef]";
                DRStatus.EffectName = "Resilience Resistance";
                DRStatus.DurationTurns = 0;
                DRStatus.ExpireOnEndOfTurn = false;
                DRStatus.Visuals = DRVED(CloseQuarterStatus.Visuals);
                DRStatus.Multiplier = 0.5f;
                DRStatus.Range = -1;
            }
            return DRStatus;
        }

        private static HealthChangeStatusDef BodyPartHealingStatus()
        {
            HealthChangeStatusDef HealingStatus = (HealthChangeStatusDef)Repo.GetDef("84aaac30-8df6-4060-83ff-06ad0f3fde65");
            if (HealingStatus == null)
            {
                HealthChangeStatusDef TorsoRegen = (HealthChangeStatusDef)Repo.GetDef("cd43f876-e352-86e4-3808-bb80240d38fc"); //"Regeneration_Torso_Constant_StatusDef"
                HealingStatus = Repo.CreateDef<HealthChangeStatusDef>("84aaac30-8df6-4060-83ff-06ad0f3fde65", TorsoRegen);
                HealingStatus.name = "E_RegenLimbsStatus [ResilienceTraining_AbilityDef]";
                HealingStatus.DurationTurns = 0;
                HealingStatus.ExpireOnEndOfTurn = false;
                HealingStatus.Visuals = HealingVED(TorsoRegen.Visuals);
                HealingStatus.HealthChangeAmount = 5f;
                HealingStatus.TargetActorHealth = false;
                HealingStatus.BodypartSlotNames = new string[]
                {
                    "Head",
                    "Torso",
                    "LeftArm",
                    "RightArm",
                    "LeftLeg",
                    "RightLeg"
                };
            }
            return HealingStatus;
        }

        private static ViewElementDef DRVED(ViewElementDef template)
        {
            ViewElementDef VED = (ViewElementDef)Repo.GetDef("6720d693-5991-4fe9-9165-6340fda19ab9");
            if(VED == null)
            {
                VED = Repo.CreateDef<ViewElementDef>("6720d693-5991-4fe9-9165-6340fda19ab9", template);
                VED.DisplayName1 = new LocalizedTextBind("RESILIENCETRAINING_NAME");
                VED.Description = new LocalizedTextBind("RESILIENCE_DR_DESC");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("Resistance.png");
            }            
            return VED;
        }

        private static ViewElementDef HealingVED(ViewElementDef template)
        {
            ViewElementDef VED = (ViewElementDef)Repo.GetDef("d9efb9df-ca31-4431-9f0a-605eb1994a91");
            if(VED == null)
            {
                VED = Repo.CreateDef<ViewElementDef>("d9efb9df-ca31-4431-9f0a-605eb1994a91", template);
                VED.DisplayName1 = new LocalizedTextBind("RESILIENCETRAINING_NAME");
                VED.Description = new LocalizedTextBind("RESILIENCE_HEAL_DESC");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("ResilienceTraining.png");
            }            
            return VED;
        }
    }
}