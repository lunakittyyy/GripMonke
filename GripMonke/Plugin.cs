using BepInEx;
using Bepinject;
using ComputerInterface;
using Grippy;
using System.Reflection;
using HarmonyLib;

namespace Grippy
{
    [BepInPlugin("org.ivy.gtag.gripmonke", "GrippyMonke", "1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.ivy.gtag.gripmonke");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Zenjector.Install<GripInstaller>().OnProject();
        }
    }
}
