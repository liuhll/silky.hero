namespace Silky.BasicData.Domain.Shared;

public class BasicDataPermissions
{
    private const string GroupName = "BasicData";
    
    public static class Dictionaries
    {
        public const string Default = GroupName + ".Dictionary";
      
        public static class Types
        {
            public const string Default = Dictionaries.Default + ".Type";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string LookDetail = Default + ".LookDetail";
        }
        
        public static class Items
        {
            public const string Default = Dictionaries.Default + ".Item";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string LookDetail = Default + ".LookDetail";
        }
    }
}