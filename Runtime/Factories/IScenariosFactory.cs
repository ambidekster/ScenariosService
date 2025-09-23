namespace Modules.ScenariosModule.Factories
{
    public interface IScenariosFactory
    {
        IScenario CreateScenario(ScenarioType scenarioType);
    }
}