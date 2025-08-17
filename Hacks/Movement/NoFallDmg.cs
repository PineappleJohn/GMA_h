using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mahak.Hacks.Movement
{
    public class NoFallDmg : BaseHack
    {
        public override string Name => "Disable Fall Damage";
        public override Catagory Catagory => Catagory.Movement;
        public override void OnEnable()
        {
            if (!Plugin.TryGetPlayer())
            {
                IsEnabled = false;
                Plugin.Logger.LogError("PlayerMovement not found, disabling No Fall Damage hack.");
            }
            Plugin.Logger.LogInfo("Fall Damage hack enabled.");
        }
        public override void OnDisable() { }

        public override void OnUpdate()
        {
            PlayerMovement __instance = Plugin.PlayerMovement;

            var field1 = typeof(PlayerMovement).GetField("FallTimer", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field1 != null)
            {
                field1.SetValue(__instance, 0f);
            }
        }

    }
}
