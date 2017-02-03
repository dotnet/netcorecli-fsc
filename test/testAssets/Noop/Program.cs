using System;

namespace ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
#if FAIL
            return 1;
#else
            return 0;
#endif
        }
    }
}
