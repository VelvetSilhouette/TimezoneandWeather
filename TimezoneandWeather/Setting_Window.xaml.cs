using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimezoneandWeather
{
    /// <summary>
    /// Interaction logic for Setting_Window.xaml
    /// </summary>
    public partial class Setting_Window : Window
    {
        private MainWindow mainWindow;
        public string SettingGuide { get { return settingGuide;}}
        private string settingGuide;
        public ObservableCollection<string> ThemeList { get { return themeList; } }
        private ObservableCollection<string> themeList;
        public ObservableCollection<TimeZoneInfo> TimezoneList {get { return timezoneList; } }
        private ObservableCollection<TimeZoneInfo> timezoneList;

        private string newtimezone ="Empty";
        private string themeinfo ="Default";
        public Setting_Window(MainWindow mainwindow)
        {
            InitializeComponent();
            this.mainWindow = mainwindow;
            settingGuide = "-Pick a country in the list to see time of that country and weather on the right panel\n-Pick between light and Dark theme for app in menu";
            themeList = new ObservableCollection<string>() {"Dark","Normal"}; //List of Theme for App
            timezoneList = new ObservableCollection<TimeZoneInfo>(); //Timezone List 
//hold currently selected option for Setting
            gettimezone();
            this.DataContext = this;
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            savesetting(newtimezone,themeinfo);
        }
        private void savesetting(string _newtimezoneid,string _themeinfo)
        {
            if(TimezoneBox.SelectedItem !=null) 
            {
                var timezone = (TimeZoneInfo)TimezoneBox.SelectedItem; //save the selected Alternate timezone
                _newtimezoneid = timezone.Id;               
            }

            if(ThemeBox.SelectedItem !=null) 
            {
                _themeinfo = ThemeBox.SelectedItem.ToString(); // save the theme setting
            }
            mainWindow.NewSetting(_newtimezoneid,_themeinfo);
            this.Close();
        }
        private void gettimezone()
        {
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timezoneList.Add(z);//Add all timezone display name to the timezone selection list
            }
        }


    }
}
