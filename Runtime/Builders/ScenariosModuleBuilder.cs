using System;
using Modules.ScenariosModule.Runtime.Controllers;
using Modules.ScenariosModule.Runtime.Factories;
using UnityEngine;

namespace Modules.ScenariosModule.Runtime.Builders
{
    public class ScenariosModuleBuilder : IScenariosModuleBuilder
    {
        private IScenariosFactory _factory;
        private Enum _startScenarioType;
        
        public IScenariosModuleBuilder WithFactory(IScenariosFactory factory)
        {
            _factory = factory;
            return this;
        }

        public IScenariosModuleBuilder WithStartScenario(Enum scenarioType)
        {
            _startScenarioType = scenarioType;
            return this;
        }

        public IStartScenarioLauncher Build()
        {
            if(_factory == null)
            {
                Debug.LogException(new Exception($"Invalid scenarios factory!"));
                return null;
            }
            
            if(_startScenarioType.Equals(null))
            {
                Debug.LogException(new Exception($"Invalid start scenario type!"));
                return null;
            }
            
            return new StartScenarioLauncher(_startScenarioType, new ScenariosController(_factory));
        }
    }
}