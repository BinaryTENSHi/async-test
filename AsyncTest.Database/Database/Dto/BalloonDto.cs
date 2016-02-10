using System;
using AsyncTest.Database.Database.Model;

namespace AsyncTest.Database.Database.Dto
{
    public class BalloonDto : IDto
    {
        public BalloonColor Color { get; set; }
        public double Diameter { get; set; }
        public Guid Id { get; set; }
    }
}