using System;
using HarmonyLib;
using Photon.Pun;
using ComputerInterface;
using ComputerInterface.ViewLib;
using Utilla;
using BepInEx;

namespace Grippy
{

    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]

    public class GripView : ComputerView
    {
        static string modStatus = "Off";
        bool inAllowedRoom = false;
        static string TextBase;

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
            modStatus = "Off";
        }

        public override void OnShow(object[] args)
        {



            Console.WriteLine(inAllowedRoom);

            base.OnShow(args); ;
            if (inAllowedRoom == true)
            {
                TextBase = "GripMonke 1.2.1 by Lillie#8947\nOption 1 to make slippery walls not slip\nOption 2 to make slippery walls slippery\nMod is ";
                Text = TextBase + modStatus + "Room allowed? " + inAllowedRoom;
            }
            else
            {
                modStatus = "Off";
                ReturnToMainMenu();
            }
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            switch (key)
            {
                case EKeyboardKey.Delete:
                    ReturnToMainMenu();
                    //Console.WriteLine("Returned to menu due to delete button press");
                    break;
                case EKeyboardKey.Option1:
                    modStatus = "On";
                    Console.WriteLine("We aren't, mod status is now: " + modStatus);
                    Text = TextBase + modStatus;
                    //Console.WriteLine("Updated text to: " + Text);
                    break;
                case EKeyboardKey.Option2:
                    modStatus = "Off";
                    //Console.WriteLine("Mod status is now: " + modStatus);
                    Text = TextBase + modStatus;
                    //Console.WriteLine("Updated text to: " + Text);
                    break;
            }
        }

        [HarmonyPatch(typeof(GorillaLocomotion.Player), "GetSlidePercentage")]
        class slidepatch
        {
            static void Postfix(ref float __result)
            {
                //Console.WriteLine("Entered postfix");
                //Console.WriteLine("Are we even connected to the servers yet? " + PhotonNetwork.IsConnected);
                if (PhotonNetwork.IsConnected == true)
                {
                    if (PhotonNetwork.InRoom == true)
                    {
                        //Console.WriteLine("Are we in a public room? " + PhotonNetwork.CurrentRoom.IsVisible);
                        // if (inAllowedRoom)
                        //{
                            //Console.WriteLine("Is the mod enabled at the computer? " + modStatus);
                            if (modStatus == "On")
                            {
                                __result = 0.03f;
                                //Console.WriteLine("Mod is enabled and we are in a private room. Result written is " + __result);
                            }
                            //else
                            //{
                                //Console.WriteLine("Mod is off at computer");
                            //}
                        //}
                        else
                        {
                            //Console.WriteLine("Room appears to be public");
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Not in a room");
                    }
                }
                else
                {
                    //Console.WriteLine("Not connected to master server yet");
                }
            }
        
        }
    }
}