using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace TimezoneandWeather.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        private DispatcherTimer timer;
        private DateTime dateTimeInfo;

        private string _currentTimeZone;
        public string CurrentTimeZone { get => _currentTimeZone; set { _currentTimeZone = value; OnPropertyChanged(); } }

        private string _currentTime;
        public string CurrentTime { get => _currentTime; set { _currentTime = value; OnPropertyChanged(); } }

        private string _selectedTime;
        public string SelectedTime { get => _selectedTime; set { _selectedTime = value; OnPropertyChanged(); } }

        private ObservableCollection<TimeZoneInfo> _timeZoneList;
        public ObservableCollection<TimeZoneInfo> TimeZoneList { get => _timeZoneList; set { _timeZoneList = value; OnPropertyChanged(); } }

        private TimeZoneInfo _selectedTimeZone;
        public TimeZoneInfo SelectedTimeZone
        {
            get => _selectedTimeZone;
            set
            {
                _selectedTimeZone = value; OnPropertyChanged(); SetTime();
                if (RandomCommand != null) RandomCommand.CanExecute(new Func<bool>(RandomCanExecute));
            }
        }

        private RelayCommand<object> _randomCommand;
        public RelayCommand<object> RandomCommand { get => _randomCommand; private set { _randomCommand = value; OnPropertyChanged(); } }

        public MainViewModel()
        {
            CurrentTimeZone = TimeZoneInfo.Local.ToString();
            CurrentTime = TimeZoneInfo.Local.DisplayName.ToString();
            TimeZoneList = AddTimeZones();
            //SelectedTimeZone = TimeZoneList[0];

            RandomCommand = new RelayCommand<object>(new Action<object>(RandomExeute), new Func<bool>(RandomCanExecute));

            StartTimer();
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, args) => SetTime();
            timer.Start();
        }

        private void RandomExeute(object o)
        {
            var rnd = new Random();
            SelectedTimeZone = TimeZoneList[rnd.Next(TimeZoneList.Count)];
        }
        private bool RandomCanExecute() => SelectedTimeZone != null;

        private ObservableCollection<TimeZoneInfo> AddTimeZones()
        {
            var result = new ObservableCollection<TimeZoneInfo>();
            foreach (var item in TimeZoneInfo.GetSystemTimeZones())
            {
                result.Add(item);
            }

            return result;
        }

        private void SetTime()
        {
            CurrentTime = $"{CurrentTimeZone} - {DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";

            if (SelectedTimeZone != null)
            {
                dateTimeInfo = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTimeOffset.Now.DateTime, SelectedTimeZone.Id);
                SelectedTime = $"{SelectedTimeZone} - {dateTimeInfo.ToShortDateString()} {dateTimeInfo.ToLongTimeString()}";
            }
        }
    }
}
