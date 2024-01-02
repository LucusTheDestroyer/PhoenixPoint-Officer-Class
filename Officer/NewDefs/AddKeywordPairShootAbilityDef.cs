using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using UnityEngine;

namespace Officer.NewDefs 
{
    [CreateAssetMenu(fileName = "AddKeywordPairShootAbilityDef", menuName = "Defs/Abilities/Tactical/Shoot/AddKeywordPairShoot")]
    [SerializeType(InheritCustomCreateFrom = typeof(ShootAbilityDef))]
    public class AddKeywordPairShootAbilityDef : ShootAbilityDef 
    {
        [Header("Additional Damage KeywordPairs")]
        public DamageKeywordPair[] AdditionalDamageKeywords;
    }
}