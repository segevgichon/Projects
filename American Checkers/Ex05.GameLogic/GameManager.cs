using System.Collections.Generic;
using System.Linq;

namespace Ex05.GameLogic
{
    public class GameManager
    {
        private eGameMode m_GameMode;
        public eGameMode GameMode
        {
            get { return m_GameMode; }
            set
            {
                m_GameMode = value;
                if (m_GameMode.Equals(eGameMode.Computer))
                {
                    InitPlayer(2, "Computer");
                }
            }
        }
        public BoardCell[] TwoTurnsAgo { get; set; }
        public bool twoTurnsAgoJump { get; set; }
        public BoardCell[] LastTurn { get; set; }
        private Board m_Board;
        private Player[] m_Players;
        private int m_CurrentTurnPlayerIdx = 0;
        private List<BoardCell[]> m_CurrentPlayerMoves;
        public bool m_WasJump { get; set; }

        public GameManager()
        {
            m_Players = new Player[2];
        }
        public void InitBoard(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
            m_Board.InitStartingBoard();
            updateCurrentPlayerMoves();
        }

        public void InitPlayer(int i_PlayerId, string i_PlayerName)
        {
            eBoardOption playerCoin, playerKing;
            if (i_PlayerId == 1)
            {
                playerCoin = eBoardOption.Player1Coin;
                playerKing = eBoardOption.Player1King;
            }
            else
            {
                playerCoin = eBoardOption.Player2Coin;
                playerKing = eBoardOption.Player2King;
            }
            int playerIdx = i_PlayerId - 1;
            m_Players[playerIdx] = new Player(i_PlayerName, playerCoin, playerKing);
        }

        public Board GetBoard()
        {
            return m_Board;
        }

        public Player GetCurrentPlayer()
        {
            return m_Players[m_CurrentTurnPlayerIdx];
        }

        public Player[] GetPlayers()
        {
            return m_Players;
        }

        public int GetCurrentPlayerIdx()
        {
            return m_CurrentTurnPlayerIdx;
        }

        public eGameStatus ExecuteMove(BoardCell[] i_Move)
        {
            bool wasJump = m_Board.ExecuteMoveAndCheckJump(i_Move);
            TwoTurnsAgo = i_Move;
            twoTurnsAgoJump = wasJump;
            eGameStatus resultingStatus = eGameStatus.OnGoing;
            m_WasJump = false;
            if (wasJump)
            {
                Player currentPlayer = GetCurrentPlayer();
                List<BoardCell[]> jumpMoves = currentPlayer.GetCellJumpMoves(i_Move[1], m_Board);
                if (jumpMoves.Count > 0)
                {
                    m_CurrentPlayerMoves = jumpMoves;
                    m_WasJump = true;
                    resultingStatus = getGameStatusAndIncrementPlayer();
                }
            }

            if (!m_WasJump)
            {
                resultingStatus = getGameStatusAndIncrementPlayer();
            }

            return resultingStatus;
        }

        public bool IsValidMove(BoardCell[] i_Move)
        {
            bool isValid = false;
            foreach (BoardCell[] move in m_CurrentPlayerMoves)
            {
                if (move[0].Equals(i_Move[0]) && move[1].Equals(i_Move[1]))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public bool IsTurnLogicValid(BoardCell i_Src, BoardCell i_Dest)
        {
            return m_Board.IsTurnLogicValid(i_Src, i_Dest);
        }

        public void ResetGame()
        {
            m_WasJump = false;
            m_CurrentTurnPlayerIdx = 0;
            m_Board.ResetScores();
            InitBoard(m_Board.BoardSize);
        }

        private void updateCurrentPlayerMoves()
        {
            if (m_WasJump)
            {
                return;
            }
            Player currentPlayer = GetCurrentPlayer();
            m_CurrentPlayerMoves = currentPlayer.GetAllValidMoves(m_Board);
        }

        private eGameStatus getGameStatusAndIncrementPlayer(bool runCPULogic = true)
        {
            eGameStatus gameStatus = eGameStatus.OnGoing;
            int nextPlayerIdx = (m_CurrentTurnPlayerIdx + 1) % 2;
            Player currentPlayer = GetCurrentPlayer();
            Player nextPlayer = m_Players[nextPlayerIdx];
            List<BoardCell[]> currentPlayerMoves = currentPlayer.GetAllValidMoves(m_Board);
            List<BoardCell[]> nextPlayerMoves = nextPlayer.GetAllValidMoves(m_Board);

            if (m_Board.GetPlayerScores().Contains(0) || (currentPlayerMoves.Count > 0 && nextPlayerMoves.Count == 0))
            {
                gameStatus = eGameStatus.Win;
            }
            else if (currentPlayerMoves.Count == 0 && nextPlayerMoves.Count == 0)
            {
                gameStatus = eGameStatus.Draw;
            }
            else if (m_GameMode.Equals(eGameMode.Computer) && runCPULogic && !m_WasJump)
            {
                gameStatus = runCPUTurn(nextPlayerMoves);
            }
            else if (m_GameMode.Equals(eGameMode.Human) && !m_WasJump)
            {
                m_CurrentTurnPlayerIdx = nextPlayerIdx;
                m_CurrentPlayerMoves = nextPlayerMoves;
            }

            return gameStatus;
        }

        private eGameStatus runCPUTurn(List<BoardCell[]> i_LegalMoves)
        {
            Player cpuPlayer = m_Players[1];
            BoardCell[] cpuMove = cpuPlayer.GetCPUMove(i_LegalMoves);
            bool wasJump = m_Board.ExecuteMoveAndCheckJump(cpuMove);
            eGameStatus gameStatus = getGameStatusAndIncrementPlayer(false);

            while (wasJump && gameStatus.Equals(eGameStatus.OnGoing))
            {
                twoTurnsAgoJump = true;
                List<BoardCell[]> jumpMoves = cpuPlayer.GetCellJumpMoves(cpuMove[1], m_Board);
                if (jumpMoves.Count == 0)
                {
                    break;
                }
                //issue with knowing cpu jump moves
                cpuMove = cpuPlayer.GetCPUMove(jumpMoves);
                wasJump = m_Board.ExecuteMoveAndCheckJump(cpuMove);
                gameStatus = getGameStatusAndIncrementPlayer(false);
            }

            LastTurn = cpuMove;

            if (gameStatus.Equals(eGameStatus.Win))
            {
                gameStatus = eGameStatus.CPUWin;
            }
            else
            {
                updateCurrentPlayerMoves();
            }

            return gameStatus;
        }

        public static bool ValidateUserName(string i_UserName)
        {
            return !(i_UserName.Contains(" ") || i_UserName.Length > 10);
        }

        public static bool ValidateBoardSize(string i_BoardSize)
        {
            return i_BoardSize == "6" || i_BoardSize == "8" || i_BoardSize == "10";
        }

        public static bool ValidateGameMode(string i_GameMode)
        {
            return i_GameMode == "C" || i_GameMode == "H";
        }
    }
}
