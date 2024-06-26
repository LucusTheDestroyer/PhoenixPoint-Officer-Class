using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Effects.ApplicationConditions;
using Base.Entities.Statuses;
using Base.UI;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class TriggerDiscipline
    {
        private static readonly DefRepository Repo = ModHandler.Repo;

        //ApplyStatus_MindControlImmunity_AbilityDef
        private static readonly ApplyStatusAbilityDef AspidaPsyJammer = (ApplyStatusAbilityDef)Repo.GetDef("cf5b7bba-e467-7aa4-88e4-1dd54d24f630");

        public static ApplyStatusAbilityDef GetOrCreate()
        {
            ApplyStatusAbilityDef TriggerDiscipline = (ApplyStatusAbilityDef)Repo.GetDef("6e43173e-df60-466b-98ef-d7ede5f6431b");
            if (TriggerDiscipline == null)
            {
                TriggerDiscipline = Repo.CreateDef<ApplyStatusAbilityDef>("6e43173e-df60-466b-98ef-d7ede5f6431b", AspidaPsyJammer);
                TriggerDiscipline.name = "TriggerDiscipline_ApplyStatusAbilityDef";
                TriggerDiscipline.CharacterProgressionData = TDProgression();
                TriggerDiscipline.TargetingDataDef = TDTargeting();
                TriggerDiscipline.ViewElementDef = TDVED();
                TriggerDiscipline.SceneViewElementDef = TDSceneView();
                
                TriggerDiscipline.StatusDef = DisciplineStatus();
                TriggerDiscipline.TargetApplicationConditions = new EffectConditionDef[]{};
                TriggerDiscipline.StatusSource = StatusSource.Actor;
            }
            return TriggerDiscipline;
        }

        private static StanceStatusDef DisciplineStatus()
        {
            StanceStatusDef Status = (StanceStatusDef)Repo.GetDef("bb6f5d64-103f-4f72-8e94-134e07d0ae49");
            if (Status == null)
            {
                //"StomperLegs_StabilityStance_StatusDef"
                StanceStatusDef StomperStatus = (StanceStatusDef)Repo.GetDef("17d8d9fb-daa4-1e34-88bf-4f30a696bd68");

                Status = Repo.CreateDef<StanceStatusDef>("bb6f5d64-103f-4f72-8e94-134e07d0ae49", StomperStatus);
                Status.name = "E_StatusDef [TriggerDiscipline_ApplyStatusAbilityDef]";
                Status.EffectName = "Trigger Discipline";
                Status.VisibleOnPassiveBar = true;
                Status.VisibleOnHealthbar = TacStatusDef.HealthBarVisibility.AlwaysVisible;
                Status.Visuals = TDVED();
                Status.StatModifications = new ItemStatModification[]
                {
                    new ItemStatModification
                    {
                        TargetStat = StatModificationTarget.Accuracy,
                        Modification = StatModificationType.Add,
                        Value = 0.20f
                    },
                };
            }
            return Status;
        }

        private static AbilityCharacterProgressionDef TDProgression()
        {
            AbilityCharacterProgressionDef Progression = (AbilityCharacterProgressionDef)Repo.GetDef("ecd65038-1749-4388-a048-cdbae7ced2df");
            if (Progression == null)
            {
                Progression = Repo.CreateDef<AbilityCharacterProgressionDef>("ecd65038-1749-4388-a048-cdbae7ced2df");
                Progression.name = "E_CharacterProgressionData [TriggerDiscipline_ApplyStatusAbilityDef]";
                Progression.RequiredStrength = 0;
                Progression.RequiredWill = 0;
                Progression.RequiredSpeed = 0;
                Progression.SkillPointCost = 10;
                Progression.MutagenCost = 10;
                Progression.PersonalTrackTags = new GameTagDef[]{};
            }
            return Progression;
        }

        private static TacticalTargetingDataDef TDTargeting()
        {
            TacticalTargetingDataDef Targeting = (TacticalTargetingDataDef)Repo.GetDef("824506d3-0f8b-4feb-8871-8ad59a9d2566");
            if (Targeting == null)
            {
                Targeting = Repo.CreateDef<TacticalTargetingDataDef>("824506d3-0f8b-4feb-8871-8ad59a9d2566", AspidaPsyJammer.TargetingDataDef);
                Targeting.name = "E_TargetingData [TriggerDiscipline_ApplyStatusAbilityDef]";
                Targeting.Origin.TargetSelf = false;
                Targeting.Origin.LineOfSight = LineOfSightType.Ignore;
                Targeting.Origin.FactionVisibility = LineOfSightType.Ignore;
                Targeting.Origin.Range = 7.1f;
                Targeting.Target.Range = 5.9f;
            }
            return Targeting;
        }

        private static TacticalAbilityViewElementDef TDVED()
        {
            TacticalAbilityViewElementDef VED = (TacticalAbilityViewElementDef)Repo.GetDef("54952742-9e41-44c4-9335-4c226aa19547");
            if (VED == null)
            {
                VED = Repo.CreateDef<TacticalAbilityViewElementDef>("54952742-9e41-44c4-9335-4c226aa19547", AspidaPsyJammer.ViewElementDef);
                VED.name = "E_ViewElement [TriggerDiscipline_ApplyStatusAbilityDef]";
                VED.Name = "TriggerDiscipline";
                VED.DisplayName1 = new LocalizedTextBind("TRIGGERDISCIPLINE_NAME");
                VED.Description = new LocalizedTextBind("TRIGGERDISCIPLINE_DESC");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("TriggerDiscipline.png");
            }
            return VED;
        }

        private static AreaOfEffectAbilitySceneViewDef TDSceneView()
        {
            AreaOfEffectAbilitySceneViewDef SceneView = (AreaOfEffectAbilitySceneViewDef)Repo.GetDef("dc57f31b-8d7e-44d7-99e1-daaacf750d98");
            if (SceneView == null)
            {
                //"_Generic_PassiveAura_SceneViewElementDef"
                AreaOfEffectAbilitySceneViewDef FriendlyAOE = (AreaOfEffectAbilitySceneViewDef)Repo.GetDef("e75f51e8-20cb-9804-8809-5730ca2cb00a"); 
                SceneView = Repo.CreateDef<AreaOfEffectAbilitySceneViewDef>("dc57f31b-8d7e-44d7-99e1-daaacf750d98", FriendlyAOE);
                SceneView.name = "E_AreaOfEffectSceneViewElementDef [TriggerDiscipline_ApplyStatusAbilityDef]";
                // SceneView.TargetRadiusMarker = PhoenixPoint.Tactical.View.GroundMarkerType.AreaOfEffectAura;
                SceneView.UseOriginData = false;
            }
            return SceneView;
        }
    }
}