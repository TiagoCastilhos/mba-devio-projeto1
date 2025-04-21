namespace SuperStore.Core.Options;
internal sealed class IdentityOptions
{
    public const string SectionName = "Identity";
    public string SigningKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int TokenExpirationInHours { get; set; } = 8;
}
