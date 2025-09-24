using ScenariosService.Runtime.Scenarios;

namespace ScenariosService.Runtime.Factories
{
    public interface IScenariosFactory
    {
        IScenario CreateScenario(ScenarioType scenarioType);
    }
}