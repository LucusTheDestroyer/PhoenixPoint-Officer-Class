using Base.Defs;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Weapons;

namespace Officer.Misc
{
    public static class PDWs
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        internal static SharedDamageKeywordsDataDef keywords = ModHandler.keywords;
        
        //Minor nerfs to the game's existing PDWs so that they scale less effectively against armour. Reduce cost of Gorgon a little to compensate.
        public static void Rebalance()
        {
            Balance_Defender();
            Balance_Enforcer();
            Balance_Gorgon();
        }

        //Udar 2+43, 9MP
        //Yat 3+59, 11MP
        //Cypher 3T+54M, 40MP
        //Poseidon 4T+68M, 51MP
        //Ares 5T+86M, 58MP
        //NJ Gauss 12T+95M, 71MP
        //NJ AR (G) 12T+100M, 71MP
        //NJ Pierce 30T+90M, 86 MP
        //NJ AR (P) 32+95, 90MP
        //Gorgon 52T+105M, 113 MP
        //Deimos 33T+65M, 77MP

        private static void Balance_Defender()
        {
            WeaponDef Defender = (WeaponDef)Repo.GetDef("0bf0c4af-c925-7cf4-99eb-fe201b918c53"); //"NJ_Gauss_PDW_WeaponDef"
            Defender.ChargesMax = 30;
            Defender.CompatibleAmmunition[0].ChargesMax = 30;
            Defender.DamagePayload.DamageKeywords.Find(dkp => dkp.DamageKeywordDef == keywords.DamageKeyword).Value = 35f;
            Defender.DamagePayload.AutoFireShotCount = 3;
        }

        private static void Balance_Enforcer()
        {
            WeaponDef Enforcer = (WeaponDef)Repo.GetDef("6c5c8426-264b-1c34-ba76-4d5060fc7dc8"); //"NJ_PRCR_PDW_WeaponDef"
            Enforcer.DamagePayload.DamageKeywords.Find(dkp => dkp.DamageKeywordDef == keywords.PiercingKeyword).Value = 10f;
            Enforcer.DamagePayload.AutoFireShotCount = 5;
        }

        private static void Balance_Gorgon()
        {
            WeaponDef Gorgon = (WeaponDef)Repo.GetDef("1c053f71-38a0-9674-7821-8dffcdca49aa"); //"PX_LaserPDW_WeaponDef"
            Gorgon.ManufactureTech = 36f;
            Gorgon.ManufactureMaterials = 81f;
            Gorgon.ManufacturePointsCost = 98;
            Gorgon.DamagePayload.DamageKeywords.Find(dkp => dkp.DamageKeywordDef == keywords.DamageKeyword).Value = 30f;
            Gorgon.SpreadDegrees = (40.99f/21);
        }
    }
}