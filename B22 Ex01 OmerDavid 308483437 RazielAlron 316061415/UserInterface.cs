using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class UserInterface
    {
        private static bool validateInputBoardSize(string i_UserInputBoardSize)
        {
            return (i_UserInputBoardSize == "6" || i_UserInputBoardSize == "8" || i_UserInputBoardSize == "10");
        }

        public static int GetUserInputBoardSize()
        {
            string i_UserInputBoardSize;

            Console.WriteLine("Please Enter the board size 6/8/10: ");
            i_UserInputBoardSize = Console.ReadLine();
            while (validateInputBoardSize(i_UserInputBoardSize))
            {
                Console.WriteLine("Wrong Input, Please enter 6/8/10");
                i_UserInputBoardSize = Console.ReadLine();
            }

            return int.Parse(i_UserInputBoardSize);
        }


    }
}
