using Base.Defs;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.DamageKeywords;

namespace Officer.NewDefs
{
    [SerializeType(InheritCustomCreateFrom = typeof(BaseDef))]
    public class BodyPartMultiplierDamageKeywordDataDef : DamageKeywordDef
    {
    }
}