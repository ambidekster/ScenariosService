using Modules.ScenariosModule.Factories;
using VContainer;

namespace Modules.ScenariosModule.Installers
{
    public static class ScenariosModuleInstaller
    {
        public static void Bind(IContainerBuilder builder)
        {
            builder.Register<IScenariosFactory, ScenariosFactory>(Lifetime.Singleton);
            builder.Register<IScenariosController, ScenariosController>(Lifetime.Singleton);
        }
    }
}