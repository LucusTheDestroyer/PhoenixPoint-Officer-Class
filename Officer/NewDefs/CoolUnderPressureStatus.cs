using Base.Defs;
using Base.Entities.Statuses;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Statuses;
using UnityEngine;

namespace Officer.NewDefs
{   
    [SerializeType(InheritCustomCreateFrom = typeof(TacStatus))]
    public class CoolUnderPressureStatus : TacStatus
    {
        private float WPChangeSinceLastReset;
        public CoolUnderPressureStatusDef CoolUnderPressureStatusDef
        {
            get
            {
                return this.Def<CoolUnderPressureStatusDef>();
            }
        }

        private void ApplyModification()
        {
            int num = Mathf.Min((int)WPChangeSinceLastReset, this.CoolUnderPressureStatusDef.MaxStacks);
            foreach(StatModification modification in CoolUnderPressureStatusDef.StatsToBuff)
            {
                BaseStat baseStat = base.TacticalActor.Status.GetStat(modification.StatName, null);
                if (baseStat == null)
                {
                    return;
                }
                baseStat.RemoveStatModificationsWithSource(modification.Source, true);
                StatModification AdjustedModification = modification.AccumulateModification(num);
                baseStat.AddStatModification(AdjustedModification);
                baseStat.ReapplyModifications();
            }
        }

        public override void OnApply(StatusComponent statusComponent)
        {
            base.OnApply(statusComponent);
            if(base.TacticalActor == null)
            {
                this.RequestUnapply(statusComponent);
                return;
            }
            this.WPChangeSinceLastReset = 0;
            base.TacticalActor.CharacterStats.WillPoints.StatChangeEvent += this.WillPointChangedHandler;
        }

        private void WillPointChangedHandler(BaseStat stat, StatChangeType change, float prevValue, float unclampedValue)
        {
            StatusStat willPointStat = stat as StatusStat;
            if(willPointStat.Value < prevValue)
            {
                WPChangeSinceLastReset += prevValue - willPointStat.Value;
                this.ApplyModification();
            }
        }

        public override void EndTurn()
        {
            base.EndTurn();
            this.WPChangeSinceLastReset = 0;
            
        }
        
        public override void OnUnapply()
        {
            base.OnUnapply();
            foreach(StatModification modification in this.CoolUnderPressureStatusDef.StatsToBuff)
            {
                BaseStat baseStat = base.TacticalActor.Status.GetStat(modification.StatName, null);
                baseStat.RemoveStatModificationsWithSource(modification.Source, true);
                baseStat.ReapplyModifications();
            }
            base.TacticalActor.CharacterStats.WillPoints.StatChangeEvent -= this.WillPointChangedHandler;
        }
    }
}