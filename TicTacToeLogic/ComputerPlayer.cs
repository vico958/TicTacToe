namespace TicTacToeLogic
{
    using System;
    using System.Collections.Generic;

    public class ComputerPlayer
    {
        private readonly Random r_RandomNumbersToGenerate = new Random();
        private Player m_ComputerPlayer;
        private int m_Row;
        private int m_Col;

        public ComputerPlayer(char i_Symbol)
        {
            int playerId = 2;

            m_ComputerPlayer = new Player(playerId, i_Symbol);
            m_Row = 0;
            m_Col = 0;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }

        public int Score
        {
            get
            {
                return m_ComputerPlayer.Score;
            }

            set
            {
                m_ComputerPlayer.Score = value;
            }
        }

        public int PlayerId
        {
            get
            {
                return m_ComputerPlayer.PlayerId;
            }
        }

        public char Symbol
        {
            get
            {
                return m_ComputerPlayer.Symbol;
            }
        }

        public void ComputerMove(Board i_Board, char i_PlayerSymbol, List<int> i_ArrOfEmptySpotsToPlay)
        {
            if (i_Board.BoardSize == 3)
            {
                findBestMoveForAi(i_Board, i_PlayerSymbol);
            }
            else
            {
                randomMoveOfComputer(i_ArrOfEmptySpotsToPlay);
            }
        }

        private int minMaxAlgo(Board i_Board, bool i_IsMax, char i_PlayerSymbol)
        {
            int bestScore = gettingScoreForMove(i_Board, i_PlayerSymbol);
            int sizeOfBoard = i_Board.BoardSize;
            int funcationReturn;

            if ((bestScore == 0) && (i_Board.BoardIsFull() == false))
            {
                if (i_IsMax == true)
                {
                    bestScore = 1000;
                    for (int row = 0; row < sizeOfBoard; row++)
                    {
                        for (int col = 0; col < sizeOfBoard; col++)
                        {
                            if (i_Board.WhatThereIsInThisPostion(row, col) == ' ')
                            {
                                i_Board.UpdateBoard(row, col, Symbol);
                                funcationReturn = minMaxAlgo(i_Board, !i_IsMax, i_PlayerSymbol);
                                bestScore = Math.Min(bestScore, funcationReturn);
                                i_Board.UpdateBoard(row, col, ' ');
                            }
                        }
                    }
                }
                else
                {
                    bestScore = -1000;
                    for (int row = 0; row < sizeOfBoard; row++)
                    {
                        for (int col = 0; col < sizeOfBoard; col++)
                        {
                            if (i_Board.WhatThereIsInThisPostion(row, col) == ' ')
                            {
                                i_Board.UpdateBoard(row, col, i_PlayerSymbol);
                                funcationReturn = minMaxAlgo(i_Board, !i_IsMax, i_PlayerSymbol);
                                bestScore = Math.Max(bestScore, funcationReturn);
                                i_Board.UpdateBoard(row, col, ' ');
                            }
                        }
                    }
                }
            }

            return bestScore;
        }

        private int gettingScoreForMove(Board i_Board, char i_PlayerSymbol)
        {
            int score = 0;
            int row, col = 0;
            int sizeOfBoard = i_Board.BoardSize;

            for (row = 0; row < sizeOfBoard; row++)
            {
                if (checkIfWeHaveWinnerForAi(Symbol, row, col, i_Board) == true)
                {
                    score = 1;
                    break;
                }

                if (checkIfWeHaveWinnerForAi(i_PlayerSymbol, row, col, i_Board) == true)
                {
                    score = -1;
                    break;
                }
            }

            if (score == 0)
            {
                row = 0;
                for (col = 0; col < sizeOfBoard; col++)
                {
                    if (checkIfWeHaveWinnerForAi(Symbol, row, col, i_Board) == true)
                    {
                        score = 1;
                        break;
                    }

                    if (checkIfWeHaveWinnerForAi(i_PlayerSymbol, row, col, i_Board) == true)
                    {
                        score = -1;
                        break;
                    }
                }
            }

            return score;
        }

        private void findBestMoveForAi(Board i_Board, char i_PlayerSymbol)
        {
            int bestValueScore = 1000;
            int sizeOfBoard = i_Board.BoardSize;
            int scoreOfMinMaxAlgo;

            for (int row = 0; row < sizeOfBoard; row++)
            {
                for (int col = 0; col < sizeOfBoard; col++)
                {
                    if (i_Board.WhatThereIsInThisPostion(row, col) == ' ')
                    {
                        i_Board.UpdateBoard(row, col, Symbol);
                        scoreOfMinMaxAlgo = minMaxAlgo(i_Board, false, i_PlayerSymbol);
                        i_Board.UpdateBoard(row, col, ' ');
                        if (scoreOfMinMaxAlgo < bestValueScore)
                        {
                            bestValueScore = scoreOfMinMaxAlgo;
                            m_Row = row;
                            m_Col = col;
                        }
                    }
                }
            }
        }

        private bool checkIfWeHaveWinnerForAi(char i_Symbol, int i_Row, int i_Col, Board i_Board)
        {
            bool thereIsAWinner = i_Board.DoesRowIsFull(i_Symbol, i_Row, i_Col);

            thereIsAWinner = thereIsAWinner || i_Board.DoesColIsFull(i_Symbol, i_Row, i_Col);
            if ((thereIsAWinner == false) && ((i_Row == i_Col) || (i_Row + i_Col + 1 == i_Board.BoardSize)))
            {
                thereIsAWinner = i_Board.DoesDiagonalIsFull(i_Symbol);
            }

            return thereIsAWinner;
        }

        private void randomMoveOfComputer(List<int> i_ArrOfEmptySpotsToPlay)
        {
            int rowAndCol;
            int rowAndColIndex;

            rowAndColIndex = r_RandomNumbersToGenerate.Next(0, i_ArrOfEmptySpotsToPlay.Count - 1);
            rowAndCol = i_ArrOfEmptySpotsToPlay[rowAndColIndex];
            m_Row = (rowAndCol / 10) - 1;
            m_Col = (rowAndCol % 10) - 1;
        }
    }
}