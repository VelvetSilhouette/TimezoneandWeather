using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimezoneandWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeZoneInfo alttimezoneid;
        public MainWindow()
        {
            InitializeComponent();
            starclock();
            //method to check user local timer usesing DispatcherTimer
            showtimezone();
            runalttime();
            //Check and show user time zone
        }
        private void starclock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);//update clock every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            UserTimeTxtBlk.Text = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";//update timer
            updatealttime(alttimezoneid);
        }
        private void updatealttime(TimeZoneInfo alttimezoneid)
        {
            if (alttimezoneid != null)
            {
                var eventTimeLocal = DateTimeOffset.Now;
                var eventTimeAlt = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(eventTimeLocal,alttimezoneid.Id);
                AltTimeTxtBlck.Text = eventTimeAlt.ToString("MM/dd/yyyy HH:mm:ss tt");//use method to update alternative clock
            }
        }

        private void showtimezone()
        {
            TimeZoneInfo usertimezone = TimeZoneInfo.Local;
            UserLocalTxtBlk.Text=usertimezone.DisplayName.ToString();
        }
        private void Setting_Btn_Click(object sender, RoutedEventArgs e)
        {
            Setting_Window Setting1 = new Setting_Window(this);
            Setting1.ShowDialog();
        }
        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void NewSetting(string newtimezone,string theme)
        {
            var timezoneinfo1 = TimeZoneInfo.FindSystemTimeZoneById(newtimezone);
            if (timezoneinfo1 != null) 
            {
                AltTimeZoneTxtBlck.Text = timezoneinfo1.DisplayName;
                updatealttime(timezoneinfo1);
                alttimezoneid = timezoneinfo1;//update App with new Setting
            }
        }

        private void runalttime()
        {
        }

    }
}