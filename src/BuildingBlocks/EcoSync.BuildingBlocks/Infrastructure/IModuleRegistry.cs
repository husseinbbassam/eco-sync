namespace EcoSync.BuildingBlocks.Infrastructure;

public interface IModuleRegistry
{
    void Register(string moduleName, IModuleConfiguration configuration);
    IModuleConfiguration? GetModule(string moduleName);
}

public interface IModuleConfiguration
{
    string ModuleName { get; }
}
