using BepInEx;
using Bepinject;
using ComputerInterface;
using Grippy;
using System.Reflection;
using HarmonyLib;

namespace Grippy
{
    [BepInPlugin("org.ivy.gtag.gripmonke", "GripMonke", "1.1")]
    public class Plugin : BaseUnityPlugin
    {
        public static string modStatus = "Off";
        public void Awake()
        {
            var harmony = new Harmony("com.ivy.gtag.gripmonke");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Zenjector.Install<GripInstaller>().OnProject();
        }
        
        public void onEnable()
        {
            modStatus = "On";
        }
    
        public void onDisable()
        {
            modStatus = "Off";
        }
    }
}
