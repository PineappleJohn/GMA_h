using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace mahak.Hacks.Casting
{
    public class ClickSpell : BaseHack
    {
        public override string Name => "Click Hack";
        public override Catagory Catagory => Catagory.Casting;

        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {
            if (!Plugin.TryGetPlayer())
            {
                IsEnabled = false;
                Plugin.Logger.LogError("PlayerMovement not found, disabling Click Hack.");
            }
            Plugin.Logger.LogInfo("Click Hack enabled.");
        }

        public override void OnUpdate()
        {
            PlayerInventory inv = Plugin.PlayerMovement?.GetComponent<PlayerInventory>();
            if (Input.GetMouseButtonDown(0))
            {
                inv.cFireball();
                inv.cFrostbolt();
                inv.cCastworm();
                inv.cCastWard();
                /*VoiceControlListener vcl = Plugin.TryGetVoiceControlListener();
                foreach (ISpellCommand spellPage3 in vcl.SpellPages)
                {
                    spellPage3?.TryCastSpell();
                }*/
            }
            if (Input.GetMouseButtonDown(1))
            {
                inv.cCasthole();
            }
        }
    }
}
