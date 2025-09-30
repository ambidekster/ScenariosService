using System;
using Modules.ScenariosModule.Runtime.Args;
using Modules.ScenariosModule.Runtime.Models;

namespace Modules.ScenariosModule.Runtime.Scenarios
{
    public interface IScenario
    {
        event EventHandler CloseScenarioRequested;
        event EventHandler<OpenScenarioRequestEventArgs> OpenScenarioRequested;
        
        Enum Type { get; }
        ScenarioState State { get; }

        void Activate(IScenarioActivationModel model);
        void Close();
        
        void Pause();
        void Resume();
    }
}