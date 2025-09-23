using Modules.ScenariosModule.Models;

namespace Modules.ScenariosModule.SpecificScenarios
{
    public class LevelEndScenarioActivationModel : IScenarioActivationModel
    {
        public int LevelNumber { get; }
        public bool IsCompleted { get; }

        public LevelEndScenarioActivationModel(int levelNumber, bool isCompleted)
        {
            LevelNumber = levelNumber;
            IsCompleted = isCompleted;
        }
    }
}