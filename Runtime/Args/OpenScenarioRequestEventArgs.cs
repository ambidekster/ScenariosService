using System;
using Modules.ScenariosModule.Runtime.Models;

namespace Modules.ScenariosModule.Runtime.Args
{
    public class OpenScenarioRequestEventArgs : EventArgs
    {
        public Enum ScenarioType { get; }
        public IScenarioStartModel StartModel { get; }
        public bool CloseParentScenario { get; }

        public OpenScenarioRequestEventArgs(Enum scenarioType, 
                                            IScenarioStartModel startModel, 
                                            bool closeParentScenario)
        {
            ScenarioType = scenarioType;
            StartModel = startModel;
            CloseParentScenario = closeParentScenario;
        }
    }
}