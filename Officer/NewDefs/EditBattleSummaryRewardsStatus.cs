using Base.Defs;
using Base.Entities.Statuses;
using Base.Serialization.General;
using Officer.Misc;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Statuses;
using PhoenixPoint.Tactical.Levels;
using System.Collections.Generic;
using System.Linq;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(TacStatus))]
    public class EditBattleSummaryRewardsStatus : TacStatus
    {
        public EditBattleSummaryRewardsStatusDef EditBattleSummaryRewardsStatusDef
        {
            get
            {
                return this.Def<EditBattleSummaryRewardsStatusDef>();
            }
        }

        public override void OnApply(StatusComponent statusComponent)
        {
            base.OnApply(statusComponent);
            this.TacticalLevel.GameOverEvent += this.OnGameOver;
        }

        public void OnGameOver(TacticalLevelController controller)
        {
            List<TacticalActor> survivingApplicableActor = (from actor in this.TacticalActor.TacticalFaction.GetOwnedActors<TacticalActor>()
            where actor.LevelProgression != null && actor.IsAlive && !actor.GameTags.Contains(CommonHelpers.GetSharedGameTags().VehicleTag) && !actor.GameTags.Contains(NewTags.OfficerClassTag())
            select actor).ToList<TacticalActor>();
            foreach (TacticalActor actor in survivingApplicableActor)
            {
                if(this.TacticalActor.TacticalFaction.State == TacFactionState.Won && controller.Difficulty != null)
                {
                    actor.LevelProgression.AddExperience(this.EditBattleSummaryRewardsStatusDef.Experience);
                    if (actor.LevelProgression.Def.UsesSkillPoints)
                    {
                        actor.CharacterProgression.AddSkillPoints(this.EditBattleSummaryRewardsStatusDef.SkillPoints);
                    }
                }
            }
        }

        public override void OnUnapply()
        {
            base.OnUnapply();
            this.TacticalLevel.GameOverEvent -= this.OnGameOver;
        }
    }
}