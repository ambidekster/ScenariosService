using Modules.Match3Module.Providers;
using Modules.ScenariosModule.Models;
using Modules.WindowsModule;
using Modules.WindowsModule.Views;
using Modules.WindowsModule.Views.SpecificViews.MainMenu;

namespace Modules.ScenariosModule.SpecificScenarios
{
    public class MainMenuScenario : Scenario<MainMenuScenarioActivationModel>
    {
        public override ScenarioType ScenarioType => ScenarioType.MainMenu;

        private readonly ILevelsProvider _levelsProvider;
        
        private IWindowView _windowView;
        
        public MainMenuScenario(IWindowsDisplayController windowsDisplayController, 
                                ILevelsProvider levelsProvider) 
                : base(windowsDisplayController)
        {
            _levelsProvider = levelsProvider;
        }

        protected override void OnActivate(IScenarioActivationModel model)
        {
            base.OnActivate(model);
            
            _windowView = WindowsDisplayController.ShowWindow(WindowType.MainMenu, 
                    new MainMenuWindowParameters(
                            HandleLevelButtonClicked, _levelsProvider.LevelsCount));
        }
        
        
        private void HandleLevelButtonClicked(int levelNumber)
        {
            OpenScenario(ScenarioType.Level, 
                    new LevelScenarioActivationModel(levelNumber), true);
        }
        

        protected override void OnClose()
        {
            base.OnClose();
            WindowsDisplayController.HideWindow(_windowView);
        }
    }
}