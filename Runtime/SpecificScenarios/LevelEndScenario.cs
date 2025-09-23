using Modules.Match3Module.Providers;
using Modules.ScenariosModule.Models;
using Modules.WindowsModule;
using Modules.WindowsModule.Views;
using Modules.WindowsModule.Views.SpecificViews.LevelCompleted;
using Modules.WindowsModule.Views.SpecificViews.LevelFailed;

namespace Modules.ScenariosModule.SpecificScenarios
{
    public class LevelEndScenario : Scenario<LevelEndScenarioActivationModel>
    {
        public override ScenarioType ScenarioType => ScenarioType.LevelEnd;
     
        private readonly ILevelsProvider _levelsProvider;
        
        // Todo: Move to base scenario?
        private IWindowView _windowView;

        private int _levelNumber;
        
        public LevelEndScenario(IWindowsDisplayController windowsDisplayController, 
                                ILevelsProvider levelsProvider) 
                : base(windowsDisplayController)
        {
            _levelsProvider = levelsProvider;
        }

        protected override void OnActivate(IScenarioActivationModel model)
        {
            base.OnActivate(model);

            var scenarioModel = (LevelEndScenarioActivationModel)model;
            _levelNumber = scenarioModel.LevelNumber;
                    
            _windowView = scenarioModel.IsCompleted ? 
                    WindowsDisplayController.ShowWindow(WindowType.LevelCompleted,
                            new LevelCompletedWindowParameters(_levelNumber,
                                    HandleNextLevelButtonClicked, HandleCloseButtonClicked)) : 
                    WindowsDisplayController.ShowWindow(WindowType.LevelFailed,
                            new LevelFailedWindowParameters(HandleRetryLevelButtonClicked, HandleCloseButtonClicked));
        }
        
        
        private void HandleCloseButtonClicked()
        {
            GoToMainMenu();
        }
        
        private void HandleNextLevelButtonClicked()
        {
            var nextLevelNumber = _levelNumber + 1;
            if(nextLevelNumber <= _levelsProvider.LevelsCount)
            {
                StartLevel(nextLevelNumber);
            }
            else
            {
                GoToMainMenu();
            }
        }
        
        private void HandleRetryLevelButtonClicked()
        {
            StartLevel(_levelNumber);
        }

        private void StartLevel(int levelNumber)
        {
            OpenScenario(ScenarioType.Level, new LevelScenarioActivationModel(levelNumber), true);
        }

        private void GoToMainMenu()
        {
            OpenScenario(ScenarioType.MainMenu, new MainMenuScenarioActivationModel(), true);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            
            WindowsDisplayController.HideWindow(_windowView);
        }
    }
}