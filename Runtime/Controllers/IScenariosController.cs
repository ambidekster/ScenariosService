using System;
using ScenariosService.Runtime.Models;
using ScenariosService.Runtime.Scenarios;

namespace ScenariosService.Runtime.Controllers
{
    internal interface IScenariosController
    {
        IScenario GetCurrentScenario();
        
        void OpenScenario(Enum scenarioType, bool closeParentScenario);
        void OpenScenario<TModel>(Enum scenarioType, TModel activationModel, bool closeParentScenario)
                where TModel : IScenarioActivationModel;
    }
}