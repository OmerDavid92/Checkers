using Checkers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enum = Checkers.Enum;

namespace CheckersForm
{
    public partial class FormGameSettings : Form
    {
        private FormBoard m_FormBoard = null;
        public FormGameSettings()
        {
            InitializeComponent();
        }

        private int getBoardSize()
        {
            int boardSize = 0;

            if (RadioButtonBoardSize6X6.Checked)
            {
                boardSize = 6;
            }
            if (RadioButtonBoardSize8X8.Checked)
            {
                boardSize = 8;
            }
            if (RadioButtonBoardSize10X10.Checked)
            {
                boardSize = 10;
            }

            return boardSize;
        }

        private Player getPlayer1()
        {
            Enum.PlayerType PlayerType;
            string playerName = Player1.Text;

            return new Player(
                (char)Enum.Player1Tools.Trooper,
                (char)Enum.Player1Tools.King,
                Enum.PlayerType.Human,
                true,
                playerName);
        }

        private Player getPlayer2()
        {
            Player player2;
            Enum.PlayerType PlayerType;
            string playerName = string.Empty;

            if (Player2CheckBox.Checked)
            {
                playerName = Player2TextBox.Text;
                player2 = new Player(
                    (char)Enum.Player2Tools.Trooper,
                    (char)Enum.Player2Tools.King,
                    Enum.PlayerType.Human,
                    true,
                    playerName);
            }
            else
            {
                playerName = "PC";
                player2 = new Player(
                (char)Enum.Player2Tools.Trooper,
                (char)Enum.Player2Tools.King,
                Enum.PlayerType.PC,
                false,
                playerName);
            }

            return player2;
        }


        private void submitSettings(object sender, MouseEventArgs e)
        {
            int boardSize = getBoardSize();
            Board board = new Board(boardSize);
            Player player1 = getPlayer1();
            Player player2 = getPlayer2();

            board.InitBoard(player1.m_ToolSign, player2.m_ToolSign);
            m_FormBoard = new FormBoard(board);
        }
    }
}
