using Modules.Match3Module.Controllers;
using Modules.Match3Module.Providers;
using Modules.ScenariosModule.SpecificScenarios;
using Modules.WindowsModule;
using UnityEngine;

namespace Modules.ScenariosModule.Factories
{
    public class ScenariosFactory : IScenariosFactory
    {
        private readonly IWindowsDisplayController _windowsDisplayController;
        private readonly ILevelWorldController _levelWorldController;
        private readonly ILevelsProvider _levelsProvider;

        public ScenariosFactory(IWindowsDisplayController windowsDisplayController,
                                ILevelWorldController levelWorldController,
                                ILevelsProvider levelsProvider)
        {
            _windowsDisplayController = windowsDisplayController;
            _levelWorldController = levelWorldController;
            _levelsProvider = levelsProvider;
        }

        // Todo: Create from DI
        public IScenario CreateScenario(ScenarioType scenarioType)
        {
            switch(scenarioType)
            {
                case ScenarioType.MainMenu:
                    return new MainMenuScenario(_windowsDisplayController, _levelsProvider);
                
                case ScenarioType.Level:
                    return new LevelScenario(_windowsDisplayController, _levelWorldController, _levelsProvider);
                
                case ScenarioType.LevelEnd:
                    return new LevelEndScenario(_windowsDisplayController, _levelsProvider);
                
                default:
                    Debug.LogError($"Invalid scenario type: {scenarioType}");
                    return null;
            }
        }
    }
}