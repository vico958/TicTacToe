namespace TicTacToeLogic
{
    using System.Collections.Generic;

    public class Game
    {
        private readonly List<int> r_ArrOfEmptySpotsToPlay;
        private Board m_GameBoard;
        private Player[] m_Player;
        private int m_HowManyPlayers;
        private ComputerPlayer m_ComputerPlayer = null;
        private int m_CurrentPlayerIdTurn;

        public Game(int i_HowManyPlayers, int i_SizeOfBoard)
        {
            CurrentPlayerTurn = 1;
            r_ArrOfEmptySpotsToPlay = new List<int>();
            m_HowManyPlayers = i_HowManyPlayers;
            m_GameBoard = new Board(i_SizeOfBoard);
            initEmptySpot(i_SizeOfBoard);
            initPlayers(i_HowManyPlayers);
        }

        public int CurrentPlayerTurn
        {
            get
            {
                return m_CurrentPlayerIdTurn;
            }

            private set
            {
                m_CurrentPlayerIdTurn = value;
            }
        }

        public bool DoesGameIsTie()
        {
            return m_GameBoard.BoardIsFull();
        }

        public bool WeHaveWinnerAndUpdateScoreIfNeed(char i_Symbol, int i_Row, int i_Col) // checking if a player won
        {
            bool thereIsAWinner = m_GameBoard.DoesRowIsFull(i_Symbol, i_Row, i_Col);

            thereIsAWinner = thereIsAWinner || m_GameBoard.DoesColIsFull(i_Symbol, i_Row, i_Col);
            if ((thereIsAWinner == false) && ((i_Row == i_Col) || (i_Row + i_Col + 1 == m_GameBoard.BoardSize)))
            {
                thereIsAWinner = m_GameBoard.DoesDiagonalIsFull(i_Symbol);
            }

            if (thereIsAWinner == true)
            {
                if (m_HowManyPlayers == 2)
                {
                    if (i_Symbol == m_Player[0].Symbol)
                    {
                        m_Player[1].Score++;
                    }
                    else
                    {
                        m_Player[0].Score++;
                    }
                }
                else
                {
                    if (i_Symbol == m_Player[0].Symbol)
                    {
                        m_ComputerPlayer.Score++;
                    }
                    else
                    {
                        m_Player[0].Score++;
                    }
                }
            }

            return thereIsAWinner;
        }

        public bool IsSymbolUpdated(int i_Row, int i_Col, char i_Symbol) // checking and updating the board
        {
            bool isUpdated = true;

            if (m_GameBoard.WhatThereIsInThisPostion(i_Row, i_Col) != ' ')
            {
                isUpdated = false;
            }
            else
            {
                m_GameBoard.UpdateBoard(i_Row, i_Col, i_Symbol);
                r_ArrOfEmptySpotsToPlay.Remove(((i_Row + 1) * 10) + i_Col + 1); // Removing the option that the user put so the computer will not try to generate this row and col
            }

            return isUpdated;
        }

        public void StartRemach()
        {
            CurrentPlayerTurn = 1;
            r_ArrOfEmptySpotsToPlay.Clear();
            initEmptySpot(m_GameBoard.BoardSize);
            m_GameBoard.ClearBoard();
        }

        public ComputerPlayer PcPlayer
        {
            get
            {
                return m_ComputerPlayer;
            }
        }

        public Board GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        public Player[] Players
        {
            get
            {
                return m_Player;
            }
        }

        public bool StartTurnAndCheckIfMoveMade(int i_Row, int i_Col)
        {
            bool isMoveMade;

            if (m_HowManyPlayers == 1)
            {
                isMoveMade = playerVsComputerAndIsSymbolInsert(i_Row, i_Col);
            }
            else
            {
                isMoveMade = playerVsAnotherPlayerAndIsSymbolInsert(i_Row, i_Col);
            }

            return isMoveMade;
        }

        private void initEmptySpot(int i_SizeOfBoard)
        {
            for (int row = 1; row <= i_SizeOfBoard; row++)
            {
                for (int col = 1; col <= i_SizeOfBoard; col++)
                {
                    r_ArrOfEmptySpotsToPlay.Add((row * 10) + col);
                }
            }
        }

        private void initPlayers(int i_HowManyPlayers)
        {
            int playerId = 1;

            m_Player = new Player[i_HowManyPlayers];
            m_Player[0] = new Player(playerId, 'X');
            if (m_HowManyPlayers == 1)
            {
                m_ComputerPlayer = new ComputerPlayer('O');
            }
            else
            {
                playerId = 2;
                m_Player[1] = new Player(playerId, 'O');
            }
        }

        private bool playerVsAnotherPlayerAndIsSymbolInsert(int i_Row, int i_Col)
        {
            bool isSymbolInsertInBoard;

            isSymbolInsertInBoard = IsSymbolUpdated(i_Row, i_Col, m_Player[CurrentPlayerTurn - 1].Symbol);
            if ((CurrentPlayerTurn == m_Player[0].PlayerId) && (isSymbolInsertInBoard == true))
            {
                CurrentPlayerTurn = m_Player[1].PlayerId;
            }
            else
            {
                if (isSymbolInsertInBoard == true)
                {
                    CurrentPlayerTurn = m_Player[0].PlayerId;
                }
            }

            return isSymbolInsertInBoard;
        }

        private bool playerVsComputerAndIsSymbolInsert(int i_Row, int i_Col)
        {
            bool isSymbolInsertInBoard;

            if (CurrentPlayerTurn == m_ComputerPlayer.PlayerId)
            {
                placingComputerSymbol();
                isSymbolInsertInBoard = true;
                CurrentPlayerTurn = m_Player[0].PlayerId;
            }
            else
            {
                isSymbolInsertInBoard = IsSymbolUpdated(i_Row, i_Col, m_Player[0].Symbol);
                if (isSymbolInsertInBoard == true)
                {
                    CurrentPlayerTurn = m_ComputerPlayer.PlayerId;
                }
            }

            return isSymbolInsertInBoard;
        }

        private void placingComputerSymbol()
        {
            bool updateBoardSucseesed = false;

            while (updateBoardSucseesed == false)
            {
                m_ComputerPlayer.ComputerMove(m_GameBoard, m_Player[0].Symbol, r_ArrOfEmptySpotsToPlay);
                updateBoardSucseesed = IsSymbolUpdated(m_ComputerPlayer.Row, m_ComputerPlayer.Col, m_ComputerPlayer.Symbol);
            }
        }
    }
}