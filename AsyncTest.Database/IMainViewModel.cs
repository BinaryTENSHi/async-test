using System.Collections.Generic;
using AsyncTest.Database.Database.Dto;
using AsyncTest.Shared.UI;

namespace AsyncTest.Database
{
    public interface IMainViewModel
    {
        BalloonDto SelectedBalloon { get; set; }
        IAsyncCommand UpdateAsyncCommand { get; set; }
        IList<BalloonDto> Balloons { get; set; }
        IAsyncCommand AddAsyncCommand { get; }
    }
}