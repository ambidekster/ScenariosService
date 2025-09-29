using System;
using Modules.ScenariosModule.Runtime.Models;
using Modules.ScenariosModule.Runtime.Scenarios;

namespace Modules.ScenariosModule.Runtime.Controllers
{
    internal interface IScenariosController
    {
        IScenario GetCurrentScenario();
        
        void OpenScenario(Enum scenarioType, bool closeParentScenario);
        void OpenScenario<TModel>(Enum scenarioType, TModel activationModel, bool closeParentScenario)
                where TModel : IScenarioActivationModel;
    }
}