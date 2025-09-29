using Modules.ScenariosModule.Runtime.Models;

namespace Modules.ScenariosModule.Runtime.Tools
{
    internal static class ScenariosTools
    {
        public static readonly IScenarioActivationModel EmptyActivationModel = new EmptyScenarioActivationModel();
    }
}