using Base.Defs;
using Base.Serialization.General;
using Officer.Misc;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Levels;
using System.Collections.Generic;
using System.Linq;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(TacticalAbility))]
    public class EditBattleSummaryRewardsAbility : TacticalAbility
    {
        public EditBattleSummaryRewardsAbilityDef EditBattleSummaryRewardsAbilityDef
        {
            get
            {
                return this.Def<EditBattleSummaryRewardsAbilityDef>();
            }
        }

        public override bool HasValidTargets
        {
            get
            {
                return true;
            }
        }

        public override void AbilityAdded()
        {
            base.AbilityAdded();
            this.TacticalActor.TacticalLevel.GameOverEvent += this.OnGameOver;
        }

        public override void AbilityRemovingStart()
        {
            base.AbilityRemovingStart();
            this.TacticalActor.TacticalLevel.GameOverEvent -= this.OnGameOver;
        }

        public void OnGameOver(TacticalLevelController controller)
        {
            List<TacticalActor> survivingApplicableActor = (from actor in this.TacticalActor.TacticalFaction.GetOwnedActors<TacticalActor>()
            where actor.LevelProgression != null && actor.IsAlive && !actor.GameTags.Contains(CommonHelpers.GetSharedGameTags().VehicleTag) && !actor.GameTags.Contains(Tags.OfficerClassTag())
            select actor).ToList<TacticalActor>();
            foreach (TacticalActor actor in survivingApplicableActor)
            {
                if(this.TacticalActor.TacticalFaction.State == TacFactionState.Won && controller.Difficulty != null)
                {
                    actor.LevelProgression.AddExperience(this.EditBattleSummaryRewardsAbilityDef.Experience);
                    if (actor.LevelProgression.Def.UsesSkillPoints)
                    {
                        actor.CharacterProgression.AddSkillPoints(this.EditBattleSummaryRewardsAbilityDef.SkillPoints);
                    }
                }
            }
        }
    }
}