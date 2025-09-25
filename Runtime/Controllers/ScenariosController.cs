using System;
using System.Collections.Generic;
using ScenariosService.Runtime.Args;
using ScenariosService.Runtime.Factories;
using ScenariosService.Runtime.Models;
using ScenariosService.Runtime.Scenarios;
using ScenariosService.Runtime.Tools;

namespace ScenariosService.Runtime.Controllers
{
    internal class ScenariosController : IScenariosController
    {
        private readonly Stack<IScenario> _scenarios = new Stack<IScenario>();
        private readonly IScenariosFactory _scenariosFactory;
        
        public ScenariosController(IScenariosFactory scenariosFactory)
        {
            _scenariosFactory = scenariosFactory;
        }

        public IScenario GetCurrentScenario() => _scenarios.Count > 0 ? 
                _scenarios.Peek() : null;

        public void OpenScenario(Enum scenarioType, bool closeParentScenario)
        {
            OpenScenario(scenarioType, ScenariosTools.EmptyActivationModel, closeParentScenario);
        }

        public void OpenScenario<TModel>(Enum scenarioType, TModel activationModel, bool closeParentScenario) 
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