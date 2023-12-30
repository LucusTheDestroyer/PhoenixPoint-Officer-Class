using System.Collections.Generic;
using Base.Defs;
using Base.UI;
using HarmonyLib;
using PhoenixPoint.Common.Core;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Entities.Items.SkinData;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Animations;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Weapons;
using UsefulMethods;

namespace Officer.Misc
{
    public static class Poseidon90
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        internal static SharedDamageKeywordsDataDef keywords = ModHandler.keywords;

        public static void AddToRiflesList()
        {
            EquipmentListDef Rifles = (EquipmentListDef)Repo.GetDef("4f0d4253-78d7-ee64-7bfd-8c0e699dcfce"); //"RiflesListDef"
            Rifles.Equipments = Rifles.Equipments.AddToArray(GetOrCreate());
        }

        public static WeaponDef GetOrCreate()
        {
            WeaponDef P90 = (WeaponDef)Repo.GetDef("573bed0c-39e8-4f62-8953-d4af5996f238");
            if(P90 == null)
            {
                WeaponDef Gorgon = (WeaponDef)Repo.GetDef("1c053f71-38a0-9674-7821-8dffcdca49aa"); //"PX_LaserPDW_WeaponDef"
                P90 = Repo.CreateDef<WeaponDef>("573bed0c-39e8-4f62-8953-d4af5996f238", Gorgon);
                P90.name = "PX_StandardPDW_WeaponDef";
                P90.Tags.Add(Tags.OfficerClassTag());
                P90.SkinData = P90Skin();
                P90.ViewElementDef = P90VED(Gorgon.ViewElementDef);
                P90.ChargesMax = 50;
                P90.CompatibleAmmunition = new TacticalItemDef[]
                {
                    Poseidon90Ammo.P90Ammo
                };
                P90.ManufactureTech = 4f;
                P90.ManufactureMaterials = 68f;
                P90.ManufacturePointsCost = 51f;
                P90.CrateSpawnWeight = 90;
                P90.DeploymentCost = 12;

                WeaponDef Udar = (WeaponDef)Repo.GetDef("05181c5d-da49-1a94-cab4-08e6be85ddd2"); //"NE_Pistol_WeaponDef"
                P90.MainSwitch = Udar.MainSwitch; 
                P90.VisualEffects = Udar.VisualEffects;
                P90.DamagePayload.DamageKeywords.Find(dkp => dkp.DamageKeywordDef == keywords.DamageKeyword).Value = 25f;
                P90.DamagePayload.AutoFireShotCount = 5;
                P90.DamagePayload.ProjectileVisuals = Udar.DamagePayload.ProjectileVisuals;
                P90.SpreadDegrees = (40.99f/19);
            }
            return P90;
        }

        private static SimpleSkinDataDef P90Skin()
        {
            SimpleSkinDataDef Skin = (SimpleSkinDataDef)Repo.GetDef("276ee5fe-84f8-4bdb-a59b-8545895994df");
            if(Skin == null)
            {
                Skin = Repo.CreateDef<SimpleSkinDataDef>("276ee5fe-84f8-4bdb-a59b-8545895994df");
                Skin.name = "E_SkinData [PX_StandardPDW_WeaponDef]";
                Skin.SkinsToRig = false;
                Skin.DefaultPrefab = new UnityEngine.AddressableAssets.AssetReferenceGameObject("6a06c46ef1cd04046be32d0af9242ef3");
            }
            return Skin;
        }
        
        private static ViewElementDef P90VED(ViewElementDef template)
        {
            ViewElementDef VED = (ViewElementDef)Repo.GetDef("a54ed0bf-afa5-46fa-ab7b-f7b6b3c3bbd0");
            if (VED == null)
            {
                VED = Repo.CreateDef<ViewElementDef>("a54ed0bf-afa5-46fa-ab7b-f7b6b3c3bbd0", template);
                VED.name = "E_View [PX_StandardPDW_WeaponDef]";
                VED.Name = "PX Standard PDW";
                VED.DisplayName1 = new LocalizedTextBind("PX_PDW_NAME");
                VED.Description = new LocalizedTextBind("PX_PDW_DESC");
                VED.SmallIcon = VED.LargeIcon = VED.InventoryIcon = Helper.CreateSpriteFromImageFile("P90_Prototype.png");
            }
            return VED;
        }
    }
}