using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Effects;
using UnityEngine;

namespace Officer.NewDefs 
{
    [CreateAssetMenu(fileName = "ChangeMultiplierShootAbilityDef", menuName = "Defs/Abilities/Tactical/Shoot/ChangeMultiplierShoot")]
    [SerializeType(InheritCustomCreateFrom = typeof(ShootAbilityDef))]
    public class ChangeMultiplierShootAbilityDef : ShootAbilityDef 
    {
        [Header("Which multiplier is it adjusting?")]
        public DamageApplicationType MultiplierType;

        [Header("Increment Multiplier by this amount.")]
        public float Value;
    }
}