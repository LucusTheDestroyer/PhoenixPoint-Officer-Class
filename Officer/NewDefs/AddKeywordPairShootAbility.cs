using Base.Defs;
using Base.Entities;
using Base.Serialization.General;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Tactical.Entities.Abilities; //ShootAbilityDefs are part of these
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using HarmonyLib;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(ShootAbility))]
    public class AdditionalDamageShootAbility : ShootAbility
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
            // List<DamageKeywordPair> WeaponDamageKeywords = this.Weapon.WeaponDef.DamagePayload.DamageKeywords;
            // List<DamageKeywordPair> AdditionalKeywords = this.AdditionalDamageShootAbilityDef.AdditionalDamageKeywords;

            foreach (DamageKeywordPair dkp in this.AddKeywordPairShootAbilityDef.AdditionalDamageKeywords)
            {
                // TestingMain.Main.Logger.LogInfo($"Applying DamageKeyword to Actor: {dkp.DamageKeywordDef.name}, Value: {dkp.Value}");
                base.TacticalActor.AddDamageKeywordPair(dkp);
            }
            
            // foreach(DamageKeywordPair dkp in this.AdditionalDamageShootAbilityDef.AdditionalDamageKeywords)
            // {
            //     // this.Weapon.WeaponDef.DamagePayload.DamageKeywords = this.Weapon.WeaponDef.DamagePayload.DamageKeywords.Add
            //     this.Weapon.WeaponDef.DamagePayload.DamageKeywords.Add(dkp);
            // }
            // WeaponDamageKeywords.AddRange(AdditionalKeywords);

            base.Activate(parameter);
            
            // foreach(DamageKeywordPair dkp in this.AdditionalDamageShootAbilityDef.AdditionalDamageKeywords)
            // {
            //     this.Weapon.WeaponDef.DamagePayload.DamageKeywords.Remove(dkp);
            // }
            // RemoveAll (DamageKeywordPair)dkp where AdditionalKeywords.Contains(dkp)
            // WeaponDamageKeywords.RemoveAll(dkp => AdditionalKeywords.Contains(dkp));
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