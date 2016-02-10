using AsyncTest.Database.Database.Dto;
using AsyncTest.Database.Database.Model;

namespace AsyncTest.Database.Database.Mapper
{
    public class BalloonMapper : IMapper<BalloonEntity, BalloonDto>
    {
        public void MapToDto(BalloonEntity source, BalloonDto target)
        {
            target.Id = source.Id;
            target.Diameter = source.Diameter;
            target.Color = source.Color;
        }

        public void MapToEntity(BalloonDto source, BalloonEntity target)
        {
            target.Id = source.Id;
            target.Diameter = source.Diameter;
            target.Color = source.Color;
        }
    }
}