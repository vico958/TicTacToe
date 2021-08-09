namespace TicTacToeApp
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using TicTacToeLogic;

    public class TicTacToeMisereForm : Form
    {
        private readonly ButtonWithPostion[,] r_ButtonsWithPostionBoard;
        private readonly Label r_LabelPlayerOne = new Label();
        private readonly Label r_LabelPlayerTwo = new Label();
        private readonly Label r_LabelPlayerOneScore = new Label();
        private readonly Label r_LabelPlayerTwoScore = new Label();
        private readonly int r_NumberOfPlayers;
        private readonly Game r_TicTacToe;
        private readonly int r_SpaceBetweenButtons = 6;
        private readonly int r_SizeOfPixelOfButtonOfBoard = 50;
        private readonly string r_NameOfPlayerOne;
        private readonly string r_NameOfPlayerTwo;

        public TicTacToeMisereForm(int i_SizeOfBoard, string i_NameOfPlayerOne, string i_NameOfPlayerTwo, int i_NumberOfPlayers)
        {
            r_NameOfPlayerOne = i_NameOfPlayerOne;
            r_NameOfPlayerTwo = i_NameOfPlayerTwo;
            r_NumberOfPlayers = i_NumberOfPlayers;
            r_TicTacToe = new Game(r_NumberOfPlayers, i_SizeOfBoard);
            r_ButtonsWithPostionBoard = new ButtonWithPostion[i_SizeOfBoard, i_SizeOfBoard];
            settingTheBoardGame(i_SizeOfBoard);
            this.ShowDialog();
        }

        private void settingTheBoardGame(int i_SizeOfBoard)
        {
            int xOfLabelPlayerOneScore, xOfLabelPlayerTwoScore, xOfLabelPlayerOne, xOfLabelPlayerTwo;
            int yOfLabelPlayerOne;
            string scoreOfPlayer;
            string nameOfPlayerOne = r_NameOfPlayerOne + ":";
            string nameOfPlayerTwo = r_NameOfPlayerTwo + ":";

            designFormOfTicTacToe(i_SizeOfBoard);
            initializerBoardButtons(i_SizeOfBoard);
            xOfLabelPlayerOne = (this.Width - r_LabelPlayerOne.Width) / 2;
            yOfLabelPlayerOne = this.Height - 60;
            initializeLabel(nameOfPlayerOne, xOfLabelPlayerOne, yOfLabelPlayerOne, r_LabelPlayerOne);
            xOfLabelPlayerTwo = r_LabelPlayerOne.Location.X + r_LabelPlayerOne.Width + 15;
            initializeLabel(nameOfPlayerTwo, xOfLabelPlayerTwo, r_LabelPlayerOne.Location.Y, r_LabelPlayerTwo);
            scoreOfPlayer = r_TicTacToe.Players[0].Score.ToString();
            xOfLabelPlayerOneScore = r_LabelPlayerOne.Location.X + r_LabelPlayerOne.Width;
            initializeLabel(scoreOfPlayer, xOfLabelPlayerOneScore, r_LabelPlayerOne.Location.Y, r_LabelPlayerOneScore);
            xOfLabelPlayerTwoScore = r_LabelPlayerTwo.Location.X + r_LabelPlayerTwo.Width;
            initializeLabel(scoreOfPlayer, xOfLabelPlayerTwoScore, r_LabelPlayerTwo.Location.Y, r_LabelPlayerTwoScore);
            decidingWhichLabelToChangeToBoltAndWhichToNormal();
        }

        private void designFormOfTicTacToe(int i_SizeOfBoard)
        {
            int sizeOfSpaceBtwButtonAndPixelOfButton = r_SpaceBetweenButtons + r_SizeOfPixelOfButtonOfBoard;
            int sizeOfWidthOfForm = (i_SizeOfBoard * sizeOfSpaceBtwButtonAndPixelOfButton) + 30;
            int sizeOfHeightOfForm = (i_SizeOfBoard * sizeOfSpaceBtwButtonAndPixelOfButton) + 70;

            this.Text = "TicTacToeMisere";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(sizeOfWidthOfForm, sizeOfHeightOfForm);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void initializeLabel(string i_NameOfLabel, int i_PostionX, int i_PostionY, Label i_Label)
        {
            i_Label.Text = i_NameOfLabel;
            i_Label.Location = new Point(i_PostionX, i_PostionY);
            i_Label.AutoSize = true;
            this.Controls.Add(i_Label);
        }

        private void initializerBoardButtons(int i_SizeOfBoard)
        {
            int previesButtonPostionX, previesButtonPostionY;

            for (int row = 0; row < i_SizeOfBoard; row++)
            {
                for (int col = 0; col < i_SizeOfBoard; col++)
                {
                    r_ButtonsWithPostionBoard[row, col] = new ButtonWithPostion(row, col);
                    r_ButtonsWithPostionBoard[row, col].Size = new Size(r_SizeOfPixelOfButtonOfBoard, r_SizeOfPixelOfButtonOfBoard);
                    r_ButtonsWithPostionBoard[row, col].Click += boardCell_Click;
                    if (row == 0 && col == 0)
                    {
                        r_ButtonsWithPostionBoard[0, 0].Location = new Point(10, 10);
                        this.Controls.Add(r_ButtonsWithPostionBoard[row, col]);
                        continue;
                    }

                    if (col != 0)
                    {
                        previesButtonPostionX = r_ButtonsWithPostionBoard[row, col - 1].Location.X + r_ButtonsWithPostionBoard[row, col - 1].Width + r_SpaceBetweenButtons;
                        previesButtonPostionY = r_ButtonsWithPostionBoard[row, col - 1].Location.Y;
                    }
                    else
                    {
                        previesButtonPostionX = r_ButtonsWithPostionBoard[0, 0].Location.X;
                        previesButtonPostionY = r_ButtonsWithPostionBoard[row - 1, col].Location.Y + r_ButtonsWithPostionBoard[row - 1, col].Height + r_SpaceBetweenButtons;
                    }

                    r_ButtonsWithPostionBoard[row, col].Location = new Point(previesButtonPostionX, previesButtonPostionY);
                    this.Controls.Add(r_ButtonsWithPostionBoard[row, col]);
                }
            }
        }

        private void boardCell_Click(object sender, EventArgs e)
        {
            ButtonWithPostion currentButtonPushed = sender as ButtonWithPostion;

            if (r_NumberOfPlayers == 2)
            {
                playerVsPlayer(currentButtonPushed);
            }
            else
            {
                playerVsComputer(currentButtonPushed);
            }
        }

        private void checkIfWeHaveWinnerOrTieAndShowMessage(string i_PlayerNameOfPotentialWinner, int i_Row, int i_Col, char i_Symbol, ref bool io_IsWinnerOrTie)
        {
            string messageToPlayersWhenGameFinish, titleOfMessageWhenGameFinish;

            io_IsWinnerOrTie = r_TicTacToe.WeHaveWinnerAndUpdateScoreIfNeed(i_Symbol, i_Row, i_Col);
            if (io_IsWinnerOrTie == true)
            {
                messageToPlayersWhenGameFinish = string.Format(
@"The winner is {0}!
Would you like to play another round?", i_PlayerNameOfPotentialWinner);
                titleOfMessageWhenGameFinish = "A Win!";
                messageWhenGameFinish(messageToPlayersWhenGameFinish, titleOfMessageWhenGameFinish);
                updateScoreLabels();
            }
            else
            {
                io_IsWinnerOrTie = r_TicTacToe.DoesGameIsTie();
                if (io_IsWinnerOrTie == true)
                {
                    messageToPlayersWhenGameFinish = @"Tie!
Would you like to play another round?";
                    titleOfMessageWhenGameFinish = "A Tie!";
                    messageWhenGameFinish(messageToPlayersWhenGameFinish, titleOfMessageWhenGameFinish);
                }
            }
        }

        private void updateScoreLabels()
        {
            r_LabelPlayerOneScore.Text = r_TicTacToe.Players[0].Score.ToString();
            if (r_NumberOfPlayers == 2)
            {
                r_LabelPlayerTwoScore.Text = r_TicTacToe.Players[1].Score.ToString();
            }
            else
            {
                r_LabelPlayerTwoScore.Text = r_TicTacToe.PcPlayer.Score.ToString();
            }
        }

        private void messageWhenGameFinish(string i_MessageWhenGameFinish, string i_TitleOfMessageWhenGameFinish)
        {
            DialogResult dialogMessageOfWineer = MessageBox.Show(i_MessageWhenGameFinish, i_TitleOfMessageWhenGameFinish, MessageBoxButtons.YesNo);

            if (dialogMessageOfWineer == DialogResult.Yes)
            {
                rematch();
            }
            else
            {
                this.Close();
            }
        }

        private void playerVsComputer(ButtonWithPostion i_CurrentButtonPushed)
        {
            int currentPlayerTurn = r_TicTacToe.CurrentPlayerTurn;
            int rowOfPcPlayer, colOfPcPlayer;
            char playerSymbol;
            bool isWinnerOrTie = false;

            playerSymbol = r_TicTacToe.Players[currentPlayerTurn - 1].Symbol;
            r_TicTacToe.StartTurnAndCheckIfMoveMade(i_CurrentButtonPushed.Row, i_CurrentButtonPushed.Col);
            i_CurrentButtonPushed.Text = r_TicTacToe.Players[currentPlayerTurn - 1].Symbol.ToString();
            disableButton(i_CurrentButtonPushed.Row, i_CurrentButtonPushed.Col);
            checkIfWeHaveWinnerOrTieAndShowMessage(r_NameOfPlayerTwo, i_CurrentButtonPushed.Row, i_CurrentButtonPushed.Col, playerSymbol, ref isWinnerOrTie);
            if (isWinnerOrTie == false)
            {
                playerSymbol = r_TicTacToe.PcPlayer.Symbol;
                decidingWhichLabelToChangeToBoltAndWhichToNormal();
                r_TicTacToe.StartTurnAndCheckIfMoveMade(i_CurrentButtonPushed.Row, i_CurrentButtonPushed.Col);
                rowOfPcPlayer = r_TicTacToe.PcPlayer.Row;
                colOfPcPlayer = r_TicTacToe.PcPlayer.Col;
                r_ButtonsWithPostionBoard[rowOfPcPlayer, colOfPcPlayer].Text = r_TicTacToe.PcPlayer.Symbol.ToString();
                disableButton(rowOfPcPlayer, colOfPcPlayer);
                checkIfWeHaveWinnerOrTieAndShowMessage(r_NameOfPlayerOne, rowOfPcPlayer, colOfPcPlayer, playerSymbol, ref isWinnerOrTie);
                decidingWhichLabelToChangeToBoltAndWhichToNormal();
            }
        }

        private void decidingWhichLabelToChangeToBoltAndWhichToNormal()
        {
            if (r_TicTacToe.CurrentPlayerTurn == r_TicTacToe.Players[0].PlayerId)
            {
                makingOneNameLabelFontToBoldAndOtherToDefault(r_LabelPlayerOne, r_LabelPlayerTwo, r_LabelPlayerOneScore, r_LabelPlayerTwoScore);
            }
            else
            {
                makingOneNameLabelFontToBoldAndOtherToDefault(r_LabelPlayerTwo, r_LabelPlayerOne, r_LabelPlayerTwoScore, r_LabelPlayerOneScore);
            }
        }

        private void makingOneNameLabelFontToBoldAndOtherToDefault(Label i_LabelToMakeBolt, Label i_LabelToMakeFontDefault, Label i_LabelScoreToMakeBolt, Label i_LabelScoreToMakeFontDefault)
        {
            i_LabelToMakeBolt.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            i_LabelScoreToMakeBolt.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            i_LabelToMakeFontDefault.Font = Label.DefaultFont;
            i_LabelScoreToMakeFontDefault.Font = Label.DefaultFont;
        }

        private void playerVsPlayer(ButtonWithPostion i_CurrentButtonPushed)
        {
            char playerSymbol;
            string nameOfPotionalWineer;
            int currentPlayerTurn = r_TicTacToe.CurrentPlayerTurn;
            bool isWinnerOrTie = false;

            if (currentPlayerTurn == r_TicTacToe.Players[0].PlayerId)
            {
                nameOfPotionalWineer = r_NameOfPlayerTwo;
            }
            else
            {
                nameOfPotionalWineer = r_NameOfPlayerOne;
            }

            r_TicTacToe.StartTurnAndCheckIfMoveMade(i_CurrentButtonPushed.Row, i_CurrentButtonPushed.Col);
            playerSymbol = r_TicTacToe.Players[currentPlayerTurn - 1].Symbol;
            i_CurrentButtonPushed.Text = r_TicTacToe.Players[currentPlayerTurn - 1].Symbol.ToString();
            disableButton(i_CurrentButtonPushed.Row, i_CurrentButtonPushed.Col);
            checkIfWeHaveWinnerOrTieAndShowMessage(nameOfPotionalWineer, i_CurrentButtonPushed.Row, i_CurrentButtonPushed.Col, playerSymbol, ref isWinnerOrTie);
            decidingWhichLabelToChangeToBoltAndWhichToNormal();
        }

        private void disableButton(int i_Row, int i_Col)
        {
            r_ButtonsWithPostionBoard[i_Row, i_Col].Enabled = false;
        }

        private void rematch()
        {
            foreach (ButtonWithPostion buttonInBoard in r_ButtonsWithPostionBoard)
            {
                buttonInBoard.Enabled = true;
                buttonInBoard.Text = string.Empty;
            }

            r_TicTacToe.StartRemach();
            decidingWhichLabelToChangeToBoltAndWhichToNormal();
        }
    }
}