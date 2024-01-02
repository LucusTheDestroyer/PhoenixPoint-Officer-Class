using Base.Defs;
using Base.Entities.Abilities;
using Base.UI;
using Officer.Misc;
using PhoenixPoint.Common.Entities;
using PhoenixPoint.Common.Entities.Characters;
using PhoenixPoint.Common.Entities.GameTags;
using PhoenixPoint.Common.Entities.GameTagsTypes;
using PhoenixPoint.Common.UI;
using PhoenixPoint.Tactical.Entities.Abilities;
using UsefulMethods;

namespace Officer.Abilities
{
    public static class OfficerSpecialisation
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        public static SpecializationDef GetOrCreate()
        {
            SpecializationDef OfficerSpec = (SpecializationDef)Repo.GetDef("983583db-a133-446a-a53c-7af2569c75a1");
            if (OfficerSpec == null)
            {
                //"SniperSpecializationDef"
                SpecializationDef SniperSpecialization = (SpecializationDef)Repo.GetDef("8b8510fe-f1cb-53b4-3a85-3a306c94e31f");

                OfficerSpec = Repo.CreateDef<SpecializationDef>("983583db-a133-446a-a53c-7af2569c75a1",SniperSpecialization);
                OfficerSpec.name = "OfficerSpecializationDef";
                OfficerSpec.ResourcePath = "Defs/Common/TacUnitClasses/SpecializationDef/OffcerSpecializationDef";
                OfficerSpec.AchievementID = "";
                OfficerSpec.ViewElementDef = SpecialisationVED(SniperSpecialization.ViewElementDef);
                OfficerSpec.ClassFilterText.LocalizationKey = "OFFICER_FILTER";
                OfficerSpec.ClassTag = Tags.OfficerClassTag();
                OfficerSpec.AbilityTrack = OfficerAbilityTrack();
                OfficerSpec.IsEliteUnit = true;
            }
            return OfficerSpec;
        }

        private static AbilityTrackDef OfficerAbilityTrack()
        {
            AbilityTrackDef OfficerTrack = (AbilityTrackDef)Repo.GetDef("9bafe3ad-123c-46c8-b659-1f820497e235");
            if (OfficerTrack == null)
            {
                OfficerTrack = Repo.CreateDef<AbilityTrackDef>("9bafe3ad-123c-46c8-b659-1f820497e235");
                OfficerTrack.name = "E_AbilityTrack [OfficerSpecializationDef]";
                OfficerTrack.ResourcePath = "Defs/Common/TacUnitClasses/SpecializationDef/OffcerSpecializationDef";
                OfficerTrack.AbilitiesByLevel = new AbilityTrackSlot[]
                {
                    new AbilityTrackSlot
                    {
                        Ability = OfficerClassProficiency.GetOrCreate(), 
                        RequiresPrevAbility = false
                    },
                    new AbilityTrackSlot
                    {
                        Ability = TriggerDiscipline.GetOrCreate(),
                        RequiresPrevAbility = false
                    },
                    new AbilityTrackSlot
                    {
                        Ability = RallyAbility.GetOrCreate(),
                        RequiresPrevAbility = false
                    },
                    new AbilityTrackSlot
                    {
                        Ability = null,
                        RequiresPrevAbility = false
                    },
                    new AbilityTrackSlot
                    {
                        Ability = FieldCommander.GetOrCreate(),
                        RequiresPrevAbility = false
                    },
                    new AbilityTrackSlot
                    {
                        Ability = (ApplyStatusAbilityDef)Repo.GetDef("175744da-5e1d-d1d4-58fb-b08d226b58f6"), //"DeterminedAdvance_AbilityDef"
                        RequiresPrevAbility = false
                    },
                    new AbilityTrackSlot
                    {
                        Ability = (ApplyStatusAbilityDef)Repo.GetDef("43d3e67f-da6a-b0e4-f8bf-3294ec5771b8"), //"MarkedForDeath_AbilityDef"
                        RequiresPrevAbility = false
                    },
                };
            }
            return OfficerTrack;
        }
        private static ViewElementDef SpecialisationVED(ViewElementDef template)
        {
            ViewElementDef VED = (ViewElementDef)Repo.GetDef("4fb4f27e-9d70-4e56-9b83-96fceac33592");
            if (VED == null)
            {
                VED = Repo.CreateDef<ViewElementDef>("4fb4f27e-9d70-4e56-9b83-96fceac33592", template);
                VED.name = "E_ViewElement [OfficerSpecializationDef]";
                VED.Name = "Officer";
                VED.DisplayName1 = new LocalizedTextBind("CLASS_OFFICER_NAME"); 
                VED.DisplayName2 = new LocalizedTextBind("OFFICER_DESCRIPTION");
                VED.Description = new LocalizedTextBind("CLASS_OFFICER_DESCRIPTION");
                VED.SmallIcon = VED.LargeIcon = Helper.CreateSpriteFromImageFile("OfficerIcon_NoOutline.png");
            }
            return VED;
        }
    }
}