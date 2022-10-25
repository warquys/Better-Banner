using Exiled.API.Features;
using HarmonyLib;
using System;

namespace Better_Server_Banner
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "BetterBanner";
        public override string Name => "Better Banner";
        public override string Author => "VT";
        public override Version Version { get; } = new Version(1, 0, 0);

        public EventHandler EventHandler { get; private set; }

        public Harmony Harmony { get; private set; }

        public override void OnEnabled()
        {
            base.OnEnabled();
            if (EventHandler == null)
                EventHandler = new EventHandler(Config);
            else
                EventHandler.AttachEvent();
            Patch();
        }

        public override void OnDisabled()
        {
            EventHandler.DetachEvent();
            Harmony.UnpatchAll();
            base.OnDisabled();
        }

        public void Patch()
        {
            if (Harmony == null)
                Harmony = new Harmony(Name);
            Harmony.PatchAll();
        }

        public void Unpatch()
        { 
            Harmony.UnpatchAll();
        }
    }
}