using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RngHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.DragMove();

            AutoUpdate = Properties.Settings.Default.AutoUpdate;

            // Init colors
            try
            {
                color1 = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.Color1_25);
                color2 = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.Color26_50);
                color3 = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.Color51_75);
                color4 = (Color)ColorConverter.ConvertFromString(Properties.Settings.Default.Color76_100);
            }
            catch (Exception)
            {
                // Default colours
                color1 = (Color)ColorConverter.ConvertFromString("#fdfd00");
                color2 = (Color)ColorConverter.ConvertFromString("#ff8000");
                color3 = (Color)ColorConverter.ConvertFromString("#ff0000");
                color4 = (Color)ColorConverter.ConvertFromString("#7e00fc");
            } 
            
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(UpdateNumber);
            timer.Interval = 5000; 
            timer.Start();


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private Timer timer;

        private Random rng = new Random();

        private void UpdateNumber(object source, ElapsedEventArgs e)
        {
            if (AutoUpdate)
                setRandom();
        }

        private int? random;
        public int? Random
        {
            get { return random; }
            set
            {
                random = value;
                OnPropertyChanged("Random");
                OnPropertyChanged("BackgroundColor");                
            }
        }

        private bool autoUpdate;
        public bool AutoUpdate { 
            get
            {
                return autoUpdate;
            }
            set
            {
                autoUpdate = value;
                Properties.Settings.Default.AutoUpdate = value;
                Properties.Settings.Default.Save();
            }
        }

        private Color color1;
        private Color color2;
        private Color color3;
        private Color color4;

        public Brush BackgroundColor
        {
            get
            {
                if (Random > 75)
                    return new SolidColorBrush(color4);
                if (Random > 50)
                    return new SolidColorBrush(color3);
                if (Random > 25)
                    return new SolidColorBrush(color2);
                else if (Random > 0)
                    return new SolidColorBrush(color1);
                return new SolidColorBrush();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void randomButton_Click(object sender, RoutedEventArgs e)
        {
            setRandom();
        }

        private void setRandom()
        {
            Random = rng.Next(1, 101);
        }
    }
}
