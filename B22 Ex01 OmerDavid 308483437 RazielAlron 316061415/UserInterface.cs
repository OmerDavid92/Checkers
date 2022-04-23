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
            return i_UserInputBoardSize == ((int)Enum.BoardSize.Small).ToString()
                || i_UserInputBoardSize == ((int)Enum.BoardSize.Medium).ToString()
                || i_UserInputBoardSize == ((int)Enum.BoardSize.Large).ToString();
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
            return i_UserInputPlayerType == ((int)Enum.PlayerType.Human).ToString()
                || i_UserInputPlayerType == ((int)Enum.PlayerType.PC).ToString();
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

        public static void PrintBoard(Board i_Board)
        {
            char columnSign = 'A';
            char rowSign = 'a';
            char printedSign = '\0';
            char[,] board = i_Board.m_Board;

            Console.Write(" ");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("  {0}  ", columnSign);
                columnSign++;
            }

            Console.WriteLine();
            Console.Write("=");
            Console.WriteLine(new string('=', board.GetLength(0) * 5));

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("{0}|", rowSign);
                rowSign++;

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(board[i, j] == '\0')
                    {
                        printedSign = ' ';
                    }
                    else
                    {
                        printedSign = board[i, j];
                    }

                    Console.Write("  {0} |", printedSign);
                }

                Console.WriteLine();
                Console.Write("=");
                Console.WriteLine(new string('=', board.GetLength(0) * 5));
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
                && i_UserInputTurn[1] >= 'a' && i_UserInputTurn[1] <= 'z'
                && i_UserInputTurn[2] == '>'
                && i_UserInputTurn[3] >= 'A' && i_UserInputTurn[3] <= 'Z'
                && i_UserInputTurn[4] >= 'a' && i_UserInputTurn[4] <= 'z')
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
                Console.WriteLine("Wrong Input, Please enter turn again");
                turnInput = Console.ReadLine();
            }

            if (turnInput != "Q")
            {
                pointX = (int)(turnInput[1] - 'a');
                pointY = (int)(turnInput[0] - 'A');
                o_SourcePosition = new Point(pointX, pointY);

                pointX = (int)(turnInput[4] - 'a');
                pointY = (int)(turnInput[3] - 'A');
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

        public static void PrintWinnerGame(Player i_Winner)
        {
            Console.WriteLine("~~~Game Over~~~");

            if(i_Winner != null)
            {
                Console.WriteLine("{0} ({1}) Won the Game with {2} wins!", i_Winner.m_PlayerName, i_Winner.m_ToolSign.m_TrooperSign, i_Winner.m_Score);
            }
            else
            {
                Console.WriteLine("It's a TIE!");
            }
        }

        public static void InvalidTurnMessage()
        {
            Console.WriteLine("Invalid turn made, please enter a valid turn");
        }
    }
}
