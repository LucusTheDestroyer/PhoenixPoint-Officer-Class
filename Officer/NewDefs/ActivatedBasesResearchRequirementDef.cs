using Base.Serialization.General;
using PhoenixPoint.Geoscape.Entities.Research.Requirement;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(ResearchRequirementDef))]
    public class ActivatedBasesResearchRequirementDef : ResearchRequirementDef
    {
        public override ResearchRequirement Instantiate()
        {
            return new ActivatedBasesResearchRequirement(this);
        }
    }
}