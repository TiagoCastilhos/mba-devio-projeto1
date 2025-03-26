namespace SuperStore.CrossCutting.Options;

public sealed class EnvironmentOptions
{
    public required string EnvironmentName { get; init; }

    public bool IsDevelopment()
        => EnvironmentName == "Development";
}
