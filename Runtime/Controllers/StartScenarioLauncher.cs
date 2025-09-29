using System;

namespace Modules.ScenariosModule.Runtime.Controllers
{
    internal class StartScenarioLauncher : IStartScenarioLauncher
    {
        private readonly Enum _scenarioType;
        private readonly IScenariosController _scenariosController;

        public StartScenarioLauncher(Enum scenarioType, IScenariosController scenariosController)
        {
            _scenarioType = scenarioType;
            _scenariosController = scenariosController;
        }

        public void Start()
        {
            _scenariosController.OpenScenario(_scenarioType, false);
        }
    }
}