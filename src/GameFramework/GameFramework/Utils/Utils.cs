// Copyright (c) Peter Nylander.  All rights reserved.

using System;

namespace GameFramework
{
    public static class Utils
    {
        private static Random random = new Random();

        public static float RandomBetween(float min, float max)
        {
            return min + ((float)random.NextDouble() * (max - min));
        }
    }
}
