using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mahak.Utils
{
    public class TwoPairValue<T1, T2>(T1 first, T2 second)
    {
        public T1 First { get; set; } = first;
        public T2 Second { get; set; } = second;

        public override string ToString()
        {
            return $"({First}, {Second})";
        }
    }
}
