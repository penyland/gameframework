// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace GameFramework.Utils
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
