using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Connect;

namespace ConnectFore
{
    class Speak
    {
        public MediaPlayer mediaPlayer = new MediaPlayer();
        public bool isVoicePlaying = false;

        Random r = new Random();

        public Speak()  //Constructor
        {

        }

        public void speakQuit(int numPlayers)
        {
            int iPhrase = 0;

            if (numPlayers == 1)
                iPhrase = r.Next(0, 8);  //0-7
            else
                iPhrase = r.Next(0, 6);  //0-5

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/19th hole.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Cowardly Choice.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Quit confirm.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Had enough.m4a"));
                    break;

                case 4:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Shame of defeat.m4a"));
                    break;

                case 5:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Haste ye back.m4a"));
                    break;

                case 6:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Quitting so soon.m4a"));
                    break;

                case 7:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Quit/Sucking sound.m4a"));
                    break;


                default:
                    //error handling
                    break;
            }

            mediaPlayer.Play();

        }

        public void speakHurry()
        {
            int iPhrase = r.Next(0, 7);  //0 - 6

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Done yet.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Hurry up.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Nanna.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Wake when done.m4a"));
                    break;

                case 4:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Have nae got all day.m4a"));
                    break;

                case 5:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Hurry along now you blathering numpty.m4a"));
                    break;

                case 6:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Hurry/Im getting scunnered - hurry up.m4a"));
                    break;
            }

