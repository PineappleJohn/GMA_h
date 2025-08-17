using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mahak.Hacks
{
    public enum Catagory
    {
        Movement,
        Casting,
        Item,
        Troll,
        Misc
    }
    public abstract class BaseHack
    {
        public abstract string Name { get; }
        public abstract Catagory Catagory { get; }
        public abstract void OnEnable();
        public abstract void OnDisable();
        public virtual bool IsEnabled { get; set; } = false;
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnInitialize() { }
    }
}
