using System;
using System.Collections.Generic;
using System.Linq;

namespace AsyncTest.Database.Database.Model
{
    public static class BalloonColors
    {
        public static IEnumerable<BalloonColor> All => Enum.GetValues(typeof (BalloonColor)).Cast<BalloonColor>();
    }
}