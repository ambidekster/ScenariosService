using System;
using Modules.ScenariosModule.Runtime.Factories;

namespace Modules.ScenariosModule.Runtime.Builders
{
    public interface IScenariosModuleBuilder
    {
        public IScenariosModuleBuilder WithFactory(IScenariosFactory factory);
        public IScenariosModuleBuilder WithStartScenario(Enum scenarioType);
        
        public void Build();
    }
}