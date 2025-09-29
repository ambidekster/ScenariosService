using System;
using Modules.ScenariosModule.Runtime.Models;

namespace Modules.ScenariosModule.Runtime.Args
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