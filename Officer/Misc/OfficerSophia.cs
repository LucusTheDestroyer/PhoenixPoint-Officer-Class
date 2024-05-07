using System.Linq;
using Base.Defs;
using HarmonyLib;
using Officer.Abilities;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Weapons;

namespace Officer.Misc
{
    public static class OfficerSophia
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        private static readonly TacCharacterDef SophiaTut1 = (TacCharacterDef)Repo.GetDef("977b21fc-1a46-a314-2b22-a6729345cb3f"); //"PX_Sophia_Tutorial_TacCharacterDef"
        private static readonly TacCharacterDef SophiaTut2 = (TacCharacterDef)Repo.GetDef("400f644c-41f2-c534-1b99-34d48400b7f7"); //"PX_Sophia_Tutorial2_TacCharacterDef"
        private static readonly TacCharacterDef SophiaTFTV = (TacCharacterDef)Repo.GetDef("D9EC7144-6EB5-451C-9015-3E67F194AB1B"); //"PX_Sophia_TFTV_TacCharacterDef"
        private static readonly WeaponDef Ares = (WeaponDef)Repo.GetDef("88349756-7c98-ce54-69bb-0d03d2d2e548"); //"PX_AssaultRifle_WeaponDef"
        private static readonly WeaponDef Cypher = (WeaponDef)Repo.GetDef("79c6494a-4dad-8824-2897-52b324d71e62");//"PX_Pistol_WeaponDef"
        private static readonly EquipmentDef Medkit = (EquipmentDef)Repo.GetDef("5b5cb115-1e45-b2a4-d9d3-253ac3560f79"); //"Medkit_EquipmentDef"
        private static readonly ItemDef AresAmmo = (ItemDef)Repo.GetDef("cbae3112-b372-6554-e80a-a2230f05e2a6"); //"PX_AssaultRifle_AmmoClip_ItemDef"

        public static void Implement()
        {
            OfficerMain.Main.Logger.LogInfo("Implement Sophia called");
			SophiaTut1.Data.Abilites = SophiaTut1.Data.Abilites.AddToArray(OfficerClassProficiency.GetOrCreate());
			SophiaTut1.Data.GameTags = SophiaTut1.Data.GameTags.AddToArray(Tags.OfficerClassTag());

            SophiaTut2.Data.Abilites = SophiaTut2.Data.Abilites.AddToArray(OfficerClassProficiency.GetOrCreate());
			SophiaTut2.Data.GameTags = SophiaTut2.Data.GameTags.AddToArray(Tags.OfficerClassTag());

            SophiaTut2.Data.EquipmentItems = new ItemDef[] { Poseidon90.GetOrCreate(), Cypher, Medkit };
            SophiaTut2.Data.InventoryItems = new ItemDef[] {Poseidon90Ammo.P90Ammo};
            
            if(SophiaTFTV != null)
            {
                OfficerMain.Main.Logger.LogInfo("SophiaTFTV not null. Ensuring changes are transferred");
                SophiaTFTV.Data.Abilites = SophiaTFTV.Data.Abilites.AddToArray(OfficerClassProficiency.GetOrCreate());
                SophiaTFTV.Data.GameTags = SophiaTFTV.Data.GameTags.AddToArray(Tags.OfficerClassTag());
                SophiaTFTV.Data.EquipmentItems = new ItemDef[] { Poseidon90.GetOrCreate(), Cypher, Medkit };
                SophiaTFTV.Data.InventoryItems = new ItemDef[] {Poseidon90Ammo.P90Ammo};
            }

            ItemUnit PDWAmmo = new ItemUnit
            {
                ItemDef = Poseidon90Ammo.P90Ammo,
                Quantity = 1
            };

            foreach(GameDifficultyLevelDef difficulty in Repo.GetAllDefs<GameDifficultyLevelDef>())
            {
                PDWAmmo.Quantity = 9 - difficulty.Order;
                difficulty.StartingStorage = difficulty.StartingStorage.AddToArray(PDWAmmo);
            }            
        }

        public static void Revert()
        {
            OfficerMain.Main.Logger.LogInfo("Revert Sophia called");
            SophiaTut1.Data.Abilites = SophiaTut1.Data.Abilites.Where(perk => perk != OfficerClassProficiency.GetOrCreate()).ToArray();
            SophiaTut1.Data.GameTags = SophiaTut1.Data.GameTags.Where(tag => tag != Tags.OfficerClassTag()).ToArray();

            SophiaTut2.Data.Abilites = SophiaTut2.Data.Abilites.Where(perk => perk != OfficerClassProficiency.GetOrCreate()).ToArray();
            SophiaTut2.Data.GameTags = SophiaTut2.Data.GameTags.Where(tag => tag != Tags.OfficerClassTag()).ToArray();

            SophiaTut2.Data.EquipmentItems = new ItemDef[] { Ares, Medkit, AresAmmo };
            SophiaTut2.Data.InventoryItems = new ItemDef[] {};
            
            if(SophiaTFTV != null)
            {
                OfficerMain.Main.Logger.LogInfo("SophiaTFTV not null. Ensuring changes are transferred");
                SophiaTFTV.Data.Abilites = SophiaTFTV.Data.Abilites.Where(perk => perk != OfficerClassProficiency.GetOrCreate()).ToArray();
                SophiaTFTV.Data.GameTags = SophiaTFTV.Data.GameTags.Where(tag => tag != Tags.OfficerClassTag()).ToArray();

                SophiaTut2.Data.EquipmentItems = new ItemDef[] { Ares, Medkit, AresAmmo };
                SophiaTFTV.Data.InventoryItems = new ItemDef[] {};
            }

            foreach(GameDifficultyLevelDef difficulty in Repo.GetAllDefs<GameDifficultyLevelDef>())
            {
                difficulty.StartingStorage = difficulty.StartingStorage.Where(item => item.ItemDef != Poseidon90Ammo.P90Ammo).ToArray();
            }
        }
    }
}