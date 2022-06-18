namespace CheckersForm
{
    using System;
    using System.Windows.Forms;
    using Checkers;
    using Enum = Checkers.Enum;

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
            else if (RadioButtonBoardSize8X8.Checked)
            {
                boardSize = 8;
            }
            else if (RadioButtonBoardSize10X10.Checked)
            {
                boardSize = 10;
            }

            return boardSize;
        }

        public Player GetPlayer1()
        {
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

        private void submitSettings(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Player2CheckBox_Click(object sender, EventArgs e)
        {
            if (Player2CheckBox.Checked)
            {
                Player2TextBox.Text = string.Empty;
                Player2TextBox.Enabled = true;
            }
            else
            {
                Player2TextBox.Text = "[Computer]";
                Player2TextBox.Enabled = false;
            }
        }
    }
}
