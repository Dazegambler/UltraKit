using ULTRAKIT.Loader;

namespace ULTRAKIT.Core.ModMenu
{
    public static class ModMenuInjector
    {


        public static void Initialize()
        {
            AddonLoader.harmony.PatchAll(typeof(OptionsMenuToManagerPatch));
        }


    }
}
