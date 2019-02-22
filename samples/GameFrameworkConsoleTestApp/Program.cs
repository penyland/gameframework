// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Diagnostics;

namespace GameFrameworkConsoleTestApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Creating Bootstrap");

            new Bootstrap().Run();

            Console.WriteLine("Bootstrap.Run completed");
            Console.ReadKey();
        }
    }
}
