using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TicTacToeConsole
{
    public static class Methods
    {
        /// <summary>
        /// Draws a playing board in console
        /// </summary>
        /// <param name="_board">string array</param>
        public static void drawBoard(string[] _board)
        {
            Console.WriteLine(" " + _board[0] + " " + "|" + " " + _board[1] + " " + "|" + " " + _board[2]);
            Console.WriteLine("--- --- ---");
            Console.WriteLine(" " + _board[3] + " " + "|" + " " + _board[4] + " " + "|" + " " + _board[5]);
            Console.WriteLine("--- --- ---");
            Console.WriteLine(" " + _board[6] + " " + "|" + " " + _board[7] + " " + "|" + " " + _board[8]);
        }

        /// <summary>
        /// Sets player marker on board position and check available to set marker in current position
        /// </summary>
        /// <param name="_board">string array</param>
        /// <param name="_marker">string Players marker</param>
        /// <param name="_position">int selected Players position</param>
        /// <returns>
        /// The board with markers
        /// </returns>
        public static string[] placeMarker(string[] _board, string _marker, int _position)
        {
            while ((_position - 1 > 8 || _position - 1 < 0) || _board[_position - 1] != " ")
            {
                Console.WriteLine("Wrong position ...");
                Console.Write("Your next position: ");
                _position = Convert.ToInt32(Console.ReadLine());
            }
            _board[_position - 1] = _marker;
            return _board;
        }

        /// <summary>
        /// Checks integer position value 
        /// </summary>
        /// <param name="_name">strin Players name</param>
        /// <returns>
        /// positions if int
        /// </returns>
        public static int checkIntPosition(string _name)
        {
            int position = 0;
            bool success = false;
            while (!success)
            {
                Console.Write(_name + ", please input your position: ");
                success = int.TryParse(Console.ReadLine(), out position);
            }
            return position;
        }

        /// <summary>
        /// Defines a players with first turn
        /// </summary>
        /// <param name="_firstName">string first Players name</param>
        /// <param name="_secondName">string second Players name</param>
        /// <returns>
        /// string name of player with first turn
        /// </returns>
        public static string firstTurn(string _firstName, string _secondName)
        {
            var rnd = new Random();
            var choise = rnd.Next(2);

            if (choise == 0)
                return _firstName;
            else
                return _secondName;
        }


        /// <summary>
        /// Fills an array by selected markers
        /// </summary>
        /// <param name="_markers">string a Players marker</param>
        /// <param name="_name">string a Players name</param>
        /// <returns>
        /// array of markers. [0] - First Player, [1] - Second Player
        /// </returns>
        public static string[] fillMarkers(string[] _markers, string _name)
        {
            var temp = "";
            while (!(temp == "X" || temp == "O"))
            {
                Console.Write(_name + ": do you want to X or O? --> ");
                temp = Console.ReadLine().ToUpper();
            }
            if (temp == "X")
            {
                _markers[0] = "X";
                _markers[1] = "O";
            }
            else
            {
                _markers[0] = "O";
                _markers[1] = "X";
            }

            return _markers;
        }

        /// <summary>
        /// Technical method for set " " on board
        /// </summary>
        /// <param name="_board"></param>
        /// <param name="_position"></param>
        /// <returns>
        /// a board position - " "
        /// </returns>
        public static bool spaceCheck(string[] _board, int _position)
        {
            return _board[_position] == " ";
        }

        /// <summary>
        /// Checks fill a board
        /// </summary>
        /// <param name="_board">string array</param>
        /// <returns>
        /// bool value
        /// </returns>
        public static bool fullBoardCheck(string[] _board)
        {
            for (int i = 0; i < _board.Length; i++)
                if (spaceCheck(_board, i))
                    return false;
            return true;
        }

        /// <summary>
        /// Check a Players win
        /// </summary>
        /// <param name="_board">string array</param>
        /// <param name="_marker">string Players marker</param>
        /// <returns>
        /// bool value
        /// </returns>
        public static bool winCheck(string[] _board, string _marker)
        {
            return ((_board[0] == _marker && _board[1] == _marker && _board[2] == _marker) ||
                    (_board[3] == _marker && _board[4] == _marker && _board[5] == _marker) ||
                    (_board[6] == _marker && _board[7] == _marker && _board[8] == _marker) ||
                    (_board[0] == _marker && _board[4] == _marker && _board[8] == _marker) ||
                    (_board[2] == _marker && _board[4] == _marker && _board[6] == _marker) ||
                    (_board[0] == _marker && _board[3] == _marker && _board[6] == _marker) ||
                    (_board[1] == _marker && _board[4] == _marker && _board[7] == _marker) ||
                    (_board[2] == _marker && _board[5] == _marker && _board[8] == _marker));
        }

        /// <summary>
        /// Ask to play again. any words starts on "y" will be - "yes"
        /// </summary>
        /// <param name="_choise">string choise to select varios question. playAgain - play again, saveGame - save the game, loadGame - load the game</param>
        /// <returns>
        /// bool value
        /// </returns>
        public static bool yesNoQuestion(string _choise)
        {
            switch (_choise)
            {
                case "playAgain":
                    Console.Write("Do you want play again? (Y/N): ");
                    break;
                case "saveGame":
                    Console.Write("Do you want to save the game? (Y/N): ");
                    break;
                case "loadGame":
                    Console.Write("Do you want load the game? (Y/N): ");
                    break;
                default:
                    break;
            }
            var answer = Console.ReadLine();
            return answer.Substring(0, 1) == "Y" || answer.Substring(0, 1) == "y";
        }

        /// <summary>
        /// Save the current game
        /// </summary>
        /// <param name="_pathNameFile">string path and file name from player</param>
        /// <param name="_gameEntity">GameEntity object current game</param>
        /// <returns>
        /// Bool value true or false saving
        /// </returns>
        public static bool saveTheGame(string _pathNameFile, GameEntity _gameEntity)
        {
            try
            {
                using (FileStream fs = new FileStream(_pathNameFile, FileMode.OpenOrCreate))
                {
                    JsonSerializer.SerializeAsync<GameEntity>(fs, _gameEntity);
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Can't save the game. Please check path and filename.");
                return false;
            }
        }

        /// <summary>
        /// Load the game
        /// </summary>
        /// <param name="_pathNameFile">string path + filename</param>
        /// <returns>
        /// GameEntity obj in case correct filename and empty GameEntity obj in case a fault
        /// </returns>
        public static GameEntity loadTheGame(string _pathNameFile)
        {
            try
            {
                using(FileStream fs = new FileStream(_pathNameFile, FileMode.OpenOrCreate))
                {
                    var ge = JsonSerializer.DeserializeAsync<GameEntity>(fs);
                    GameEntity nge = new GameEntity(ge.Result.GameBoard, ge.Result.Markers, ge.Result.Turn, ge.Result.FirstPlayerName, ge.Result.SecondPlayerName);
                    return nge;
                }
            }
            catch
            {
                Console.WriteLine("Sorry ... path, file name or file is incorrect. Something wrong. Try next time ...");
                Console.ReadLine();
                return new GameEntity();
            }
        }
    }
}
