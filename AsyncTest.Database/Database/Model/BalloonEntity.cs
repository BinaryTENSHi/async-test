using System;

namespace AsyncTest.Database.Database.Model
{
    public class BalloonEntity
    {
        public Guid Id { get; set; }

        public BalloonColor Color { get; set; }
        public double Diameter { get; set; }
    }
}