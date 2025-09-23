using Modules.ScenariosModule.Models;

namespace Modules.ScenariosModule.SpecificScenarios
{
    public class LevelScenarioActivationModel : IScenarioActivationModel
    {
        public int LevelNumber { get; }

        public LevelScenarioActivationModel(int levelNumber)
        {
            LevelNumber = levelNumber;
        }
    }
}