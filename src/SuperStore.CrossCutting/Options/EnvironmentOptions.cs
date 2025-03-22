using SuperStore.CrossCutting.Enums;

namespace SuperStore.CrossCutting.Options;

public sealed class EnvironmentOptions
{
    public EnvironmentType EnvironmentType { get; init; }

    public bool IsDevelopment()
        => EnvironmentType == EnvironmentType.Development;
}
