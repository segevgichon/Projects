using System;
using System.Collections.Generic;

namespace Ex05.GameLogic
{
    public class Board
    {
        public int BoardSize { get; set; }
        // $G$ DSN-999 (-3) This Array should be readonly.
        private readonly eBoardOption[,] m_BoardMatrix;
        private int[] m_PlayerScores;

        public Board(int i_BoardSize)
        {
            BoardSize = i_BoardSize;
            m_BoardMatrix = new eBoardOption[BoardSize, BoardSize];
            m_PlayerScores = new int[]{0, 0};
        }

        public void InitStartingBoard()
        {
            int numOfCoinRows = BoardSize / 2 - 1;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i < numOfCoinRows && ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0)))
                    {
                        m_BoardMatrix[i, j] = eBoardOption.Player2Coin;
                        m_PlayerScores[0]++;
                    }
                    else if (i > BoardSize - 1 - numOfCoinRows && ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0)))
                    {
                        m_BoardMatrix[i, j] = eBoardOption.Player1Coin;
                        m_PlayerScores[1]++;
                    }
                    else if ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0))
                    {
                        m_BoardMatrix[i, j] = eBoardOption.Empty;
                    }
                    else
                    {
                        m_BoardMatrix[i, j] = eBoardOption.NotLegal;
                    }
                }
            }
        }

        public void ResetScores()
        {
            m_PlayerScores = new int[] { 0, 0 };
        }

        public bool IsTurnLogicValid(BoardCell i_Src, BoardCell i_Dest)
        {
            bool isValid = true;
            if (i_Src.RowIdx < 0 || i_Src.RowIdx >= BoardSize || i_Src.ColIdx < 0 || i_Src.ColIdx >= BoardSize)
            {
                isValid = false;
            }
            else if (i_Dest.RowIdx < 0 || i_Dest.RowIdx >= BoardSize || i_Dest.ColIdx < 0 ||
                i_Dest.ColIdx >= BoardSize)
            {
                isValid = false;
            }

            return isValid;
        }

        public eBoardOption[,] GetBoardMatrix()
        {
            return m_BoardMatrix;
        }

        public int[] GetPlayerScores()
        {
            return m_PlayerScores;
        }

        public bool ExecuteMoveAndCheckJump(BoardCell[] i_Move)
        {
            // Move coin in source cell to destination cell
            eBoardOption currentCoin = m_BoardMatrix[i_Move[0].RowIdx, i_Move[0].ColIdx];
            m_BoardMatrix[i_Move[1].RowIdx, i_Move[1].ColIdx] = currentCoin;
            m_BoardMatrix[i_Move[0].RowIdx, i_Move[0].ColIdx] = eBoardOption.Empty;

            handleKingTransform(i_Move[1]);
            return checkAndHandleJump(i_Move);
        }

        public List<BoardCell[]> GetAllPlayerValidMoves(eBoardOption i_PlayerCoin, eBoardOption i_PlayerKing)
        {
            List<BoardCell[]> validMoves;
            List<BoardCell[]> regularMoves = new List<BoardCell[]>();
            List<BoardCell[]> jumpMoves = new List<BoardCell[]>();
            eBoardOption currentCell;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    currentCell = m_BoardMatrix[i, j];
                    if (currentCell.Equals(i_PlayerCoin) || currentCell.Equals(i_PlayerKing))
                    {
                        addCellMovesToLists(currentCell, i, j, regularMoves, jumpMoves);
                    }
                }
            }
            
            if (jumpMoves.Count > 0)
            {
                validMoves = jumpMoves;
            }
            else
            {
                validMoves = regularMoves;
            }

            return validMoves;
        }

        public List<BoardCell[]> GetCellJumpMoves(BoardCell i_Cell, eBoardOption i_PlayerCoin, eBoardOption i_PlayerKing)
        {
            List<BoardCell[]> regularMoves = new List<BoardCell[]>();
            List<BoardCell[]> jumpMoves = new List<BoardCell[]>();
            eBoardOption cellCoin = m_BoardMatrix[i_Cell.RowIdx, i_Cell.ColIdx];
            addCellMovesToLists(cellCoin, i_Cell.RowIdx, i_Cell.ColIdx, regularMoves, jumpMoves);

            return jumpMoves;
        }

        private void addCellMovesToLists(eBoardOption i_Coin, int i_RowIdx, int i_ColIdx,
            List<BoardCell[]> i_RegMoves, List<BoardCell[]> I_JumpMoves)
        {
            BoardCell srcCell = new BoardCell(i_RowIdx, i_ColIdx);
            BoardCell dstCell;
            eBoardOption dstCoin, opponentCoin, opponentKing, jumpDstCoin;

            if (i_Coin.Equals(eBoardOption.Player1Coin) || i_Coin.Equals(eBoardOption.Player1King))
            {
                opponentCoin = eBoardOption.Player2Coin;
                opponentKing = eBoardOption.Player2King;
            }
            else
            {
                opponentCoin = eBoardOption.Player1Coin;
                opponentKing = eBoardOption.Player1King;
            }

            // Lower left move and jump
            if (i_RowIdx + 1 < BoardSize && i_ColIdx - 1 >= 0)
            {
                dstCoin = m_BoardMatrix[i_RowIdx + 1, i_ColIdx - 1];
                if (dstCoin.Equals(eBoardOption.Empty) && !i_Coin.Equals(eBoardOption.Player1Coin))
                {
                    // Lower left move
                    dstCell = new BoardCell(i_RowIdx + 1, i_ColIdx - 1);
                    i_RegMoves.Add(new BoardCell[]{ srcCell , dstCell });
                }

                if (i_RowIdx + 2 < BoardSize && i_ColIdx - 2 >= 0)
                {
                    jumpDstCoin = m_BoardMatrix[i_RowIdx + 2, i_ColIdx - 2];
                    if ((dstCoin.Equals(opponentCoin) || dstCoin.Equals(opponentKing)) &&
                        !i_Coin.Equals(eBoardOption.Player1Coin) && jumpDstCoin.Equals(eBoardOption.Empty))
                    {
                        // Lower left jump
                        dstCell = new BoardCell(i_RowIdx + 2, i_ColIdx - 2);
                        I_JumpMoves.Add(new BoardCell[] { srcCell, dstCell });
                    }
                }
            }

            // Lower right move and jump
            if (i_RowIdx + 1 < BoardSize && i_ColIdx + 1 < BoardSize)
            {
                dstCoin = m_BoardMatrix[i_RowIdx + 1, i_ColIdx + 1];
                if (dstCoin.Equals(eBoardOption.Empty) && !i_Coin.Equals(eBoardOption.Player1Coin))
                {
                    // Lower right move
                    dstCell = new BoardCell(i_RowIdx + 1, i_ColIdx + 1);
                    i_RegMoves.Add(new BoardCell[] { srcCell, dstCell });
                }

                if (i_RowIdx + 2 < BoardSize && i_ColIdx + 2 < BoardSize)
                {
                    jumpDstCoin = m_BoardMatrix[i_RowIdx + 2, i_ColIdx + 2];
                    if ((dstCoin.Equals(opponentCoin) || dstCoin.Equals(opponentKing)) &&
                        !i_Coin.Equals(eBoardOption.Player1Coin) && jumpDstCoin.Equals(eBoardOption.Empty))
                    {
                        // Lower right jump
                        dstCell = new BoardCell(i_RowIdx + 2, i_ColIdx + 2);
                        I_JumpMoves.Add(new BoardCell[] { srcCell, dstCell });
                    }
                }
            }

            // Upper left move and jump
            if (i_RowIdx - 1 >= 0 && i_ColIdx - 1 >= 0)
            {
                dstCoin = m_BoardMatrix[i_RowIdx - 1, i_ColIdx - 1];
                if (dstCoin.Equals(eBoardOption.Empty) && !i_Coin.Equals(eBoardOption.Player2Coin))
                {
                    // Upper left move
                    dstCell = new BoardCell(i_RowIdx - 1, i_ColIdx - 1);
                    i_RegMoves.Add(new BoardCell[] { srcCell, dstCell });
                }

                if (i_RowIdx - 2 >= 0 && i_ColIdx - 2 >= 0)
                {
                    jumpDstCoin = m_BoardMatrix[i_RowIdx - 2, i_ColIdx - 2];
                    if ((dstCoin.Equals(opponentCoin) || dstCoin.Equals(opponentKing)) &&
                        !i_Coin.Equals(eBoardOption.Player2Coin) && jumpDstCoin.Equals(eBoardOption.Empty))
                    {
                        // Upper left jump
                        dstCell = new BoardCell(i_RowIdx - 2, i_ColIdx - 2);
                        I_JumpMoves.Add(new BoardCell[] { srcCell, dstCell });
                    }
                }
            }

            // Upper right move and jump
            if (i_RowIdx - 1 >= 0 && i_ColIdx + 1 < BoardSize)
            {
                dstCoin = m_BoardMatrix[i_RowIdx - 1, i_ColIdx + 1];
                if (dstCoin.Equals(eBoardOption.Empty) && !i_Coin.Equals(eBoardOption.Player2Coin))
                {
                    // Upper right move
                    dstCell = new BoardCell(i_RowIdx - 1, i_ColIdx + 1);
                    i_RegMoves.Add(new BoardCell[] { srcCell, dstCell });
                }

                if (i_RowIdx - 2 >= 0 && i_ColIdx + 2 < BoardSize)
                {
                    jumpDstCoin = m_BoardMatrix[i_RowIdx - 2, i_ColIdx + 2];
                    if ((dstCoin.Equals(opponentCoin) || dstCoin.Equals(opponentKing)) &&
                        !i_Coin.Equals(eBoardOption.Player2Coin) && jumpDstCoin.Equals(eBoardOption.Empty))
                    {
                        // Upper right jump
                        dstCell = new BoardCell(i_RowIdx - 2, i_ColIdx + 2);
                        I_JumpMoves.Add(new BoardCell[] { srcCell, dstCell });
                    }
                }
            }
        }

        private void handleKingTransform(BoardCell i_Dest)
        {
            eBoardOption currentCoin = m_BoardMatrix[i_Dest.RowIdx, i_Dest.ColIdx];
            if (currentCoin.Equals(eBoardOption.Player1Coin) && i_Dest.RowIdx == 0)
            {
                m_BoardMatrix[i_Dest.RowIdx, i_Dest.ColIdx] = eBoardOption.Player1King;
                m_PlayerScores[0] += 3;
            }
            else if (currentCoin.Equals(eBoardOption.Player2Coin) && i_Dest.RowIdx == BoardSize - 1)
            {
                m_BoardMatrix[i_Dest.RowIdx, i_Dest.ColIdx] = eBoardOption.Player2King;
                m_PlayerScores[1] += 3;
            }
        }

        private bool checkAndHandleJump(BoardCell[] i_Move)
        {
            bool isJump;
            if (Math.Abs(i_Move[0].ColIdx - i_Move[1].ColIdx) < 2)
            {
                isJump = false;
            }
            else
            {
                isJump = true;
                int rowIdx = i_Move[0].RowIdx - 1;
                int colIdx = i_Move[0].ColIdx - 1;

                if (i_Move[0].RowIdx < i_Move[1].RowIdx)
                {
                    rowIdx = i_Move[0].RowIdx + 1;
                }

                if (i_Move[0].ColIdx < i_Move[1].ColIdx)
                {
                    colIdx = i_Move[0].ColIdx + 1;
                }

                // Update scores
                eBoardOption currentCoin = m_BoardMatrix[rowIdx, colIdx];
                if (currentCoin.Equals(eBoardOption.Player1Coin))
                {
                    m_PlayerScores[0]--;
                }
                else if (currentCoin.Equals(eBoardOption.Player1King))
                {
                    m_PlayerScores[0] -= 4;
                }
                else if (currentCoin.Equals(eBoardOption.Player2Coin))
                {
                    m_PlayerScores[1]--;
                }
                else
                {
                    m_PlayerScores[1] -= 4;
                }

                m_BoardMatrix[rowIdx, colIdx] = eBoardOption.Empty;
            }

            return isJump;
        }
    }
}