            mediaPlayer.Play();

        }

        public void speakStart2Player()
        {
            int iPhrase = r.Next(0, 6);  //0-5

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 2 player/2 Player - Challenge.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 2 player/2 Players.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 2 player/Get on with it.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 2 player/have your fun.m4a"));
                    break;

                case 4:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 2 player/Me next time.m4a"));
                    break;

                case 5:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 2 player/Geein it laldy.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speakStart1Player(MainWindow.AIOption _aiOption, MainWindow.DifficultyOption _difficulty)
        {
            int iPhrase = r.Next(0, 11);  //0-10

            switch (iPhrase)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:  //This wll be selected 80% of the time
                    switch (_aiOption)
                    {
                        case MainWindow.AIOption.Random:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Feartie cat.m4a"));
                            break;
                        case MainWindow.AIOption.Defensive:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Feartie cat.m4a"));
                            break;
                        case MainWindow.AIOption.MINMAX:
                            if (_difficulty == MainWindow.DifficultyOption.Low || _difficulty == MainWindow.DifficultyOption.Medium)
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Interesting choice.m4a"));
                            else
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Bold choice.m4a"));
                            break;
                        case MainWindow.AIOption.Hybrid:
                            if (_difficulty == MainWindow.DifficultyOption.Low)
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Interesting choice.m4a"));
                            else if (_difficulty == MainWindow.DifficultyOption.Medium)
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Bold choice.m4a"));
                            else
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Regret.m4a"));
                            break;
                        case MainWindow.AIOption.Monte_Carlo:
                            if (_difficulty == MainWindow.DifficultyOption.Low)
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Bold choice.m4a"));
                            else
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Regret.m4a"));
                            break;
                        case MainWindow.AIOption.Q_Learning:
                            if (_difficulty == MainWindow.DifficultyOption.Low)
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Bold choice.m4a"));
                            else
                                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Regret.m4a"));
                            break;
                    }
                    break;
                case 8:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Lets Play.m4a"));
                    break;
                case 9:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Best robot win.m4a"));
                    break;
                case 10:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Start - 1 player/Geein it laldy.m4a"));
                    break;

            }

            mediaPlayer.Play();

        }

        public void speakPlayFirst(MainWindow.AIOption _aIOption, Player _player)
        {
            if (_aIOption == MainWindow.AIOption.TwoPlayer) //coin flip
            {
                if (_player == Player.Player1)
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Play First/Blue first.m4a"));
                else
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Play First/Red first.m4a"));
            }
            else if (_aIOption == MainWindow.AIOption.Random || _aIOption == MainWindow.AIOption.Defensive)
            {
                if (_player == Player.Player1)
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Play First/I play first.m4a"));
                else
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Play First/Red first.m4a"));
            }
            else
            {
                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Play First/I play first.m4a"));
            }

            mediaPlayer.Play();

        }

        public void speakComWin()
        {
            int iPhrase = r.Next(0, 9);  //0-8

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/Better luck next time.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/Choking sound.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/Sucking sound.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/The best you got.m4a"));
                    break;

                case 4:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/Worthy opponent.m4a"));
                    break;

                case 5:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/You stink.m4a"));
                    break;

                case 6:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/the baws on the slates.m4a"));
                    break;

                case 7:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/New file bad golfers.m4a"));
                    break;

                case 8:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/Take some lessons.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speakComLoss()
        {
            int iPhrase = r.Next(0, 7);  //0-6

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Looses/Better lucky than good.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Looses/Cant do it again.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Looses/Hole in one - joke.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Looses/Lawnmower and Bagpipe.m4a"));
                    break;

                case 4:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Looses/Rematch.m4a"));
                    break;

                case 5:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Looses/Stratch golfer.m4a"));
                    break;

                case 6:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Looses/You cheated.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speakComTurn()
        {
            int iPhrase = r.Next(0, 14);  //0-13

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Bolt ya rocket.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Clear off.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Expert.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Let me show you.m4a"));
                    break;

                case 4:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Mind your ankles.m4a"));
                    break;

                case 5:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/My turn.m4a"));
                    break;

                case 6:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Turn/Sling yer hook.m4a"));
                    break;

                case 7:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Golf is a game.m4a"));
                    break;

                case 8:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Good shot.m4a"));
                    break;

                case 9:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Putt.m4a"));
                    break;

                case 10:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Shot.m4a"));
                    break;

                case 11:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Intended move.m4a"));
                    break;

                case 12:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Not Choice I would have made.m4a"));
                    break;

                case 13:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Yee mak a better door than a windae.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speakHumanTurn(Player _player, MainWindow.AIOption _aiOption)
        {

            if (_player == Player.Player1)
            {
                int iPhrase1 = r.Next(0, 10);  //0-9

                switch (iPhrase1)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Blues turn.m4a"));
                        break;

                    case 4:
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Golf is a game.m4a"));
                        break;

                    case 5:
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Good shot.m4a"));
                        break;

                    case 6:
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Putt.m4a"));
                        break;

                    case 7:
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Shot.m4a"));
                        break;

                    case 8:
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Intended move.m4a"));
                        break;

                    case 9:
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Not Choice I would have made.m4a"));
                        break;
                }
            }
            else
            {
                if (_aiOption == MainWindow.AIOption.TwoPlayer)
                {
                    int iPhrase2 = r.Next(0, 10);  //0-9

                    switch (iPhrase2)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Reds turn.m4a"));
                            break;

                        case 4:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Golf is a game.m4a"));
                            break;

                        case 5:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Good shot.m4a"));
                            break;

                        case 6:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Putt.m4a"));
                            break;

                        case 7:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Shot.m4a"));
                            break;

                        case 8:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Intended move.m4a"));
                            break;

                        case 9:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Not Choice I would have made.m4a"));
                            break;
                    }
                }
                else
                {
                    int iPhrase3 = r.Next(0, 6);  //0-5

                    switch (iPhrase3)
                    {
                        case 0:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Your turn.m4a"));
                            break;

                        case 1:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Human turn.m4a"));
                            break;

                        case 2:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Let you have a turn.m4a"));
                            break;

                        case 3:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Reds turn.m4a"));
                            break;

                        case 4:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Have your turn.m4a"));
                            break;

                        case 5:
                            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Human Turn/Your shot.m4a"));
                            break;
                    }
                }
            }

            mediaPlayer.Play();
        }

        public void speakComment()
        {
            int iPhrase = r.Next(0, 9);  //0-8

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Golf is a game.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Good shot.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Putt.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Great Shot.m4a"));
                    break;

                case 4:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Intended move.m4a"));
                    break;

                case 5:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Not Choice I would have made.m4a"));
                    break;

                case 6:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Many a mickle maks a muckle.m4a"));
                    break;

                case 7:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/Ah dinnae ken if you intended to make that move.m4a"));
                    break;

                case 8:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Comment/It gies me the boak watchin you play.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speakAnotherBall(Player _player)
        {
            int iPhrase = r.Next(0, 4);  //0-3

            switch (iPhrase)
            {
                case 0:
                    if (_player == Player.Player1)
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Another Ball/Blue ball.m4a"));
                    else
                        mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Another Ball/Red ball.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Another Ball/How many balls.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Another Ball/Lost ball.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Another Ball/Water Hazard.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speakCheating()
        {
            int iPhrase = r.Next(0, 4);  //0-3

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Cheating/Cheating on golf course.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Cheating/No cheating allowed.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Cheating/No swickin allowed.m4a"));
                    break;

                case 3:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Computer Wins/the baws on the slates.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speakTie()
        {
            int iPhrase = r.Next(0, 3);  //0-2

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Tie/Finally over.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Tie/Game never end.m4a"));
                    break;

                case 2:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/tie/Tie.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

        public void speak2PlayerGameOver(Result r, Player winner)
        {
            if (r == Result.Win)
            {
                if (winner == Player.Player1)
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game Over/Game over Blue wins.m4a"));
                else
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Game over/Game over Red wins.m4a"));
            }
            if (r == Result.Tie)
            {
                speakTie();
            }

            if (r == Result.Intermediate)
            {
                speakQuit(2);
            }

            if (r == Result.Invalid)
            {
                speakCheating();
            }

            mediaPlayer.Play();
        }

        public void speakRules()
        {
            int iPhrase = r.Next(0, 2);  //0-1

            switch (iPhrase)
            {
                case 0:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Rules.m4a"));
                    break;

                case 1:
                    mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Haud yer wheesht and read this carefully.m4a"));
                    break;
            }

            mediaPlayer.Play();
        }

    }
}
