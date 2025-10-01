using System;
using Modules.ScenariosModule.Runtime.Args;
using Modules.ScenariosModule.Runtime.Models;
using Modules.ScenariosModule.Runtime.Tools;

namespace Modules.ScenariosModule.Runtime.Scenarios
{
    public abstract class Scenario<TModel> : IScenario 
            where TModel : IScenarioStartModel
    {
        public event EventHandler CloseScenarioRequested;
        public event EventHandler<OpenScenarioRequestEventArgs> OpenScenarioRequested;
        
        public ScenarioState State { get; private set; }

        protected TModel Model { get; private set; }

        public void Start(IScenarioStartModel model)
        {
            State = ScenarioState.Active;
            Model = (TModel)model;
            OnStart(Model);
        }

        protected virtual void OnStart(TModel model)
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
            OpenScenario(scenarioType, ScenariosTools.EmptyStartModel, closeParentScenario);
        }
        
        protected void OpenScenario(Enum scenarioType, 
                                    IScenarioStartModel startModel, 
                                    bool closeParentScenario)
        {
            OpenScenarioRequested?.Invoke(this, 
                    new OpenScenarioRequestEventArgs(scenarioType, startModel, closeParentScenario));
        }

        protected void CloseScenario()
        {
            CloseScenarioRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}