using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncTest.Database.Database.Dto;
using AsyncTest.Database.Database.Model;
using AsyncTest.Database.Database.Repository;
using AsyncTest.Shared.UI;
using Caliburn.Micro;

namespace AsyncTest.Database
{
    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly IBalloonRepository _balloonRepository;
        private IList<BalloonDto> _balloons;
        private BalloonDto _selectedBalloon;

        public MainViewModel(IBalloonRepository balloonRepository)
        {
            _balloonRepository = balloonRepository;
            AddAsyncCommand = new AsyncCommand { CanExecuteHandler = obj => true, ExecuteHandler = AddBalloonAsync };
            UpdateAsyncCommand = new AsyncCommand
            {
                CanExecuteHandler = obj => SelectedBalloon != null,
                ExecuteHandler = UpdateBalloonAsync
            };
        }

        public BalloonDto SelectedBalloon
        {
            get { return _selectedBalloon; }
            set
            {
                _selectedBalloon = value;
                NotifyOfPropertyChange(nameof(SelectedBalloon));
            }
        }

        public IAsyncCommand AddAsyncCommand { get; }
        public IAsyncCommand UpdateAsyncCommand { get; set; }

        public IList<BalloonDto> Balloons
        {
            get { return _balloons; }
            set
            {
                _balloons = value;
                NotifyOfPropertyChange(nameof(Balloons));
                Execute.OnUIThreadAsync(CommandManager.InvalidateRequerySuggested);
            }
        }

        private async Task UpdateBalloonAsync(object parameter)
        {
            await _balloonRepository.UpdateAsync(SelectedBalloon).ConfigureAwait(false);
            await _balloonRepository.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task AddBalloonAsync(object obj)
        {
            BalloonDto dto = RandomBalloon();
            _balloonRepository.Insert(dto);
            await _balloonRepository.SaveChangesAsync().ConfigureAwait(false);
            await UpdateBalloonsAsync().ConfigureAwait(false);
        }

        private async Task UpdateBalloonsAsync()
        {
            Balloons = (await _balloonRepository.AllAsync().ConfigureAwait(false)).ToList();
        }

        protected override void OnInitialize()
        {
            DisplayName = "Database Test";
        }

        protected override async void OnActivate()
        {
            await UpdateBalloonsAsync().ConfigureAwait(false);
        }

        private static BalloonDto RandomBalloon()
        {
            Random r = new Random();
            return new BalloonDto
            {
                Id = Guid.NewGuid(),
                Color = (BalloonColor)r.Next(0, 3),
                Diameter = r.NextDouble() * 100
            };
        }
    }
}