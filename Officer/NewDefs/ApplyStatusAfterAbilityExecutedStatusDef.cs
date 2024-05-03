using Base.Entities.Statuses;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Statuses;
using UnityEngine;

namespace Officer.NewDefs
{   
    [SerializeType(InheritCustomCreateFrom = typeof(TacStatusDef))]
    public class ApplyStatusAfterAbilityExecutedStatusDef : TacStatusDef
    {
        [Header("Required Status. Leave blank if there are no requirements")]
        public TacticalAbilityDef RequiredAbility;

        [Header("Apply Status")]
        public StatusDef StatusToApply;
    }
}