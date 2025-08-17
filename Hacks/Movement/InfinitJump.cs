using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace mahak.Hacks.Movement
{
    public class InfinitJump : BaseHack
    {
        public override string Name => "Infinite Jump";
        public override Catagory Catagory => Catagory.Movement;
        public override void OnEnable()
        {
            if (!Plugin.TryGetPlayer())
            {
                IsEnabled = false;
                Plugin.Logger.LogError("PlayerMovement not found, disabling Infinite Jump hack.");
            }
            Plugin.Logger.LogInfo("Infinite Jump hack enabled.");
        }
        public override void OnDisable() { }

        public override void OnUpdate()
        {
            //Plugin.PlayerMovement.canJump = true;

            PlayerMovement plr = Plugin.PlayerMovement;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                plr.velocity.y += 8f;
            }
        }
    }
}
