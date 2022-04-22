using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class UserInterface
    {
        public static string GetUserInputPlayerName()
        {
            Console.WriteLine("Please enter Player Name: ");
            return Console.ReadLine();
        }

        private static bool validateInputBoardSize(string i_UserInputBoardSize)
        {
            return i_UserInputBoardSize == Enum.BoardSize.Small.ToString()
                || i_UserInputBoardSize == Enum.BoardSize.Medium.ToString()
                || i_UserInputBoardSize == Enum.BoardSize.Large.ToString();
        }

        public static Enum.BoardSize GetUserInputBoardSize()
        {
            string i_UserInputBoardSize;

            Console.WriteLine("Please Enter the board size 6/8/10: ");
            i_UserInputBoardSize = Console.ReadLine();

            while (!validateInputBoardSize(i_UserInputBoardSize))
            {
                Console.WriteLine("Wrong Input, Please enter 6/8/10");
                i_UserInputBoardSize = Console.ReadLine();
            }

            return (Enum.BoardSize)int.Parse(i_UserInputBoardSize);
        }

        private static bool validateInputPlayerType(string i_UserInputPlayerType)
        {
            return i_UserInputPlayerType == Enum.PlayerType.Human.ToString()
                || i_UserInputPlayerType == Enum.PlayerType.PC.ToString();
        }

        public static Enum.PlayerType GetUserInputPlayerType()
        {
            string PlayerType;

            Console.WriteLine("Do you want to play against PC or Player? [1 - PC / 2 - Player]");
            PlayerType = Console.ReadLine();

            while (!validateInputPlayerType(PlayerType))
            {
                Console.WriteLine("Wrong Input, Please enter 1/2 [1 - PC / 2 - Player]");
                PlayerType = Console.ReadLine();
            }

            return (Enum.PlayerType)int.Parse(PlayerType);
        }

        public static void PrintBoard(char[,] i_Board)
        {
            char columnSign = 'A';
            char rowSign = 'a';
            char printedSign = '\0';

            for (int i = 0; i < i_Board.GetLength(0); i++)
            {
                Console.Write("  {0}  ", columnSign);
                columnSign++;
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', i_Board.GetLength(0) * 4));

            for (int i = 0; i < i_Board.GetLength(0); i++)
            {
                Console.Write("{0}|", rowSign);
                rowSign++;

                for (int j = 0; j < i_Board.GetLength(1); j++)
                {
                    if(i_Board[i, j] == '\0')
                    {
                        printedSign = ' ';
                    }
                    else
                    {
                        printedSign = i_Board[i, j];
                    }

                    Console.Write("  {0} |", printedSign);
                }

                Console.WriteLine(new string('=', i_Board.GetLength(0) * 4));
            }
        }

        private static bool validateInputRematch(string i_UserInputRematch)
        {
            return i_UserInputRematch == "y" || i_UserInputRematch == "n";
        }

        public static bool GetUserInputIsRematch()
        {
            string rematchInput;

            Console.WriteLine("Do you want to Rematch? [y/n]");
            rematchInput = Console.ReadLine();

            while (!validateInputRematch(rematchInput))
            {
                Console.WriteLine("Wrong Input, Please enter y/n");
                rematchInput = Console.ReadLine();
            }

            return rematchInput == "y";
        }

        private static bool validateInputTurn(string i_UserInputTurn)
        {
            return (i_UserInputTurn.Length == 5
                && i_UserInputTurn[0] >= 'A' && i_UserInputTurn[0] <= 'Z'
                && i_UserInputTurn[1] >= 'A' && i_UserInputTurn[1] <= 'Z'
                && i_UserInputTurn[2] == '>'
                && i_UserInputTurn[3] >= 'A' && i_UserInputTurn[3] <= 'Z'
                && i_UserInputTurn[4] >= 'A' && i_UserInputTurn[4] <= 'Z')
                || i_UserInputTurn == "Q";
        }

        public static bool TryGetUserInputTurn(Player i_CurrentPlayingPlayer, ref Point o_SourcePosition, ref Point o_DestinationPosition)
        {
            string turnInput;
            int pointX = 0, pointY = 0;

            Console.Write("{0} Turn ({1}): ", i_CurrentPlayingPlayer.m_PlayerName, i_CurrentPlayingPlayer.m_ToolSign.m_TrooperSign);
            turnInput = Console.ReadLine();

            while (!validateInputTurn(turnInput))
            {
                Console.WriteLine("Wrong Input, Please enter y/n");
                turnInput = Console.ReadLine();
            }

            if (turnInput != "Q")
            {
                pointX = (int)(turnInput[0] - 'A');
                pointY = (int)(turnInput[1] - 'a');
                o_SourcePosition = new Point(pointX, pointY);

                pointX = (int)(turnInput[3] - 'A');
                pointY = (int)(turnInput[4] - 'a');
                o_DestinationPosition = new Point(pointX, pointY);
            }

            return turnInput != "Q";
        }

        public static void PrintWinnerMatch(Player i_MatchWinner)
        {
            Console.WriteLine("~~~Match Over~~~");

            if (i_MatchWinner != null)
            {
                Console.WriteLine("{0} Won the Match!", i_MatchWinner);
            }
            else
            {
                Console.WriteLine("It's A TIE!");
            }
        }

        public static void PrintWinnerGame(Player player1, Player player2)
        {
            Console.WriteLine("~~~Game Over~~~");
        }
    }
}
