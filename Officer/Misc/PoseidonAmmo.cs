using Base.Defs;
using Base.UI;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Collections.Generic;
using UsefulMethods;

namespace Officer.Misc
{
    public static class Poseidon90Ammo
    {
        private static readonly DefRepository Repo = ModHandler.Repo;

        public static TacticalItemDef P90Ammo
        {
            get
            {
                return (TacticalItemDef)Repo.GetDef("096ebca6-8d79-4794-f8f5-8eef572db2a8"); //"PX_PDW_AmmoClip_ItemDef"
            }
        }

        public static void UpdateItemWeaponDatabase()
        {
            AmmoWeaponDatabase.AmmoToWeaponDictionary.Add(P90Ammo, new List<TacticalItemDef>(){Poseidon90.GetOrCreate()});
        }

        public static void UpdateP90Ammo()
        {
            P90Ammo.ViewElementDef = AmmoVED();
            P90Ammo.ChargesMax = 50;
            P90Ammo.ManufactureMaterials = 18f;
            P90Ammo.CrateSpawnWeight = 1100;
        }

        private static ViewElementDef AmmoVED()
        {
            ViewElementDef VED = (ViewElementDef)Repo.GetDef("4559e6c9-d704-4616-9ebc-ff15f3d6bd94");
            if (VED == null)
            {
                VED = Repo.CreateDef<ViewElementDef>("4559e6c9-d704-4616-9ebc-ff15f3d6bd94", P90Ammo.ViewElementDef);
                VED.Name = "PX PDW Ammo";
                VED.DisplayName1 = new LocalizedTextBind("AMMO_PX_PDW_MAGAZINE_NAME");
                VED.Description = new LocalizedTextBind("AMMO_PX_PDW_MAGAZINE_DESC");
                VED.SmallIcon = VED.LargeIcon = VED.InventoryIcon = Helper.CreateSpriteFromImageFile("P90ammo.png");
                //Need Small&Large icon in B&W version of the Inventory icon
            }
            return VED;
        }
    }
}