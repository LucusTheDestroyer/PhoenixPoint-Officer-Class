using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Abilities;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(TacticalAbilityDef))]
    public class EditBattleSummaryRewardsAbilityDef : TacticalAbilityDef
    {
        public int Experience;
        public int SkillPoints;
    }
}