using Base.Defs;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Equipments;

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
            if(data == null)
            {
                data = this.GenerateTargetData();
                return true;
            }
            if(data.Target as ItemSlot != null)
            {
                data.DamageResult.HealthDamage *= this._value;
            }
            // this._accum.BodyPartMultiplier = this._value;
            return true;
        }
    }

}