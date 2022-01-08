using BepInEx;
using Bepinject;
using System.Reflection;
using HarmonyLib;
using Utilla;

namespace Grippy
{
    [BepInPlugin("org.ivy.gtag.gripmonke", "GripMonke", "1.1")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
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
