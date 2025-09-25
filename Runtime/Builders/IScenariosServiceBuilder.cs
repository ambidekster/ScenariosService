using System;
using ScenariosService.Runtime.Factories;

namespace ScenariosService.Runtime.Builders
{
    public interface IScenariosServiceBuilder
    {
        public IScenariosServiceBuilder WithFactory(IScenariosFactory factory);
        public IScenariosServiceBuilder WithStartScenario(Enum scenarioType);
        
        public void Build();
    }
}