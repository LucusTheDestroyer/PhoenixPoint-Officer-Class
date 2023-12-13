using Base.Defs;
using PhoenixPoint.Common.Entities.GameTagsTypes;

namespace Officer.Misc
{
    public static class NewTags
    {
        private static readonly DefRepository Repo = ModHandler.Repo;
        
        public static ClassTagDef OfficerClassTag()
        {
            ClassTagDef OfficerTag = (ClassTagDef)Repo.GetDef("637a5db1-ba13-4cfd-a988-c332878bb36c");
            if (OfficerTag == null)
            {
                OfficerTag = Repo.CreateDef<ClassTagDef>("637a5db1-ba13-4cfd-a988-c332878bb36c");
                OfficerTag.name = "Officer_ClassTagDef";
                OfficerTag.ResourcePath = "Defs/GameTags/Classes/Officer_ClassTagDef";
            }
            return OfficerTag;
        }
    }
}