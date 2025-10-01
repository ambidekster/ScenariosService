using System;
using System.Collections.Generic;
using Modules.ScenariosModule.Runtime.Args;
using Modules.ScenariosModule.Runtime.Factories;
using Modules.ScenariosModule.Runtime.Models;
using Modules.ScenariosModule.Runtime.Scenarios;
using Modules.ScenariosModule.Runtime.Tools;

namespace Modules.ScenariosModule.Runtime.Controllers
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
            OpenScenario(scenarioType, ScenariosTools.EmptyStartModel, closeParentScenario);
        }

        public void OpenScenario<TModel>(Enum scenarioType, TModel startModel, bool closeParentScenario) 
                where TModel : IScenarioStartModel
        {
            var scenarioToOpen = _scenariosFactory.CreateScenario(scenarioType);
            if(scenarioToOpen != null)
            {
                DeactivateCurrentScenario(closeParentScenario);
                
                scenarioToOpen.OpenScenarioRequested += HandleOpenScenarioRequested;
                scenarioToOpen.CloseScenarioRequested += HandleCloseScenarioRequested;
                scenarioToOpen.Start(startModel);
                
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
            
            OpenScenario(e.ScenarioType, e.StartModel, e.CloseParentScenario);
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