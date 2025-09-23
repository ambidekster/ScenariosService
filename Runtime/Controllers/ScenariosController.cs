using System;
using System.Collections.Generic;
using Modules.ScenariosModule.Factories;
using Modules.ScenariosModule.Models;
using Modules.WindowsModule;

namespace Modules.ScenariosModule
{
    public class ScenariosController : IScenariosController
    {
        private readonly Stack<IScenario> _scenarios = new Stack<IScenario>();
        private readonly IScenariosFactory _scenariosFactory;
        
        public ScenariosController(IScenariosFactory scenariosFactory)
        {
            _scenariosFactory = scenariosFactory;
        }

        public IScenario GetCurrentScenario() => _scenarios.Count > 0 ? 
                _scenarios.Peek() : null;
        
        public void OpenScenario<TModel>(ScenarioType scenarioType, TModel activationModel, bool closeParentScenario) 
                where TModel : IScenarioActivationModel
        {
            var scenarioToOpen = _scenariosFactory.CreateScenario(scenarioType);
            if(scenarioToOpen != null)
            {
                DeactivateCurrentScenario(closeParentScenario);
                
                scenarioToOpen.OpenScenarioRequested += HandleOpenScenarioRequested;
                scenarioToOpen.CloseScenarioRequested += HandleCloseScenarioRequested;
                scenarioToOpen.Activate(activationModel);
                
                _scenarios.Push(scenarioToOpen);   
            }
        }

        // Todo: add scenario mode?
        private void DeactivateCurrentScenario(bool closeParentScenario)
        {
            var currentScenario = GetCurrentScenario();
            if(currentScenario != null)
            {
                if(closeParentScenario)
                {
                    currentScenario.OpenScenarioRequested -= HandleOpenScenarioRequested;
                    currentScenario.CloseScenarioRequested -= HandleCloseScenarioRequested;
                    currentScenario.Close();
                    
                    _scenarios.Pop();
                }
                else
                {
                    currentScenario.Pause();
                }
            }
        }

        private void ResumeParentScenario()
        {
            var currentScenario = GetCurrentScenario();
            if(currentScenario != null &&
               currentScenario.State == ScenarioState.Pause)
            {
                currentScenario.Resume();
            }
        }

        private void HandleOpenScenarioRequested(object sender, OpenScenarioRequestEventArgs e)
        {
            var scenario = (IScenario)sender;
            if(scenario.State != ScenarioState.Active)
            {
                return;
            }
            
            OpenScenario(e.ScenarioType, e.ActivationModel, e.CloseParentScenario);
        }
        
        private void HandleCloseScenarioRequested(object sender, EventArgs e)
        {
            var scenario = (IScenario)sender;
            if(scenario.State != ScenarioState.Active)
            {
                return;
            }
            
            DeactivateCurrentScenario(true);
            ResumeParentScenario();
        }
    }
}