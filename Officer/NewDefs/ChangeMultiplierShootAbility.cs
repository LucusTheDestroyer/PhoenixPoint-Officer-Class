using Base.Defs;
using Base.Entities;
using Base.Serialization.General;
using PhoenixPoint.Tactical.Entities.Abilities;
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
            base.TacticalActorBase.TacticalLevel.GameOverEvent += this.OnGameOver;
        }

        protected override void OnPlayingActionEnd(PlayingAction action)
        {
            base.OnPlayingActionEnd(action);
            this.RemoveModifier();
        }

        public override void EndTurn()
        {
            base.EndTurn();
            this.RemoveModifier();
        }

        private void OnGameOver(TacticalLevelController controller)
        {
            this.RemoveModifier();
        }

        public void ApplyModifier()
        {
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
            if(this.PreviousWeaponApplication != null)
            {
                switch(this.ChangeMultiplierShootAbilityDef.MultiplierType)
                {
                    case DamageApplicationType.Actor:
                        this.PreviousWeaponApplication.DamagePayload.ActorMultiplier -= this.ChangeMultiplierShootAbilityDef.Value;
                        break;
                    case DamageApplicationType.BodyPart:
                        this.PreviousWeaponApplication.DamagePayload.BodyPartMultiplier -= this.ChangeMultiplierShootAbilityDef.Value;
                        break;
                    case DamageApplicationType.Object:
                        this.PreviousWeaponApplication.DamagePayload.ObjectMultiplier -= this.ChangeMultiplierShootAbilityDef.Value;
                        break;
                }
                this.PreviousWeaponApplication = null;
            }
        }
    }
}