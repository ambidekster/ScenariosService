using ScenariosService.Runtime.Models;

namespace ScenariosService.Runtime.Tools
{
    internal static class ScenariosTools
    {
        public static readonly IScenarioActivationModel EmptyActivationModel = new EmptyScenarioActivationModel();
    }
}