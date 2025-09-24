using System;
using ScenariosService.Runtime.Models;
using ScenariosService.Runtime.Scenarios;

namespace ScenariosService.Runtime.Args
{
    public class OpenScenarioRequestEventArgs : EventArgs
    {
        public Enum ScenarioType { get; }
        public IScenarioActivationModel ActivationModel { get; }
        public bool CloseParentScenario { get; }

        public OpenScenarioRequestEventArgs(Enum scenarioType, 
                                            IScenarioActivationModel activationModel, 
                                            bool closeParentScenario)
        {
            ScenarioType = scenarioType;
            ActivationModel = activationModel;
            CloseParentScenario = closeParentScenario;
        }
    }
}