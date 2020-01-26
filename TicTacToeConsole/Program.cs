using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TicTacToeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t ~~ WELCOME to TiTacToe Game ~~");

            string[] board = new string[9];
            var firstPlayerName = "";
            var secondPlayerName = "";
            string[] markers = new string[2];
            var firstMarker = "";
            var secondMarker = "";
            var turn = "";

            while (true)
            {
                if (Methods.yesNoQuestion("loadGame"))
                {
                    Console.Write("Plaese input correct file name with path: ");
                    var pathName = Console.ReadLine();
                    var gameEntity = Methods.loadTheGame(pathName);

                    board = gameEntity.GameBoard;
                    firstPlayerName = gameEntity.FirstPlayerName;
                    secondPlayerName = gameEntity.SecondPlayerName;
                    markers = gameEntity.Markers;
                    firstMarker = markers[0];
                    secondMarker = markers[1];
                    turn = gameEntity.Turn;
                }
                else
                {
                    board = new string[9] { " ", " ", " ", " ", " ", " ", " ", " ", " " };

                    Console.Clear();
                    Console.Write("Input first player name: ");
                    firstPlayerName = Console.ReadLine();

                    Console.Write("Input second player name: ");
                    secondPlayerName = Console.ReadLine();

                    markers = new string[2];

                    Methods.fillMarkers(markers, firstPlayerName);
                    firstMarker = markers[0];
                    secondMarker = markers[1];

                    turn = Methods.firstTurn(firstPlayerName, secondPlayerName);
                    Console.WriteLine(turn + " will go first");
                    Console.Write("Please, press Enter ...");
                    Console.ReadLine();
                }

                bool gameOn = true;

                while (gameOn)
                {
                    if (turn == firstPlayerName)
                    {
                        Console.Clear();
                        Methods.drawBoard(board);

                        if (Methods.yesNoQuestion("saveGame"))
                        {
                            GameEntity ge = new GameEntity(board, markers, turn, firstPlayerName, secondPlayerName);
                            Console.Write("Plaese, input correct path and file name: ");
                            var fileName = Console.ReadLine();
                            Methods.saveTheGame(fileName, ge);
                            break;
                        }


                        int position = Methods.checkIntPosition(firstPlayerName);
                        Methods.placeMarker(board, firstMarker, position);

                        if (Methods.winCheck(board, firstMarker))
                        {
                            Console.Clear();
                            Console.WriteLine("\t\t\t !!! ~~ Congrats ~~ !!! " + firstPlayerName + " ! won !");
                            Methods.drawBoard(board);
                            gameOn = false;
                        }
                        else
                        {
                            if (Methods.fullBoardCheck(board))
                            {
                                Console.Clear();
                                Console.WriteLine("Game is draw ...");
                                Methods.drawBoard(board);
                                break;
                            }
                            else
                                turn = secondPlayerName;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Methods.drawBoard(board);

                        int position = Methods.checkIntPosition(secondPlayerName);
                        Methods.placeMarker(board, secondMarker, position);

                        if (Methods.winCheck(board, secondMarker))
                        {
                            Console.Clear();
                            Console.WriteLine("\t\t\t !!! ~~ Congrats ~~ !!! " + secondPlayerName + " ! won !");
                            Methods.drawBoard(board);
                            gameOn = false;
                        }
                        else
                        {
                            if (Methods.fullBoardCheck(board))
                            {
                                Console.WriteLine("Game is draw ...");
                                Methods.drawBoard(board);
                                break;
                            }
                            else
                                turn = firstPlayerName;
                        }
                    }
                }

                if (!Methods.yesNoQuestion("playAgain"))
                    break;

                Console.WriteLine();
            }
        }
    }
}
