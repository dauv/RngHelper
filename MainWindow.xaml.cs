using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
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

            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(UpdateNumber);
            timer.Interval = 5000; 
            timer.Start();
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

        public bool AutoUpdate { get; set; }

        private Color purple = (Color)ColorConverter.ConvertFromString("#7e00fc");
        private Color red = (Color)ColorConverter.ConvertFromString("#f70000");
        private Color orange = (Color)ColorConverter.ConvertFromString("#ff8000");
        private Color yellow = (Color)ColorConverter.ConvertFromString("#fdfd00");

        public Brush BackgroundColor
        {
            get
            {
                if (Random > 75)
                    return new SolidColorBrush(purple);
                if (Random > 50)
                    return new SolidColorBrush(red);
                if (Random > 25)
                    return new SolidColorBrush(orange);
                else if (Random > 0)
                    return new SolidColorBrush(yellow);
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
