using ScenariosService.Runtime.Models;
using ScenariosService.Runtime.Scenarios;

namespace ScenariosService.Runtime.Controllers
{
    public interface IScenariosController
    {
        IScenario GetCurrentScenario();
        
        void OpenScenario<TModel>(ScenarioType scenarioType, TModel activationModel, bool closeParentScenario)
                where TModel : IScenarioActivationModel;
    }
}