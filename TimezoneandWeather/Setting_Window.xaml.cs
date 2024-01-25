using System;
using System.Collections.Generic;
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
        public string SettingGuide { get { return settingGuide;}}
        private string settingGuide;
        public Setting_Window()
        {
            InitializeComponent();
            this.DataContext = this;
            settingGuide = "-Pick a country in the list to see time of that country and weather on the right panel\n-Pick between light and Dark theme for app in menu";
            
        }
    }
}
