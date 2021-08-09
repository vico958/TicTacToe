namespace TicTacToeLogic
{
    using System;

    public struct Board
    {
        private readonly int r_SizeOfBoard;
        private char[,] m_Board;
        private int m_CountingIfBoardIsFull;

        public Board(int i_SizeOfBoard)
        {
            r_SizeOfBoard = i_SizeOfBoard;
            m_CountingIfBoardIsFull = 0;
            m_Board = new char[i_SizeOfBoard, i_SizeOfBoard];
            ClearBoard();
        }

        public bool DoesColIsFull(char i_Symbol, int i_IndexOfRow, int i_IndexOfCol)
        {
            bool isItFullInCol = true;

            if (InsideBoardLimits(i_IndexOfRow, i_IndexOfCol) == true)
            {
                for (int i = 0; i < r_SizeOfBoard; i++)
                {
                    if (m_Board[i, i_IndexOfCol] != i_Symbol)
                    {
                        isItFullInCol = false;
                    }
                }
            }
            else
            {
                isItFullInCol = false;
            }

            return isItFullInCol;
        }

        public bool DoesRowIsFull(char i_Symbol, int i_IndexOfRow, int i_IndexOfCol)
        {
            bool isItFullInRow = true;

            if (InsideBoardLimits(i_IndexOfRow, i_IndexOfCol) == true)
            {
                for (int i = 0; i < r_SizeOfBoard; i++)
                {
                    if (m_Board[i_IndexOfRow, i] != i_Symbol)
                    {
                        isItFullInRow = false;
                    }
                }
            }
            else
            {
                isItFullInRow = false;
            }

            return isItFullInRow;
        }

        public bool DoesDiagonalIsFull(char i_Symbol)
        {
            bool isItFullInDiagonalRightToLeft = true;
            bool isItFullInDiagonalLeftToRight = true;

            for (int i = 0; i < r_SizeOfBoard; i++)
            {
                if (m_Board[i, i] != i_Symbol)
                {
                    isItFullInDiagonalRightToLeft = false;
                }

                if (m_Board[r_SizeOfBoard - i - 1, i] != i_Symbol)
                {
                    isItFullInDiagonalLeftToRight = false;
                }
            }

            return isItFullInDiagonalRightToLeft || isItFullInDiagonalLeftToRight;
        }

        public char WhatThereIsInThisPostion(int i_Row, int i_Col) // getting the char from Board[row,col]
        {
            char charToReturnFromTheBoard = '1';

            if (InsideBoardLimits(i_Row, i_Col) == true)
            {
                charToReturnFromTheBoard = m_Board[i_Row, i_Col];
            }

            return charToReturnFromTheBoard;
        }

        public void UpdateBoard(int i_Row, int i_Col, char i_Symbol) // updating a char in Board
        {
            if (InsideBoardLimits(i_Row, i_Col) == true)
            {
                if (i_Symbol == ' ')
                {
                    m_CountingIfBoardIsFull--;
                }
                else
                {
                    if (m_Board[i_Row, i_Col] == ' ')
                    {
                        m_CountingIfBoardIsFull++;
                    }
                }

                m_Board[i_Row, i_Col] = i_Symbol;
            }
        }

        public int BoardSize
        {
            get
            {
                return r_SizeOfBoard;
            }
        }

        public bool InsideBoardLimits(int i_Row, int i_Col) // check if a row and col givin is inside the board
        {
            bool insideTheBoard = true;

            if (i_Row >= r_SizeOfBoard || i_Row < 0 || i_Col >= r_SizeOfBoard || i_Col < 0)
            {
                insideTheBoard = false;
            }

            return insideTheBoard;
        }

        public void ClearBoard() // cleaning the board so it will be empty from symbols
        {
            m_CountingIfBoardIsFull = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    m_Board[i, j] = ' ';
                }
            }
        }

        public bool BoardIsFull() // comparing if the couter of times that board has been fill is equal to all size of board
        {
            return m_CountingIfBoardIsFull == Math.Pow(r_SizeOfBoard, 2);
        }
    }
}