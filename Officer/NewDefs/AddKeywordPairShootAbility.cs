using Base.Defs;
using Base.Entities;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(ShootAbility))]
    public class AddKeywordPairShootAbility : ShootAbility
    {
        public AddKeywordPairShootAbilityDef AddKeywordPairShootAbilityDef
        {
            get
            {
                return this.Def<AddKeywordPairShootAbilityDef>();
            }
        }

        public override void Activate(object parameter)
        {
            foreach (DamageKeywordPair dkp in this.AddKeywordPairShootAbilityDef.AdditionalDamageKeywords)
            {
                base.TacticalActor.AddDamageKeywordPair(dkp);
            }
            base.Activate(parameter);
        }
        
        protected override void OnPlayingActionEnd(PlayingAction action)
        {
            base.OnPlayingActionEnd(action);
            foreach(DamageKeywordPair dkp in this.AddKeywordPairShootAbilityDef.AdditionalDamageKeywords)
            {
                base.TacticalActor.RemoveDamageKeywordPair(dkp);
            }
        }
    }
}