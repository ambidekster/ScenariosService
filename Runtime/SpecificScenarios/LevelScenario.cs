using System;
using Modules.Match3Module.Controllers;
using Modules.Match3Module.Events;
using Modules.Match3Module.Providers;
using Modules.ScenariosModule.Models;
using Modules.WindowsModule;
using Modules.WindowsModule.Views;
using Modules.WindowsModule.Views.SpecificViews.Level;
using UnityEngine;

namespace Modules.ScenariosModule.SpecificScenarios
{
    public class LevelScenario : Scenario<LevelScenarioActivationModel>
    {
        public override ScenarioType ScenarioType => ScenarioType.Level;

        private readonly ILevelWorldController _levelWorldController;
        private readonly ILevelsProvider _levelsProvider;

        private IWindowView _windowView;

        private int _levelNumber;
        
        // Todo: LevelController not single?
        public LevelScenario(IWindowsDisplayController windowsDisplayController,
                             ILevelWorldController levelWorldController,
                             ILevelsProvider levelsProvider) 
                : base(windowsDisplayController)
        {
            _levelWorldController = levelWorldController;
            _levelsProvider = levelsProvider;
        }

        protected override void OnActivate(IScenarioActivationModel model)
        {
            base.OnActivate(model);

            var scenarioModel = (LevelScenarioActivationModel)model;

            _levelNumber = scenarioModel.LevelNumber;
            var levelModel = _levelsProvider.GetLevelModel(_levelNumber);
            if(levelModel == null)
            {
                return;
            }
            
            _levelWorldController.EventsDispatcher.
                                  Subscribe<LevelEndEventArgs>(HandleMatch3Event);
            
            _levelWorldController.Start(levelModel);
            
            _windowView = WindowsDisplayController.ShowWindow(WindowType.Level, 
                    new LevelWindowParameters(HandleCloseButtonClicked, 
                            _levelNumber, levelModel, _levelWorldController.ProgressModel));
        }

        private void HandleMatch3Event(LevelEndEventArgs args)
        {
            OnLevelEnd(_levelNumber, args.IsCompleted);
        }

        private void OnLevelEnd(int levelNumber, bool isCompleted)
        {
            OpenScenario(ScenarioType.LevelEnd, 
                    new LevelEndScenarioActivationModel(levelNumber, isCompleted), true);
        }

        private void HandleCloseButtonClicked()
        {
            // Todo: add empty scenario model?
            OpenScenario(ScenarioType.MainMenu, new MainMenuScenarioActivationModel(), true);
        }
        

        protected override void OnClose()
        {
            base.OnClose();
            
            _levelWorldController.EventsDispatcher.Unsubscribe<LevelEndEventArgs>(HandleMatch3Event);
            
            _levelWorldController.Reset();
            WindowsDisplayController.HideWindow(_windowView);
        }
    }
}