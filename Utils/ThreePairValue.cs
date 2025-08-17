using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mahak.Utils
{
    public class ThreePairValue<T1, T2, T3>
    {
        public T1 first { get; set; }
        public T2 second { get; set; }
        public T3 third { get; set; }

        public ThreePairValue(T1 first, T2 second, T3 third)
        {
            this.first = first;
            this.second = second;
            this.third = third;
        }
    }
}
