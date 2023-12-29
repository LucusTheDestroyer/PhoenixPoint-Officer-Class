using Base.Serialization.General;
using PhoenixPoint.Geoscape.Entities.Research.Requirement;
using PhoenixPoint.Geoscape.Entities.Sites;
using PhoenixPoint.Geoscape.Levels;
using PhoenixPoint.Geoscape.Levels.Factions;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(ResearchRequirement))]
    public class ActivatedBasesResearchRequirement : ResearchRequirement
    {
        public ActivatedBasesResearchRequirement(ResearchRequirementDef def) : base(def)
        {
        }
        
        protected override void SubscribeEvents(GeoFaction faction)
        {
            (faction as GeoPhoenixFaction).OnBaseActivated += this.OnBaseActivated;   
        }

        protected override void UnsubscribeEvents(GeoFaction faction)
        {
            (faction as GeoPhoenixFaction).OnBaseActivated -= this.OnBaseActivated;
        }

        private void OnBaseActivated(GeoPhoenixBase phoenixBase, bool activatedFromExploration)
        {
            base.UpdateProgress(1);
        }
    }
}