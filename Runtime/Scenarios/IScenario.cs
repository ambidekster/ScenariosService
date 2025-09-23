using System;
using Modules.ScenariosModule.Models;

namespace Modules.ScenariosModule
{
    public interface IScenario
    {
        event EventHandler CloseScenarioRequested;
        event EventHandler<OpenScenarioRequestEventArgs> OpenScenarioRequested;
        
        ScenarioType ScenarioType { get; }
        ScenarioState State { get; }

        void Activate(IScenarioActivationModel model);
        void Close();
        
        void Pause();
        void Resume();
    }
}