using Base.Defs;
using Base.Entities.Abilities;
using Base.Entities.Statuses;
using Base.UI;
using Officer.NewDefs;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Effects;
using PhoenixPoint.Tactical.Entities.Statuses;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class Deadeye
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        private static readonly ShootAbilityDef WeaponShoot = (ShootAbilityDef)Repo.GetDef("d3e8b389-069f-04c4-8aca-fb204c74fd37"); //"Weapon_ShootAbilityDef"

        public static ChangeMultiplierShootAbilityDef GetOrCreate()
        {
            ChangeMultiplierShootAbilityDef Deadeye = (ChangeMultiplierShootAbilityDef)Repo.GetDef("114c8923-d798-40f7-85a4-d6d03d3e924f");
            if(Deadeye == null)
            {
                Deadeye = Repo.CreateDef<ChangeMultiplierShootAbilityDef>("114c8923-d798-40f7-85a4-d6d03d3e924f");
                Helper.CopyFieldsByReflection(WeaponShoot, Deadeye);
                Deadeye.name = "Deadeye_ShootAbilityDef";
                Deadeye.CharacterProgressionData = DeadeyeProgression();
                Deadeye.ViewElementDef = DeadeyeVED();
                Deadeye.InputAction = "";
                Deadeye.UsableOnNonProficientEquipment = false;
                Deadeye.WillPointCost = 5f;
                Deadeye.EquipmentTags = new GameTagDef[]
                {
                    Misc.Tags.SingleShotWeaponTag()
                };
                Deadeye.DisablingStatuses = new StatusDef[]
                {
                    (AddAttackBoostStatusDef)Repo.GetDef("c2881c79-d17b-3741-d6c6-8094f30f7744"), //"E_Status [QuickAim_AbilityDef]"
                    (AddAttackBoostStatusDef)Repo.GetDef("291db698-9274-0e11-761a-9a9438cd246f"), //"E_Status [ArmourBreak_AbilityDef]"
                };
                Deadeye.LogStats = true;
                Deadeye.IsDefault = false;
                Deadeye.ProjectileSpreadMultiplier = (2f/3f);
                Deadeye.MultiplierType = DamageApplicationType.BodyPart;
                Deadeye.Value = 1f;
            }
            return Deadeye;
        }

        private static AbilityCharacterProgressionDef DeadeyeProgression()
        {
            AbilityCharacterProgressionDef Progression = (AbilityCharacterProgressionDef)Repo.GetDef("5c249bdf-354f-4873-8603-966d746a60a0");
            if (Progression == null)
            {
                Progression = Repo.CreateDef<AbilityCharacterProgressionDef>("5c249bdf-354f-4873-8603-966d746a60a0");
                Progression.name = "E_CharacterProgressionData [Deadeye_ShootAbilityDef]";
                Progression.RequiredStrength = 0;
                Progression.RequiredWill = 0;
                Progression.RequiredSpeed = 0;
                Progression.SkillPointCost = 30;
                Progression.MutagenCost = 30;
                Progression.PersonalTrackTags = new GameTagDef[]{};
            }
            return Progression;
        }

        private static TacticalAbilityViewElementDef DeadeyeVED()
        {
            TacticalAbilityViewElementDef VED = (TacticalAbilityViewElementDef)Repo.GetDef("aefea025-66ff-4073-b5c6-c5981f4a860b");
            if (VED == null)
            {
                VED = Repo.CreateDef<TacticalAbilityViewElementDef>("aefea025-66ff-4073-b5c6-c5981f4a860b", WeaponShoot.ViewElementDef);
                VED.name = "E_ViewElement [Deadeye_ShootAbilityDef]";
                VED.Name = "Deadeye";
                VED.DisplayName1 = new LocalizedTextBind("DEADEYE_NAME");
                VED.Description = new LocalizedTextBind("DEADEYE_DESC");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("Deadeye.png");
                VED.DisplayWithEquipmentMismatch = false;
            }
            return VED;
        }
    }
}