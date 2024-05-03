using Base.Defs;
using Base.Entities.Effects.ApplicationConditions;
using Base.Entities.Statuses;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Statuses;
using System.Linq;

namespace Officer.NewDefs
{   
    [SerializeType(InheritCustomCreateFrom = typeof(TacStatus))]
    public class ApplyStatusAfterAbilityExecutedStatus : TacStatus
    {

        public ApplyStatusAfterAbilityExecutedStatusDef ApplyStatusAfterAbilityExecutedStatusDef
        {
            get
            {
                return this.Def<ApplyStatusAfterAbilityExecutedStatusDef>();
            }
        }

        public override void OnApply(StatusComponent statusComponent)
        {
            base.OnApply(statusComponent);
            base.TacticalActorBase.TacticalLevel.AbilityExecutedEvent += OnAbilityExecuted;
        }
        
        public override void OnUnapply()
        {
            base.OnUnapply();
            Status status = this.TryGetStatusFromActor(base.TacticalActorBase);
            if (status != null)
            {
                base.TacticalActorBase.Status.UnapplyStatus(status);
            }
            base.TacticalActorBase.TacticalLevel.AbilityExecutedEvent -= OnAbilityExecuted;
        }

        private void OnAbilityExecuted(TacticalAbility ability, object parameter)
        {
            if (base.TacticalActorBase == null)
            {
                return;
            }
            if (!base.TacticalActorBase.TacticalLevel.TurnIsPlaying)
            {
                return;
            }
            if (this.ApplyStatusAfterAbilityExecutedStatusDef.RequiredAbility != null && ability.TacticalAbilityDef != this.ApplyStatusAfterAbilityExecutedStatusDef.RequiredAbility)
            {
                return;
            }
            if (ability.TacticalActorBase == this.TacticalActorBase)
            {
                CheckApplicationConditions();
            }
        }

        private void CheckApplicationConditions()
        {
            //Applying a MultiStatusDef - takes ApplicationConditions of Status itself;
            if (this.ApplyStatusAfterAbilityExecutedStatusDef.ApplicationConditions.All((EffectConditionDef condition) => condition.ConditionMet(base.TacticalActorBase)))
            {
                ApplyStatusIfNotApplied(base.TacticalActorBase);
                return;
            }
            Status status = TryGetStatusFromActor(base.TacticalActorBase);
            if (status != null)
            {
                base.TacticalActorBase.Status.UnapplyStatus(status);
            }
        }

        private void ApplyStatusIfNotApplied(TacticalActorBase actor)
        {
            Status status = TryGetStatusFromActor(actor);
            if (status == null)
            {
                actor.Status.ApplyStatus(ApplyStatusAfterAbilityExecutedStatusDef.StatusToApply);
            }
        }

        private Status TryGetStatusFromActor(TacticalActorBase actor)
        {
            return actor.Status.Statuses.FirstOrDefault((Status s) => s.Def == ApplyStatusAfterAbilityExecutedStatusDef.StatusToApply);
        }
    }
}