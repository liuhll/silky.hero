namespace Silky.Identity.Domain;

public static class IdentityUserConsts
{
    public static int MaxUserNameLength { get; set; } = 256;

    public static int MaxRealNameLength { get; set; } = 64;

    public static int MaxSurnameLength { get; set; } = 64;

    public static int MaxNormalizedUserNameLength { get; set; } = MaxUserNameLength;

    public static int MaxEmailLength { get; set; } = 256;

    public static int MaxNormalizedEmailLength { get; set; } = MaxEmailLength;

    public static int MaxPhoneNumberLength { get; set; } = 16;
    
    public static int MaxPasswordLength { get; set; } = 128;
    
    public static int MaxPasswordHashLength { get; set; } = 256;
    
    public static int MaxSecurityStampLength { get; set; } = 256;
    
    public static int MaxLoginProviderLength { get; set; } = 16;  
}