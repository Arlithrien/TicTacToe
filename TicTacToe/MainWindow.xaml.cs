using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Array holding the status of the TicTacToe board
        /// </summary>
        private SymbolUsed[] boardStatus;

        /// <summary>
        /// True means it's player 1's turn, false means it's player 2's turn
        /// </summary>
        private bool player1Turn;

        /// <summary>
        /// True if game is over, otherwise false
        /// </summary>
        private bool gameOver;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// stats a new game, clears all values to start
        /// </summary>
        private void NewGame()
        {
            ///initialize new array of empty cells
            boardStatus = new SymbolUsed[9];

            for (var i = 0; i < boardStatus.Length; i++)
                boardStatus[i] = SymbolUsed.Empty;
            player1Turn = true;

            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
               

            });

            // make sure game hasnt finished
            gameOver = false;

        }

        /// <summary>
        /// handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //If the game has ended, a new game will start on next click
            if (gameOver)
            {
                NewGame();
                return;
            }
            
            // Cast the sender to a button
            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            if (boardStatus[index] != SymbolUsed.Empty)
                return;

            // dont do anything if the cell is already occupied
            boardStatus[index] = player1Turn ? SymbolUsed.Cross : SymbolUsed.Circle;

            //set button text to result
            button.Content = player1Turn ? 'X' : 'O';

            //change turns
            player1Turn = !player1Turn;

            if (player1Turn)
                button.Foreground = Brushes.Green;
            else
                button.Foreground = Brushes.Blue;

            checkForWinner();


        }

        private void checkForWinner()
        {
            if (boardStatus[0] != SymbolUsed.Empty && (boardStatus[0] & boardStatus[1] & boardStatus[2]) == boardStatus[0])
            {
                gameOver = true;
                button0_0.Background = button1_0.Background = button2_0.Background = Brushes.Green;
            }
            if(boardStatus[3] != SymbolUsed.Empty && (boardStatus[3] & boardStatus[4] & boardStatus[5]) == boardStatus[3])
            {
                gameOver = true;
                button0_1.Background = button1_1.Background = button2_1.Background = Brushes.Green;
            }
            if(boardStatus[6] != SymbolUsed.Empty && (boardStatus[6] & boardStatus[7] & boardStatus[8]) == boardStatus[6])
            {
                gameOver = true;
                button0_2.Background = button1_2.Background = button2_2.Background = Brushes.Green;
            }
            if(boardStatus[0] != SymbolUsed.Empty && (boardStatus[0] & boardStatus[3] & boardStatus[6]) == boardStatus[0])
            {
                gameOver = true;
                button0_0.Background = button0_1.Background = button0_2.Background = Brushes.Green;
            }
            if(boardStatus[1] != SymbolUsed.Empty && (boardStatus[1] & boardStatus[4] & boardStatus[7]) == boardStatus[1])
            {
                gameOver = true;
                button1_0.Background = button1_1.Background = button1_2.Background = Brushes.Green;
            }
            if(boardStatus[2] != SymbolUsed.Empty && (boardStatus[2] & boardStatus[5] & boardStatus[8]) == boardStatus[2])
            {
                gameOver = true;
                button2_0.Background = button2_1.Background = button2_2.Background = Brushes.Green;
            }
            if(boardStatus[0] != SymbolUsed.Empty && (boardStatus[0] & boardStatus[4] & boardStatus[8]) == boardStatus[0])
            {
                gameOver = true;
                button0_0.Background = button1_1.Background = button2_2.Background = Brushes.Green;
            }
            if (boardStatus[2] != SymbolUsed.Empty && (boardStatus[2] & boardStatus[4] & boardStatus[6]) == boardStatus[2])
            {
                gameOver = true;
                button0_2.Background = button1_1.Background = button2_0.Background = Brushes.Green;
            }


            if (!boardStatus.Any(f => f == SymbolUsed.Empty))
            {
                gameOver = true;     
                button0_0.Background = button1_0.Background = button2_0.Background = Brushes.Red;
                button0_1.Background = button1_1.Background = button2_1.Background = Brushes.Red;
                button0_2.Background = button1_2.Background = button2_2.Background = Brushes.Red;

            }
        }
    }
}
