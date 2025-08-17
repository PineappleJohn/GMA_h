using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace mahak.Patches
{
    
    [HarmonyPatch(typeof(PlayerMovement), "Update")]
    public class JumpPatch
    {
        [HarmonyPostfix]
        static void Postfix(PlayerMovement __instance)
        {
            if (Plugin.hacks[1].IsEnabled)
            {
                // Use reflection to access and modify the private field  
                var field = typeof(PlayerMovement).GetField("timeSinceGrounded", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(__instance, 0f);
                }
            }
        }
    }
}
