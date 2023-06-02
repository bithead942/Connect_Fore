using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Threading;
using System.Windows.Media.Animation;

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CoinFlip : Window
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer buttonTimer = new System.Windows.Threading.DispatcherTimer();
        Speak _speak = new Speak();

        public static int iFirstPlayer = 0;
        Random random = new Random();
        bool firstTime = true;
        MainWindow.AIOption _aiOption = MainWindow.AIOption.TwoPlayer;

        public CoinFlip(MainWindow.AIOption aiOption)
        {
            InitializeComponent();
            _aiOption = aiOption;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            int r = random.Next(0, 2);  //value is either 0 or 1
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();


            if (r == 0)
                mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
            else
                mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);

            Coin.Fill = mySolidColorBrush;
            firstTime = true;
        }

        private void buttonTimer_Tick(object sender, EventArgs e)
        {
            BitmapImage image = new BitmapImage(new Uri("C:/Users/gregs/source/repos/ConnectFore/images/Buttons v2/Lets Play2.png", UriKind.Absolute));

            buttonTimer.Stop();
            if (iFirstPlayer == 2)
            {
                lblTitle.Text = "Red Plays First";
                _speak.speakPlayFirst(_aiOption, Connect.Player.Player2);
            }
            else
            {
                lblTitle.Text = "Blue Plays First";
                _speak.speakPlayFirst(_aiOption, Connect.Player.Player1);
            }
            btnAction.IsEnabled = true;
            firstTime = false;

            ControlTemplate ct = btnAction.Template;
            Image btnImage = (Image)ct.FindName("myImage", btnAction);
            btnImage.Source = image;

            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 0, 30);
            navigationTimer.Start();

        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            this.Close();
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            if (firstTime)
            {
                int r = random.Next(0, 2);  //value is either 0 or 1

                if (r == 0)
                {
                    ImageFlip.RepeatBehavior = new RepeatBehavior(new TimeSpan(0, 0, 0, 2, 800));  //Red
                    iFirstPlayer = 2;

                }
                else
                {
                    ImageFlip.RepeatBehavior = new RepeatBehavior(new TimeSpan(0, 0, 0, 3, 0));  //Blue
                    iFirstPlayer = 1;
                }

                ImageFlip.Begin();

                btnAction.IsEnabled = false;
                buttonTimer.Tick += buttonTimer_Tick;
                buttonTimer.Interval = new TimeSpan(0, 0, 0, 3, 0);
                buttonTimer.Start();
            }
            else
            {
                navigationTimer.Stop();
                this.Close();
            }
        }
    }
}
