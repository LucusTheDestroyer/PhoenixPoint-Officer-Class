using Base.Serialization.General;
using Base.Defs;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Tactical.Entities.Abilities; //ShootAbilityDefs are part of these
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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