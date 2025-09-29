using System;
using Modules.ScenariosModule.Runtime.Scenarios;

namespace Modules.ScenariosModule.Runtime.Factories
{
    public interface IScenariosFactory
    {
        IScenario CreateScenario(Enum scenarioType);
    }
}