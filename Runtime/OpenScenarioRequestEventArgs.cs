using System;
using Modules.ScenariosModule.Models;

namespace Modules.ScenariosModule
{
    public class OpenScenarioRequestEventArgs : EventArgs
    {
        public ScenarioType ScenarioType { get; }
        public IScenarioActivationModel ActivationModel { get; }
        public bool CloseParentScenario { get; }

        public OpenScenarioRequestEventArgs(ScenarioType scenarioType, 
                                            IScenarioActivationModel activationModel, 
                                            bool closeParentScenario)
        {
            ScenarioType = scenarioType;
            ActivationModel = activationModel;
            CloseParentScenario = closeParentScenario;
        }
    }
}