using DontEatTheChiliLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontEatTheChiliLib.Controllers
{
    public class GameController : INotifyPropertyChanged
    {
        #region Constants
        private const int MAX_CANDIES = 13;
        private const string START_MESSAGE = "Would you like to play a game? Choose a game mode to to begin!";
        private const string TURN_MESSAGE = "It's your turn player {0}! Make good choices!";
        private const string END_TURN_MESSAGE = "Player {0} took {1} chocolates";
        private const string END_GAME_MESSAGE = "Game Finished! Player {0} lost and ate the Chili! Do you want to play again?";
        private const string TOO_FEW_CANDIES_MESSAGE = "You can't take {0} candies! There are only {1} left.";
        private const string PLAY_MESSAGE = "You chose {0}! Player One, you're up!";
        private const string AI_PLAY_MESSAGE = "You chose to play the COMPUTER?!? Thanks for letting them go first!";
        private const string SMART_AI_PLAY_MESSAGE = "You chose to play the SMART COMPUTER?!? Thanks for letting them go first!";
        private const string AI_TURN_MESSAGE = "The COMPUTER chose to \"eat\" {0}!";
        private const string AI_VICTORY_MESSAGE = "You let the AI beat you?!? Better luck next time.";
        private const string AI_LOSS_MESSAGE = "Good Job! Way to show our robot overloads a thing or two!";
        #endregion

        #region Fields/Properties
        public Players CurrentPlayer { get; set; }
        public Players OtherPlayer
        {
            get
            {
                // Example of a ternary that gets the non-active player
                return CurrentPlayer == Players.PlayerOne ? Players.PlayerTwo : Players.PlayerOne;
            }
        }
        public GameModes GameMode { get; set; }

        private IGameAI ai;

        private bool gamePlaying = false;
        public bool GamePlaying
        {
            get
            {
                return gamePlaying;
            }
            set
            {
                gamePlaying = value;

                // Alert anyone who's bound to use that this property changed
                ValueChanged("GamePlaying");
                // We'll also alert the Game done changed since it's derived
                ValueChanged("GameDone");
            }
        }

        public bool GameDone
        {
            get { return !gamePlaying; }
        }

        private int candyCount;
        public int CandyCount
        {
            get
            {
                return candyCount;
            }
            set
            {
                candyCount = value;

                // Alert anyone who's bound to use that this property changed
                ValueChanged("CandyCount");
            }
        }

        // Anyone who cares about Game messages needs to subscribe to this event
        public event MessageDelegate MessageEvent;
        #endregion

        public GameController()
        {
            SetupGame();
        }

        private void SetupGame(GameModes gameMode = GameModes.SinglePlayer)
        {
            GamePlaying = false;
            CurrentPlayer = Players.PlayerOne;
            Message(START_MESSAGE);
        }

        public void Play(GameModes mode)
        {
            GamePlaying = true;
            GameMode = mode;
            CandyCount = MAX_CANDIES;

            if (GameMode == GameModes.SinglePlayer)
            {
                ai = new RandomGameAI();
                Message(AI_PLAY_MESSAGE);
                Message(string.Format(AI_TURN_MESSAGE, Moves.One));
                CandyCount -= 1;
            }
            else if (GameMode == GameModes.SinglePlayerHard)
            {
                ai = new SmartGameAI();
                Message(SMART_AI_PLAY_MESSAGE);
                Message(string.Format(AI_TURN_MESSAGE, Moves.One));
                CandyCount -= 1;
            }
            else
            {
                Message(string.Format(PLAY_MESSAGE, mode));
            }
        }

        public void TakeTurn(Moves currentMove)
        {
            if (!MoveIsValid(currentMove))
                return;

            // Process the player's turn
            Message(string.Format(END_TURN_MESSAGE, CurrentPlayer, currentMove));
            CandyCount -= (int)currentMove;

            if (GameMode == GameModes.TwoPlayer)
            {
                // Single Player Game Over
                if (CandyCount == 0)
                {
                    GamePlaying = false;
                    Message(string.Format(END_GAME_MESSAGE, OtherPlayer));
                    return;
                }

                TogglePlayer();
            }
            else
            {
                TakeAITurn(currentMove);
                
            }
        }

        private bool MoveIsValid(Moves currentMove)
        {
            // Example of throwing an exception when we reach an unexpected scenario
            if (CandyCount == 0)
            {
                Message("We have no candies and someone tried to take some!");
                return false;
            }
                

            if ((int)currentMove > CandyCount)
            {
                Message(string.Format(TOO_FEW_CANDIES_MESSAGE, currentMove, CandyCount));
                return false;
            }

            return true;
        }

        private void TakeAITurn(Moves currentMove)
        {
            // Single Player Wins!
            if (CandyCount == 0)
            {
                GamePlaying = false;
                Message(AI_LOSS_MESSAGE);
            }
            else
            {
                Moves aiChoice = ai.TakeTurn(currentMove, candyCount);

                Message(string.Format(AI_TURN_MESSAGE, aiChoice));
                CandyCount -= (int)aiChoice;

                // If the game is over
                if (CandyCount == 0)
                {
                    GamePlaying = false;
                    Message(AI_VICTORY_MESSAGE);
                }
            }
        }

        private void TogglePlayer()
        {

            CurrentPlayer = OtherPlayer;
            Message(string.Format(TURN_MESSAGE, CurrentPlayer));
        }

        private void Message(string message)
        {
            MessageEvent?.Invoke(message);
        }

        #region INotifyPropertyChanged Implementation
        private void ValueChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}