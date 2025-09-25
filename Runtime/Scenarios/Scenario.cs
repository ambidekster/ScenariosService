using System;
using ScenariosService.Runtime.Args;
using ScenariosService.Runtime.Models;
using ScenariosService.Runtime.Tools;

namespace ScenariosService.Runtime.Scenarios
{
    public abstract class Scenario<TModel> : IScenario 
            where TModel : IScenarioActivationModel
    {
        public event EventHandler CloseScenarioRequested;
        public event EventHandler<OpenScenarioRequestEventArgs> OpenScenarioRequested;
        
        public ScenarioState State { get; private set; }

        protected TModel Model { get; private set; }

        public void Activate(IScenarioActivationModel model)
        {
            State = ScenarioState.Active;
            Model = (TModel)model;
            OnActivate(Model);
        }

        protected virtual void OnActivate(IScenarioActivationModel model)
        {
        }

        public void Pause()
        {
            State = ScenarioState.Pause;
            OnPause();
        }
        
        protected virtual void OnPause()
        {
        }

        public void Resume()
        {
            State = ScenarioState.Active;
            OnResume();
        }

        protected virtual void OnResume()
        {
        }
        
        public void Close()
        {
            OnClose();
        }
        
        protected virtual void OnClose()
        {
        }
        
        protected void OpenScenario(Enum scenarioType, 
                                    bool closeParentScenario)
        {
            OpenScenario(scenarioType, ScenariosTools.EmptyActivationModel, closeParentScenario);
        }
        
        protected void OpenScenario(Enum scenarioType, 
                                    IScenarioActivationModel activationModel, 
                                    bool closeParentScenario)
        {
            OpenScenarioRequested?.Invoke(this, 
                    new OpenScenarioRequestEventArgs(scenarioType, activationModel, closeParentScenario));
        }

        protected void CloseScenario()
        {
            CloseScenarioRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}