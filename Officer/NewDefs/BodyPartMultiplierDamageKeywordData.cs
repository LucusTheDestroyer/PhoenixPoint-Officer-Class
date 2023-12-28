using Base.Defs;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;

namespace Officer.NewDefs
{
    [SerializeType(InheritCustomCreateFrom = typeof(BaseDef))]
    public class BodyPartMultiplierDamageKeywordData : DamageKeyword
    {
        public BodyPartMultiplierDamageKeywordDataDef BodyPartMultiplierDamageKeywordDataDef
        {
            get
            {
                return this.Def<BodyPartMultiplierDamageKeywordDataDef>();
            }
        }
        protected override bool ProcessKeywordDataInternal(ref DamageAccumulation.TargetData data)
        {
            this._accum.BodyPartMultiplier = this._value;
            return true;
        }
    }

}