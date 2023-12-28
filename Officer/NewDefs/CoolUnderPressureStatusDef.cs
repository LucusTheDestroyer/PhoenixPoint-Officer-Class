using Base.Entities.Statuses;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Statuses;

namespace Officer.NewDefs
{   
    [SerializeType(InheritCustomCreateFrom = typeof(TacStatusDef))]
    public class CoolUnderPressureStatusDef : TacStatusDef
    {
        public int MaxStacks;
        public StatModification[] StatsToBuff;
    }
}