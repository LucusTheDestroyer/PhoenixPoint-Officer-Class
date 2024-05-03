using Base.Defs;
using Base.Entities;
using Base.Serialization.General;
using Officer.Harmony;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Effects;
using PhoenixPoint.Tactical.Entities.Weapons;
using PhoenixPoint.Tactical.Levels;

namespace Officer.NewDefs 
{
    [SerializeType(InheritCustomCreateFrom = typeof(ShootAbility))]
    public class ChangeMultiplierShootAbility : ShootAbility
    {
        public ChangeMultiplierShootAbilityDef ChangeMultiplierShootAbilityDef
        {
            get
            {
                return this.Def<ChangeMultiplierShootAbilityDef>();
            }
        }

        private WeaponDef PreviousWeaponApplication = null;

        public override void AbilityAdded()
        {
            base.AbilityAdded();
            OfficerMain.Main.Logger.LogInfo($"Adding ChangeMultiplierShootAbility {this.ChangeMultiplierShootAbilityDef.name} to {this.TacticalActor.DisplayName}");
            base.TacticalActorBase.TacticalLevel.GameOverEvent += this.OnGameOver;
        }

        protected override void OnPlayingActionEnd(PlayingAction action)
        {
            base.OnPlayingActionEnd(action);
            this.RemoveModifier();
        }

        private void OnGameOver(TacticalLevelController controller)
        {
            this.RemoveModifier();
        }

        public void ApplyModifier()
        {
            OfficerMain.Main.Logger.LogInfo($"Applying modifier to {this.Weapon.WeaponDef.name}.");
            switch(this.ChangeMultiplierShootAbilityDef.MultiplierType)
            {
                case DamageApplicationType.Actor:
                    this.Weapon.WeaponDef.DamagePayload.ActorMultiplier += this.ChangeMultiplierShootAbilityDef.Value;
                    break;
                case DamageApplicationType.BodyPart:
                    this.Weapon.WeaponDef.DamagePayload.BodyPartMultiplier += this.ChangeMultiplierShootAbilityDef.Value;
                    break;
                case DamageApplicationType.Object:
                    this.Weapon.WeaponDef.DamagePayload.ObjectMultiplier += this.ChangeMultiplierShootAbilityDef.Value;
                    break;
            }
            this.PreviousWeaponApplication = this.Weapon.WeaponDef;
        }
        
        public void RemoveModifier()
        {
            OfficerMain.Main.Logger.LogInfo($"Removing modifier from {this.PreviousWeaponApplication.name}");
            if(this.PreviousWeaponApplication != null)
            {
                switch(this.ChangeMultiplierShootAbilityDef.MultiplierType)
                {
                    case DamageApplicationType.Actor:
                        this.PreviousWeaponApplication.DamagePayload.ActorMultiplier -= this.ChangeMultiplierShootAbilityDef.Value;
                        // weaponDef.DamagePayload.ActorMultiplier -= modifier.ChangeMultiplierShootAbilityDef.Value;
                        // this.PreviousWeaponApplication.DamagePayload.ActorMultiplier -= modifier.ChangeMultiplierShootAbilityDef.Value;
                        break;
                    case DamageApplicationType.BodyPart:
                        this.PreviousWeaponApplication.DamagePayload.ActorMultiplier -= this.ChangeMultiplierShootAbilityDef.Value;
                        // weaponDef.DamagePayload.BodyPartMultiplier -= modifier.ChangeMultiplierShootAbilityDef.Value;
                        // this.PreviousWeaponApplication.DamagePayload.BodyPartMultiplier -= modifier.ChangeMultiplierShootAbilityDef.Value;
                        break;
                    case DamageApplicationType.Object:
                        this.PreviousWeaponApplication.DamagePayload.ActorMultiplier -= this.ChangeMultiplierShootAbilityDef.Value;
                        // weaponDef.DamagePayload.ObjectMultiplier -= modifier.ChangeMultiplierShootAbilityDef.Value;
                        // this.PreviousWeaponApplication.DamagePayload.ObjectMultiplier -= modifier.ChangeMultiplierShootAbilityDef.Value;
                        break;
                }
                this.PreviousWeaponApplication = null;
            }
        }
    }
}