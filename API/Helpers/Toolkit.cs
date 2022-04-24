using System.Collections.Generic;

namespace Katameros
{
    public static class Tk
    {
        public static IEnumerable<T> A<T>(params T[] args)
        {
            return args;
        }
    }
}
