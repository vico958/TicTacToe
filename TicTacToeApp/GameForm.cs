namespace TicTacToeApp
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class GameForm : Form
    {
        private readonly Label r_LabelTitleOfPlayers = new Label();
        private readonly Label r_LabelPlayerOne = new Label();
        private readonly Label r_LabelPlayerTwo = new Label();
        private readonly TextBox r_TextBoxNameOfPlayerOne = new TextBox();
        private readonly CheckBox r_CheckBoxPlayerVsPlayer = new CheckBox();
        private readonly TextBox r_TextBoxNameOfPlayerTwo = new TextBox();
        private readonly Label r_LabelBorardSize = new Label();
        private readonly Label r_LabelRowsInBoard = new Label();
        private readonly Label r_LabelColsInBoard = new Label();
        private readonly NumericUpDown r_NumericUpDownSizeOfRowsInBoard = new NumericUpDown();
        private readonly NumericUpDown r_NumericUpDownSizeOfColsInBoard = new NumericUpDown();
        private readonly Button r_ButtonStart = new Button();
        private readonly int r_MaximumSizeOfBoard = 9;
        private readonly int r_MinimumSizeOfBoard = 3;

        public GameForm()
        {
            string stringTextInButtonStart = "Start!";

            gameFormSetting();
            gameSettingInitailLabel();
            gameSettingInitialNumbericUpDown();
            gameSettingInitialCheckBoxAndTextBox();
            initializeButton(r_ButtonStart, r_LabelBorardSize.Location.X, r_LabelBorardSize.Location.Y + 70, stringTextInButtonStart);
            this.ShowDialog();
        }

        private void initializeLabel(string i_TextOfLabel, int i_PostionX, int i_PostionY, Label i_LabelToInitialize)
        {
            i_LabelToInitialize.Text = i_TextOfLabel;
            i_LabelToInitialize.Location = new Point(i_PostionX, i_PostionY);
            i_LabelToInitialize.AutoSize = true;
            this.Controls.Add(i_LabelToInitialize);
        }

        private void initializeCheckBoxPlayerVsPlayer()
        {
            r_CheckBoxPlayerVsPlayer.CheckStateChanged += playerVsPlayerChekcBox_Click;
            r_CheckBoxPlayerVsPlayer.Location = new Point(r_LabelPlayerTwo.Location.X - 20, r_LabelPlayerTwo.Location.Y);
            r_CheckBoxPlayerVsPlayer.AutoSize = true;
            this.Controls.Add(r_CheckBoxPlayerVsPlayer);
        }

        private void initializeTextBoxPlayerOne()
        {
            r_TextBoxNameOfPlayerOne.Location = new Point(r_TextBoxNameOfPlayerTwo.Location.X, r_LabelPlayerOne.Location.Y);
            this.Controls.Add(r_TextBoxNameOfPlayerOne);
        }

        private void initializeTextBoxPlayerTwo()
        {
            r_TextBoxNameOfPlayerTwo.Location = new Point(r_LabelPlayerTwo.Location.X + 60, r_LabelPlayerTwo.Location.Y);
            r_TextBoxNameOfPlayerTwo.Enabled = false;
            r_TextBoxNameOfPlayerTwo.Text = "[Computer]";
            this.Controls.Add(r_TextBoxNameOfPlayerTwo);
        }

        private void initializeNumericUpDown(NumericUpDown i_NumberOfSizeOfBoard, int i_PostionX, int i_PostionY)
        {
            i_NumberOfSizeOfBoard.Minimum = r_MinimumSizeOfBoard;
            i_NumberOfSizeOfBoard.Maximum = r_MaximumSizeOfBoard;
            i_NumberOfSizeOfBoard.Location = new Point(i_PostionX, i_PostionY);
            i_NumberOfSizeOfBoard.AutoSize = true;
            i_NumberOfSizeOfBoard.Size = new Size(40, 26);
            i_NumberOfSizeOfBoard.ValueChanged += changeValueOfSizeOfBoard_Click;
            this.Controls.Add(i_NumberOfSizeOfBoard);
        }

        private void initializeButton(Button i_ButtonStart, int i_PostionX, int i_PostionY, string i_TextInButton)
        {
            i_ButtonStart.Location = new Point(i_PostionX, i_PostionY);
            i_ButtonStart.Size = new Size(220, 25);
            i_ButtonStart.Text = i_TextInButton;
            i_ButtonStart.Click += buttonStart_Click;
            this.Controls.Add(i_ButtonStart);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string nameOfPlayerTwo, nameOfPlayerOne;
            int sizeOfBoard, numOfPlayers = 2;

            if (checkIfOneOfTheTextBoxNameAreEmpty() == false)
            {
                this.Hide();
                if (r_TextBoxNameOfPlayerTwo.Enabled == false)
                {
                    nameOfPlayerTwo = "Coumputer";
                    numOfPlayers = 1;
                }
                else
                {
                    nameOfPlayerTwo = r_TextBoxNameOfPlayerTwo.Text;
                }

                nameOfPlayerOne = r_TextBoxNameOfPlayerOne.Text;
                sizeOfBoard = (int)r_NumericUpDownSizeOfRowsInBoard.Value;
                TicTacToeMisereForm board = new TicTacToeMisereForm(sizeOfBoard, nameOfPlayerOne, nameOfPlayerTwo, numOfPlayers);
            }
        }

        private bool checkIfOneOfTheTextBoxNameAreEmpty()
        {
            bool isTextBoxNameOfPlayerOneEmpty, isTextBoxNameOfPlayerTwoEmpty, isOneOfTheTextBoxEmpty;

            isTextBoxNameOfPlayerOneEmpty = string.IsNullOrEmpty(r_TextBoxNameOfPlayerOne.Text);
            isTextBoxNameOfPlayerTwoEmpty = string.IsNullOrEmpty(r_TextBoxNameOfPlayerTwo.Text);
            isOneOfTheTextBoxEmpty = (isTextBoxNameOfPlayerOneEmpty == true) || (isTextBoxNameOfPlayerTwoEmpty == true);
            if (isOneOfTheTextBoxEmpty == true)
            {
                MessageBox.Show("you need to start the game with names of players that are not empty");
            }

            return isOneOfTheTextBoxEmpty;
        }

        private void changeValueOfSizeOfBoard_Click(object sender, EventArgs e)
        {
            NumericUpDown numericUpDownSizeBoard = sender as NumericUpDown;

            if (numericUpDownSizeBoard == r_NumericUpDownSizeOfRowsInBoard)
            {
                r_NumericUpDownSizeOfColsInBoard.Value = r_NumericUpDownSizeOfRowsInBoard.Value;
            }
            else
            {
                r_NumericUpDownSizeOfRowsInBoard.Value = r_NumericUpDownSizeOfColsInBoard.Value;
            }
        }

        private void playerVsPlayerChekcBox_Click(object sender, EventArgs e)
        {
            r_TextBoxNameOfPlayerTwo.Enabled = r_CheckBoxPlayerVsPlayer.CheckState == CheckState.Checked;
            if (r_TextBoxNameOfPlayerTwo.Enabled == false)
            {
                r_TextBoxNameOfPlayerTwo.Text = "[Computer]";
            }
            else
            {
                r_TextBoxNameOfPlayerTwo.Text = string.Empty;
            }
        }

        private void gameSettingInitialCheckBoxAndTextBox()
        {
            initializeCheckBoxPlayerVsPlayer();
            initializeTextBoxPlayerTwo();
            initializeTextBoxPlayerOne();
        }

        private void gameSettingInitialNumbericUpDown()
        {
            initializeNumericUpDown(r_NumericUpDownSizeOfRowsInBoard, r_LabelRowsInBoard.Location.X + 40, r_LabelRowsInBoard.Location.Y);
            initializeNumericUpDown(r_NumericUpDownSizeOfColsInBoard, r_LabelColsInBoard.Location.X + 40, r_LabelColsInBoard.Location.Y);
        }

        private void gameFormSetting()
        {
            this.Text = "Game Properties";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(this.DefaultSize.Width - 50, this.DefaultSize.Height - 20);
        }

        private void gameSettingInitailLabel()
        {
            int xOfLabelTitleOfPlayers, yOfLabelTitleOfPlayers, xOfLabelPlayerTwo, yOfLabelPlayerTwo;
            int xOfLabelColsInBoard, yOfLabelColsInBoard, xOfLabelRowsInBoard, yOfLabelRowsInBoard, yOfLabelBorardSize;

            initializeLabel("Players:", 10, 20, r_LabelTitleOfPlayers);
            xOfLabelTitleOfPlayers = r_LabelTitleOfPlayers.Location.X;
            yOfLabelTitleOfPlayers = r_LabelTitleOfPlayers.Location.Y + 30;
            initializeLabel("Player 1:", xOfLabelTitleOfPlayers + 20, yOfLabelTitleOfPlayers, r_LabelPlayerOne);
            xOfLabelPlayerTwo = r_LabelPlayerOne.Location.X + 15;
            yOfLabelPlayerTwo = r_LabelPlayerOne.Location.Y + 30;
            initializeLabel("Player 2:", xOfLabelPlayerTwo, yOfLabelPlayerTwo, r_LabelPlayerTwo);
            yOfLabelBorardSize = r_LabelPlayerTwo.Location.Y + 50;
            initializeLabel("Board Size:", xOfLabelTitleOfPlayers, yOfLabelBorardSize, r_LabelBorardSize);
            xOfLabelColsInBoard = r_LabelBorardSize.Location.X + 20;
            yOfLabelColsInBoard = r_LabelBorardSize.Location.Y + 20;
            initializeLabel("Rows:", xOfLabelColsInBoard, yOfLabelColsInBoard, r_LabelColsInBoard);
            xOfLabelRowsInBoard = r_LabelRowsInBoard.Location.X + 120;
            yOfLabelRowsInBoard = r_LabelBorardSize.Location.Y + 20;
            initializeLabel("Cols:", xOfLabelRowsInBoard, yOfLabelRowsInBoard, r_LabelRowsInBoard);
        }
    }
}