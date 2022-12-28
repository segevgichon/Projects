using System;
using System.Windows.Forms;
using System.Drawing;
using Ex05.GameLogic;

namespace Ex05.GameUI
{
    partial class GameForm
    {
        private Label m_playerOneLabel;
        private Label m_playerTwoLabel;
        private int m_boardSize;
        private Button[,] m_checkersLocal;
        private bool m_destClick = false;
        private Point m_curSqaurePoint;
        private Point m_destSquarePoint;
        private Button m_blueButton;

        public event Func<Point, bool> FirstSquareCliked;

        public event Action<Point, Point> ExecuteMove;

        public event Action RestartGame;

        private void InitializeComponent()
        {
            m_SettingsForm.ShowDialog();
            this.m_playerOneLabel = new Label();
            this.m_playerTwoLabel = new Label();
            this.m_boardSize = m_SettingsForm.BoardSize;
            this.m_checkersLocal = new Button[m_boardSize, m_boardSize];
            InitializePlayersLabels();
            InitializeCheckerGrid();
            InitializeGameWindow();
        }

        private void InitializePlayersLabels()
        {     
            /*Player 1*/
            this.m_playerOneLabel.Anchor = AnchorStyles.Top;
            this.m_playerOneLabel.AutoSize = true;
            this.m_playerOneLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            this.m_playerOneLabel.Name = "PlayerOneLabel";
            this.m_playerOneLabel.Size = new Size(0, 25);
            
            /*Player 2*/
            this.m_playerTwoLabel.Anchor = AnchorStyles.Top;
            this.m_playerTwoLabel.AutoSize = true;
            this.m_playerTwoLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(177)));
            this.m_playerTwoLabel.Name = "PlayerTwoLabel";
            this.m_playerTwoLabel.Size = new Size(0, 25);
        }

        public void SetPlayerText(string i_playerName, int i_playerScore, int i_playerNum)
        {
            if (i_playerNum == 0)
            {
                this.m_playerOneLabel.Text = string.Format("{0} : {1}", i_playerName, i_playerScore);
            }
            else
            {
                this.m_playerTwoLabel.Text = string.Format("{0} : {1}", i_playerName, i_playerScore);
            }
        }

        private void InitializeCheckerGrid()
        {
            Button boardSquare;
            for (int row = 0; row < m_boardSize; row++)
            {
                for (int col = 0; col < m_boardSize; col++)
                {
                    boardSquare = new Button();
                    m_checkersLocal[row, col] = boardSquare;
                    boardSquare.Height = 50;
                    boardSquare.Width = 50;
                    boardSquare.Location = new Point( (row * 50) + 30, (col * 50) + 50);
                    if (row % 2 ==  col % 2)
                    {
                        boardSquare.Enabled = false;
                        boardSquare.BackColor = Color.Gray;                       
                    }
                    else
                    {
                        boardSquare.BackColor = Color.White;
                        boardSquare.Tag = new Point(row, col);
                        boardSquare.Click += new EventHandler(SquareClicked);
                    }
                    this.Controls.Add(boardSquare);
                }
            } 
        }

        private void SquareClicked(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            Point checkerLocal = (Point)senderButton.Tag;
            if (!m_destClick)
            {
                if (FirstSquareCliked != null)
                {
                    if (FirstSquareCliked.Invoke(checkerLocal))
                    {
                        senderButton.BackColor = Color.LightBlue;
                        m_curSqaurePoint = checkerLocal;
                        m_blueButton = (Button)sender;
                        m_destClick = true;
                    }
                }
            }
            else
            {
                m_blueButton.BackColor = Color.White;
                if (sender != m_blueButton)
                {
                    m_destSquarePoint = checkerLocal;
                    if (ExecuteMove != null)
                    {
                        ExecuteMove.Invoke(m_curSqaurePoint, m_destSquarePoint);
                    }
                }
                m_destClick = false;
            }
        }

        public void DeleteCheckerPiece(BoardCell src)
        {
            m_checkersLocal[src.ColIdx, src.RowIdx].Text = "";
        }

        public void MoveCheckerPiece(Point i_src, Point i_dest)
        {
            m_checkersLocal[i_dest.X, i_dest.Y].Text = m_checkersLocal[i_src.X, i_src.Y].Text;
            m_checkersLocal[i_src.X, i_src.Y].Text = "";
        }

        private void InitializeGameWindow()
        {
            this.ClientSize = new Size(50 + (m_boardSize) * 50, 100 + (m_boardSize * 50));
            this.m_playerOneLabel.Location = new Point((ClientSize.Width/2) - 90, 25);
            this.m_playerTwoLabel.Location = new Point((ClientSize.Width/2) + 25, 25);
            this.Controls.Add(m_playerOneLabel);
            this.Controls.Add(m_playerTwoLabel);
            this.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "American Checkers";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        public void InvalidMove()
        {
            MessageBox.Show("Invalid Move");
        }

        public void ShowEndGameMessage(string i_Message)
        {
            DialogResult result;
            result = MessageBox.Show(i_Message, "Another round?", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                RestartGameClicked();
            }
        }

        private void RestartGameClicked()
        {
            if (RestartGame != null)
            {
                RestartGame.Invoke();
            }
        }
        public Button[,] CheckersLocal
        {
            get { return m_checkersLocal; }
        }
    }
}