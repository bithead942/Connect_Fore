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

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for Question.xaml
    /// </summary>
    public partial class Question : Window
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        MainWindow.AIOption aiOption;

        public Question(MainWindow.AIOption _aiOption)
        {
            InitializeComponent();
            aiOption = _aiOption;

            switch (aiOption) {
                case MainWindow.AIOption.Defensive:
                    btnInfo.Visibility = Visibility.Hidden;
                    Grid.SetColumnSpan(btnLexicon, 2);
                    break;
                case MainWindow.AIOption.Hybrid:
                    btnInfo.Visibility = Visibility.Hidden;
                    Grid.SetColumnSpan(btnLexicon, 2);
                    break;
                case MainWindow.AIOption.MINMAX:
                    Grid.SetColumnSpan(btnLexicon, 1);
                    btnInfo.Visibility = Visibility.Visible;
                    break;
                case MainWindow.AIOption.Monte_Carlo:
                    btnInfo.Visibility = Visibility.Visible;
                    Grid.SetColumnSpan(btnLexicon, 1);
                    break;
                case MainWindow.AIOption.Q_Learning:
                    btnInfo.Visibility = Visibility.Visible;
                    Grid.SetColumnSpan(btnLexicon, 1);
                    break;
                case MainWindow.AIOption.Random:
                    btnInfo.Visibility = Visibility.Hidden;
                    Grid.SetColumnSpan(btnLexicon, 2);
                    break;
                case MainWindow.AIOption.TwoPlayer:
                    btnInfo.Visibility = Visibility.Hidden;
                    Grid.SetColumnSpan(btnLexicon, 2);
                    break;
                default:
                    Grid.SetColumnSpan(btnLexicon, 2);
                    btnInfo.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 3, 0);
            navigationTimer.Start();
        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            this.Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();

            Info frmInfo = new Info(aiOption);
            frmInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmInfo.ShowDialog();  //Waits for a response

            this.Close();
        }

        private void btnLexicon_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();

            Lexicon frmLexicon = new Lexicon();
            frmLexicon.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmLexicon.ShowDialog();  //Waits for a response

            this.Close();
        }
    }
}
