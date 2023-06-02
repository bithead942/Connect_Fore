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
    /// Interaction logic for Lexicon.xaml
    /// </summary>
    public partial class Lexicon : Window
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public Lexicon()
        {
            InitializeComponent();
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
            mediaPlayer.Stop();
            this.Close();
        }

        private void rowAhDinnaeKen_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Ah dinnae ken if you intended to make that move.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowAye_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Aye.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowBlether_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Hurry along now you blathering numpty.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowBoggin_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Boggin.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowBoltYaRocket_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Bolt ya rocket.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowBonnie_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Bonnie.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowCeadMileFailte_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Welcome2.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowDearg_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Dearg.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowFeartieCat_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Feartie cat.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowFore_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Fore.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowGeeinItLaldy_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Geein it laldy.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowGorm_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Gorm.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowHasteYeBack_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Haste ye back.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowHaudYerWeesht_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Haud yer wheesht and read this carefully.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowItGiesMeTheBoak_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/It gies me the boak watchin you play.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowLad_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Lad.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowLangMayYerLumReek_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Lang may yer lum reek.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowLass_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Lass.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowManyAMickleMaksAMuckle_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Many a mickle maks a muckle.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowNeepsAndTatties_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Neeps and tatties.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowNumpty_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Hurry along now you blathering numpty.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowSassenach_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Sassenach.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowScunnered_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Im getting scunnered - hurry up.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowSlingYerHook_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Sling yer hook.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowSwick_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Cheating/No swickin allowed.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowTheBawsOnTheSlates_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/the baws on the slates.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowWee_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Wee.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowYeMakABetterDoor_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Yee mak a better door than a windae.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }

        private void rowYerBumsOotTheWandae_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Lexicon/Yer bums oot the windae.m4a"));
            mediaPlayer.Play();
            navigationTimer.Start();
        }
    }
}
