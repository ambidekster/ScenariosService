using Modules.ScenariosModule.Models;

namespace Modules.ScenariosModule
{
    public interface IScenariosController
    {
        IScenario GetCurrentScenario();
        
        void OpenScenario<TModel>(ScenarioType scenarioType, TModel activationModel, bool closeParentScenario)
                where TModel : IScenarioActivationModel;
    }
}