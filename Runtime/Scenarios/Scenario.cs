using System;
using Modules.ScenariosModule.Models;
using Modules.WindowsModule;

namespace Modules.ScenariosModule
{
    public abstract class Scenario<TModel> : IScenario 
            where TModel : IScenarioActivationModel
    {
        public event EventHandler CloseScenarioRequested;
        public event EventHandler<OpenScenarioRequestEventArgs> OpenScenarioRequested;
        
        // Todo: Remove?
        public abstract ScenarioType ScenarioType { get; }
        
        public ScenarioState State { get; private set; }

        protected TModel Model { get; private set; }

        protected IWindowsDisplayController WindowsDisplayController { get; }

        protected Scenario(IWindowsDisplayController windowsDisplayController)
        {
            WindowsDisplayController = windowsDisplayController;
        }

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
        
        protected void OpenScenario(ScenarioType scenarioType, 
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