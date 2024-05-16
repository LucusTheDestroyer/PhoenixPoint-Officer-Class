using Base.Defs;
using Officer.Abilities;
using Officer.NewDefs;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Geoscape.Entities.Research;
using PhoenixPoint.Geoscape.Entities.Research.Requirement;
using PhoenixPoint.Geoscape.Entities.Research.Reward;
using System.Collections.Generic;
using Base.UI;

namespace Officer.Misc
{
    public static class OfficerResearch
    {
        private static readonly DefRepository Repo = ModHandler.Repo;

        public static void UpdatePXResearchDB()
        {
            ResearchDbDef PXDB = (ResearchDbDef)Repo.GetDef("2fd1c6e5-89d1-06d4-caff-af553b860240"); //"pp_ResearchDB"
            PXDB.Researches.Add(GetOrCreate());
        }

        public static ResearchDef GetOrCreate()
        {
            ResearchDef Research = (ResearchDef)Repo.GetDef("b511f865-8626-4c5d-9635-16e61cb6dd87");
            if (Research == null)
            {
                ResearchDef HelCannonResearch = (ResearchDef)Repo.GetDef("b30d89ee-1a4e-5f3b-c1b0-73c9a415ed19"); //"PX_HelCannon_ResearchDef"

                Research = Repo.CreateDef<ResearchDef>("b511f865-8626-4c5d-9635-16e61cb6dd87", HelCannonResearch);
                Research.name = "PX_OfficerTraining_ResearchDef";
                Research.Id = "PX_OfficerTraining_ResearchDef";
                Research.RevealRequirements.Container =  new ReseachRequirementDefOpContainer[]
                {
                    new ReseachRequirementDefOpContainer
                    {
                        Requirements = new ResearchRequirementDef[]
                        {
                            HavenRecruitsRequirement(),
                            ActivatedBasesRequirement(),
                        },
                        Operation = ResearchContainerOperation.ALL
                    },
                };
                Research.ViewElementDef = ResearchVED();
                Research.ResearchCost = 1000;
                Research.Unlocks = new ResearchRewardDef[]
                {
                    OfficerClassReward(),
                    ManufactureRewards(),
                };
                
            }
            return Research;
        }

        private static ClassResearchRewardDef OfficerClassReward()
        {
            ClassResearchRewardDef Reward = (ClassResearchRewardDef)Repo.GetDef("b3b86cef-e590-423a-b314-2285ae4f8ab9");
            if (Reward == null)
            {
                Reward = Repo.CreateDef<ClassResearchRewardDef>("b3b86cef-e590-423a-b314-2285ae4f8ab9");
                Reward.name = "E_ClassResearchReward [PX_OfficerTraining_ResearchDef]";
                Reward.ValidForFactions = new List<PhoenixPoint.Geoscape.Levels.GeoFactionDef>{};
                Reward.ValidForDLC = new List<Base.Platforms.EntitlementDef>{};
                Reward.SpecializationDef = OfficerSpecialisation.GetOrCreate();
            }
            return Reward;
        }

        private static ManufactureResearchRewardDef ManufactureRewards()
        {
            ManufactureResearchRewardDef Reward = (ManufactureResearchRewardDef)Repo.GetDef("7af7832d-1c2e-4ae8-b610-0c0899975f1a");
            if(Reward == null)
            {
                Reward = Repo.CreateDef<ManufactureResearchRewardDef>("7af7832d-1c2e-4ae8-b610-0c0899975f1a");
                Reward.name = "E_ManufactureReward [PX_OfficerTraining_ResearchDef]";
                Reward.ValidForFactions = new List<PhoenixPoint.Geoscape.Levels.GeoFactionDef>();
                Reward.ValidForDLC = new List<Base.Platforms.EntitlementDef>();
                Reward.Items = new ItemDef[]
                {
                    Poseidon90.GetOrCreate(),
                    Poseidon90Ammo.P90Ammo,
                };
            }
            return Reward;
        }

        private static ExistingResearchRequirementDef HavenRecruitsRequirement()
        {
            ExistingResearchRequirementDef Requirement = (ExistingResearchRequirementDef)Repo.GetDef("7581525d-225f-4d2f-8b99-092ce17dd7c2");
            if (Requirement == null)
            {
                Requirement = Repo.CreateDef<ExistingResearchRequirementDef>("7581525d-225f-4d2f-8b99-092ce17dd7c2");
                Requirement.name = "E_ExistingResearchRequirement [PX_OfficerTraining_ResearchDef]";
                Requirement.RequirementText = new LocalizedTextBind("");
                Requirement.TotalValue = 1;
                Requirement.IsRetroactive = false;
                Requirement.Id = "";
                Requirement.ResearchID = "PX_HavenRecruits_ResearchDef";
                Requirement.Tag = null;
                Requirement.Faction = null;
            }
            return Requirement;
        }

        private static ActivatedBasesResearchRequirementDef ActivatedBasesRequirement()
        {
            ActivatedBasesResearchRequirementDef Requirement = (ActivatedBasesResearchRequirementDef)Repo.GetDef("cd20e564-6c08-4ccc-8f8a-a0ffd8955a24");
            if (Requirement == null)
            {
                Requirement = Repo.CreateDef<ActivatedBasesResearchRequirementDef>("cd20e564-6c08-4ccc-8f8a-a0ffd8955a24");
                Requirement.name = "E_ActivatedBasesRequirement [PX_OfficerTraining_ResearchDef]";
                Requirement.RequirementText = new LocalizedTextBind("");
                Requirement.TotalValue = 3;
                Requirement.IsRetroactive = false;
                Requirement.Id = "";
            }
            return Requirement;
        }

        private static ResearchViewElementDef ResearchVED()
        {
            ResearchViewElementDef VED = (ResearchViewElementDef)Repo.GetDef("c05a2d88-5b0a-4954-847b-ede6ab158e6a");
            if (VED == null)
            {
                ResearchViewElementDef template = (ResearchViewElementDef)Repo.GetDef("e9845ae2-7228-e105-9659-188fd3487ae4"); //NJ_Training_ViewElementDef
                VED = Repo.CreateDef<ResearchViewElementDef>("c05a2d88-5b0a-4954-847b-ede6ab158e6a", template);
                VED.name = "E_ViewElement [PX_OfficerTraining_ResearchDef]";
                VED.DisplayName1 = new LocalizedTextBind("PX_OFFICERTRAINING_NAME");
                VED.RevealText = new LocalizedTextBind("");
                VED.UnlockText = new LocalizedTextBind("PX_OFFICERTRAINING_UNLOCK");
                VED.CompleteText = new LocalizedTextBind("PX_OFFICERTRAINING_COMPLETE");
                VED.BenefitsText = new LocalizedTextBind("PX_OFFICERTRAINING_BENEFITS");
            }
            return VED;
        }
    }
}