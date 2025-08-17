using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mahak.Utils
{
    public class FourPairValue<T1, T2, T3, T4>
    {
        public T1 first { get; set; }
        public T2 second { get; set; }
        public T3 third { get; set; }
        public T4 fourth { get; set; }

        public FourPairValue(T1 first, T2 second, T3 third, T4 fourth)
        {
            this.first = first;
            this.second = second;
            this.third = third;
            this.fourth = fourth;
        }
    }
}
