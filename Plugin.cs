using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using mahak.GUI.Windows;
using mahak.Hacks;
using mahak.Hacks.Casting;
using mahak.Hacks.Movement;
using System;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;

namespace mahak
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        public static PlayerMovement PlayerMovement;

        public static VoiceControlListener Vcl;

        private Harmony harmony = new(MyPluginInfo.PLUGIN_GUID);

        public static bool outdated = false;

        public static readonly BaseHack[] hacks =
        {
            new InfinitJump(),
            new NoFallDmg(),
            new ClickSpell()
        };

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo($"Generic Mage Arena hack loaded");

            gameObject.AddComponent<WindowManager>();

            try
            {
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to patch methods || {ex.Message}");
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Label($"GMA_h - Version: {MyPluginInfo.PLUGIN_VERSION}");
#if !DEBUG
            if (outdated)
                GUILayout.Label($"<color=red>GMA_h is outdated! Use with caution!"/*, new GUIStyle().normal.*/);
#endif
            GUILayout.EndVertical();
        }

        private void Update()
        {
            foreach (BaseHack hack in hacks)
            {
                if (hack.IsEnabled)
                {
                    hack.OnUpdate();
                }
            }
        }

        /// <summary>
        /// Updates the player movement reference if it is not already set.
        /// </summary>
        /// <returns>The player</returns>
        public static PlayerMovement? TryGetPlayer()
        {
            foreach (PlayerMovement plrmov in FindObjectsByType<PlayerMovement>(UnityEngine.FindObjectsSortMode.None))
            {
                if (plrmov.IsOwner)
                {
                    PlayerMovement = plrmov;
                }
            }

            return PlayerMovement;
        }

        public static VoiceControlListener? TryGetVoiceControlListener()
        {
            foreach (VoiceControlListener vcl in FindObjectsByType<VoiceControlListener>(UnityEngine.FindObjectsSortMode.None))
            {
                if (vcl.IsOwner)
                {
                    Vcl = vcl;
                }
            }
            return Vcl;
        }
    }
}
