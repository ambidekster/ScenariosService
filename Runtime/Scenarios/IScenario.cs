using System;
using ScenariosService.Runtime.Args;
using ScenariosService.Runtime.Models;

namespace ScenariosService.Runtime.Scenarios
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