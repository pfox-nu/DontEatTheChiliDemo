using DontEatTheChiliLib;
using DontEatTheChiliLib.Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DontEatTheChili
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameController game;
        private Color playerOneColor = Colors.Black;
        private Color playerTwoColor = Colors.Green;

        public MainWindow()
        {
            InitializeComponent();

            Setup();
        }

        private void Setup()
        {
            game = new GameController();
            game.MessageEvent += Message;

            // Set the datacontext of this window
            // This lets us use DataBinding between UI elements and game properties
            this.DataContext = game;

            AddChocolatesToButton(btnOneCandy, 1);
            AddChocolatesToButton(btnTwoCandies, 2);
            AddChocolatesToButton(btnThreeCandies, 3);
        }

        private void Message(string message)
        {
            Color messageColor = GetPlayerColor();

            if (game.GameDone) messageColor = Colors.Red;

            messageLogPanel.Children.Insert(0, new Label() { Content = message, Foreground = new SolidColorBrush(messageColor) });
            lblMessages.Content = message;
        }

        private Color GetPlayerColor()
        {
            return game.CurrentPlayer == Players.PlayerOne ? playerOneColor : playerTwoColor;
        }

        private void BtnSinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            game.Play(GameModes.SinglePlayer);
            UpdateGame();
        }

        private void BtnSinglePlayerHard_Click(object sender, RoutedEventArgs e)
        {
            game.Play(GameModes.SinglePlayerHard);
            UpdateGame();
        }

        private void BtnTwoPlayer_Click(object sender, RoutedEventArgs e)
        {
            game.Play(GameModes.TwoPlayer);
            UpdateGame();
        }

        private void TurnButton_Click(object sender, RoutedEventArgs e)
        {
            // We can use the Tag to know which button was clicked
            string btnTag = ((Button)sender).Tag.ToString();
            Moves move = (Moves)Enum.Parse(typeof(Moves), btnTag);

            game.TakeTurn(move);

            UpdateGame();
        }

        private void UpdateGame()
        {
            pieceGrid.Children.Clear();

            // Add remaining candies
            for (int i = 0; i < game.CandyCount; i++)
            {
                pieceGrid.Children.Add(
                    GetChocolateImage()
                );
            }

            // Add the Chili
            pieceGrid.Children.Add(
               new Image()
               {
                   Source = new BitmapImage(new Uri(
                    AppDomain.CurrentDomain.BaseDirectory + "chili.png", UriKind.Absolute
                ))
               }
            );
        }

        private void AddChocolatesToButton(Button btn, int count)
        {
            StackPanel pnl = new StackPanel();
            pnl.Background = new SolidColorBrush(Colors.Tan);
            pnl.Orientation = Orientation.Horizontal;
            pnl.Height = 50;
            for (int i = 0; i < count; i++)
            {
                pnl.Children.Add(
                    GetChocolateImage()
                );
            }
            btn.Content = pnl;
        }

        private Image GetChocolateImage()
        {
            return new Image()
            {
                Source = new BitmapImage(new Uri(
                    AppDomain.CurrentDomain.BaseDirectory + "chocolate.png", UriKind.Absolute
                ))
            };
        }
    }
}