using BepInEx;
using Bepinject;
using System.Reflection;
using HarmonyLib;
using Utilla;
using System.ComponentModel;

namespace Grippy
{
    [Description("HauntedModMenu")]
    [BepInPlugin("org.ivy.gtag.gripmonke", "GripMonke", "2.1.1")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        private static Harmony harmony;
        static bool inAllowedRoom = false;

        public void OnEnable() // Capitalization on these methods matters
        {
            harmony = new Harmony("com.ivy.gtag.gripmonke");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnDisable()
        {
            harmony.UnpatchSelf();
        }

        [ModdedGamemodeJoin]
        public void RoomJoined(string gamemode)
        {
            // The room is modded. Enable mod stuff.
            inAllowedRoom = true;
        }

        [ModdedGamemodeLeave]
        public void RoomLeft(string gamemode)
        {
            // The room was left. Disable mod stuff.
            inAllowedRoom = false;
        }

        [HarmonyPatch(typeof(GorillaLocomotion.Player), "GetSlidePercentage")]
        class slidepatch
        {
            static void Postfix(ref float __result)
            {
                if (inAllowedRoom)
                {
                    __result = 0.03f;
                }
            }
        }
    }
}
