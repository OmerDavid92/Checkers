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
        public FormGameSettings()
        {
            InitializeComponent();
        }

        public int GetBoardSize()
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

        public Player GetPlayer1()
        {
            Enum.PlayerType PlayerType;
            string playerName = Player1.Text;

            if (playerName == string.Empty)
            {
                playerName = "Player1";
            }

            return new Player(
                (char)Enum.Player1Tools.Trooper,
                (char)Enum.Player1Tools.King,
                Enum.PlayerType.Human,
                true,
                playerName);
        }

        public Player GetPlayer2()
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
                    false,
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

        private void FormGameSettings_Load(object sender, EventArgs e)
        {

        }

        private void submitSettings(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Player2CheckBox_Click(object sender, EventArgs e)
        {
            if (Player2CheckBox.Checked)
            {
                Player2TextBox.Text = "";
                Player2TextBox.Enabled = true;
            }
            else
            {
                Player2TextBox.Text = "[Computer]";
                Player2TextBox.Enabled = false;
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {

        }
    }
}
