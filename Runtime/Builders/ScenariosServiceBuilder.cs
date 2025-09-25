using System;
using ScenariosService.Runtime.Controllers;
using ScenariosService.Runtime.Factories;
using ScenariosService.Runtime.Models;
using UnityEngine;

namespace ScenariosService.Runtime.Builders
{
    public class ScenariosServiceBuilder : IScenariosServiceBuilder
    {
        private IScenariosFactory _factory;
        private Enum _startScenarioType;
        
        public IScenariosServiceBuilder WithFactory(IScenariosFactory factory)
        {
            _factory = factory;
            return this;
        }

        public IScenariosServiceBuilder WithStartScenario(Enum scenarioType)
        {
            _startScenarioType = scenarioType;
            return this;
        }

        public void Build()
        {
            if(_factory == null)
            {
                Debug.LogException(new Exception($"Invalid scenarios factory!"));
                return;
            }
            
            if(_startScenarioType.Equals(null))
            {
                Debug.LogException(new Exception($"Invalid start scenario type!"));
                return;
            }
            
            new ScenariosController(_factory).OpenScenario(
                    _startScenarioType, new EmptyScenarioActivationModel(), false);
        }
    }
}