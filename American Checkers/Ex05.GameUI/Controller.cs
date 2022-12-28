using Ex05.GameLogic;
using System.Drawing;
using System.Text;
using System;

namespace Ex05.GameUI
{
    class Controller
    {
        private GameManager m_game;
        private GameForm m_gameForm;
        private bool m_firstGame = true;
        private int[] m_scores;

        public Controller()
        {
            m_game = new GameManager();
            m_scores = new int[] { 0, 0 };
            m_gameForm = new GameForm();
            setListeners();
        }

        public void Run()
        {
            m_game.InitPlayer(1, m_gameForm.SettingsForm.PlayerOneName);
            if (m_gameForm.SettingsForm.IsNotAgainstComputer)
            {
                m_game.GameMode = eGameMode.Human;
                m_game.InitPlayer(2, m_gameForm.SettingsForm.PlayerTwoName);
            }
            else
            {
                m_game.GameMode = eGameMode.Computer;
            }
            m_game.InitBoard(m_gameForm.SettingsForm.BoardSize);
            m_gameForm.SetPlayerText(m_gameForm.SettingsForm.PlayerOneName, m_scores[0], 0);
            m_gameForm.SetPlayerText(m_gameForm.SettingsForm.PlayerTwoName, m_scores[1], 1);
            InitializePiecesOnForm();
            if(m_firstGame == true)
            {
                m_firstGame = false;
                m_gameForm.ShowDialog();
            }
        }

        private void InitializePiecesOnForm()
        {
            for (int row = 0; row < m_gameForm.SettingsForm.BoardSize; row++)
            {
                for (int col = 0; col < m_gameForm.SettingsForm.BoardSize; col++)
                {
                    if(m_game.GetBoard().GetBoardMatrix()[row, col] == eBoardOption.Player1Coin)
                    {
                        m_gameForm.CheckersLocal[col, row].Text = "X";
                    }
                    else if(m_game.GetBoard().GetBoardMatrix()[row, col] == eBoardOption.Player2Coin)
                    {
                        m_gameForm.CheckersLocal[col, row].Text = "O";
                    }
                    else if(m_game.GetBoard().GetBoardMatrix()[row, col] == eBoardOption.Player1King)
                    {
                        m_gameForm.CheckersLocal[col, row].Text = "Q";
                    }
                    else if (m_game.GetBoard().GetBoardMatrix()[row, col] == eBoardOption.Player2King)
                    {
                        m_gameForm.CheckersLocal[col, row].Text = "Z";
                    }
                    else
                    {
                        m_gameForm.CheckersLocal[col, row].Text = "";
                    }
                }
            }
        }

        private void GameForm_ExecuteMove(Point i_src, Point i_dest)
        {
            eGameStatus gameStatus;
            BoardCell src = new BoardCell(i_src.Y, i_src.X);
            BoardCell dest = new BoardCell(i_dest.Y, i_dest.X);
            BoardCell[] move = new BoardCell[] { src, dest };
            string name;
            StringBuilder message = new StringBuilder();
            if (m_game.IsValidMove(move))
            {
                gameStatus = m_game.ExecuteMove(move);
                InitializePiecesOnForm();
                if(gameStatus == eGameStatus.Win)
                {
                    if (m_game.GetCurrentPlayer().PlayerName.Equals(m_gameForm.SettingsForm.PlayerOneName))
                    {
                        name = m_game.GetPlayers()[0].PlayerName;
                        message = message.AppendFormat("{0} Won!", name);
                        UpdateScoresOnForm(0);
                    }
                    else
                    {
                        name = m_game.GetPlayers()[1].PlayerName;
                        message = message.AppendFormat("{0} Won!", name);
                        UpdateScoresOnForm(1);
                    }             
                    m_gameForm.ShowEndGameMessage(message.ToString());
                }
                else if (gameStatus == eGameStatus.CPUWin)
                {
                    name = m_game.GetPlayers()[1].PlayerName;
                    message = message.AppendFormat("{0} Won!", name);
                    UpdateScoresOnForm(1);
                    m_gameForm.ShowEndGameMessage(message.ToString());
                }
                else if(gameStatus == eGameStatus.Draw)
                {
                    message = message.AppendFormat("Tie!");
                    m_gameForm.ShowEndGameMessage(message.ToString());
                }
            }
            else
            {
                m_gameForm.InvalidMove();
            }
        }

        private void UpdateScoresOnForm(int i_winnerIndex)
        {
            int[] currentGameScores = m_game.GetBoard().GetPlayerScores();
            m_scores[i_winnerIndex] += currentGameScores[i_winnerIndex] - currentGameScores[Math.Abs(i_winnerIndex -1)];
        }

        private bool GameForm_FirstSquareClicked(Point i_point)
        {
            return m_game.GetBoard().GetBoardMatrix()[i_point.Y, i_point.X] != eBoardOption.Empty;
        }

        private void GameForm_RestartGame()
        {
            this.Run();
        }

        private void setListeners()
        {
            m_gameForm.ExecuteMove += GameForm_ExecuteMove;
            m_gameForm.FirstSquareCliked += GameForm_FirstSquareClicked;
            m_gameForm.RestartGame += GameForm_RestartGame;
        }
    }
}