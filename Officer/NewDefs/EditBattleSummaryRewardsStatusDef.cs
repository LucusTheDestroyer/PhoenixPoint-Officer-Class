using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Statuses;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(TacStatusDef))]
    public class EditBattleSummaryRewardsStatusDef : TacStatusDef
    {
        public int Experience;
        public int SkillPoints;
    }
}