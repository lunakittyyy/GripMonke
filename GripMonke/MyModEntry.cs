using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;
using ComputerInterface;
using ComputerInterface.Interfaces;
using Zenject;

namespace Grippy
{
    public class GripEntry : IComputerModEntry
    {
        public string EntryName => "GrippyMonke";
        
        public Type EntryViewType => typeof(GripView);
     
    }
}