using System;
using ScenariosService.Runtime.Scenarios;

namespace ScenariosService.Runtime.Factories
{
    public interface IScenariosFactory
    {
        IScenario CreateScenario(Enum scenarioType);
    }
}