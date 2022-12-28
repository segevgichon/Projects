using System.Windows.Forms;

namespace Ex05.GameUI
{
    partial class GameSettingsForm
    {
        private Label m_PlayersLabel;
        private Label m_PlayerOneLabel;
        private CheckBox m_PlayerTwoCheckBox;
        private TextBox m_PlayerOneNameTextBox;
        private TextBox m_PlayerTwoNameTextBox;
        private Label m_BorderSizeLabel;
        private Label m_RowsLabel;
        private RadioButton m_SixSix;
        private RadioButton m_EightEight;
        private RadioButton m_TenTen;
        private int m_BoardSize = 6;
        private Label m_ColumnsLabel;
        private Button m_StartButton;

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public bool IsNotAgainstComputer
        {
            get { return m_PlayerTwoCheckBox.Checked; }
        }

        public string PlayerOneName
        {
            get { return m_PlayerOneNameTextBox.Text; }
        }

        public string PlayerTwoName
        {
            get { return m_PlayerTwoNameTextBox.Text; }
        }

        private void initializeComponent()
        {
            this.m_PlayersLabel = new Label();
            this.m_PlayerOneLabel = new Label();
            this.m_PlayerTwoCheckBox = new CheckBox();
            this.m_PlayerOneNameTextBox = new TextBox();
            this.m_PlayerTwoNameTextBox = new TextBox();
            this.m_BorderSizeLabel = new Label();
            this.m_RowsLabel = new Label();
            this.m_SixSix = new RadioButton();
            this.m_EightEight = new RadioButton();
            this.m_TenTen = new RadioButton();
            this.m_ColumnsLabel = new Label();
            this.m_StartButton = new Button();
            this.SuspendLayout();
            initializePlayersLabels();
            initializePlayersTextBox();
            initializeBoardSizeLabels();
            initializeBoardRadioButtons();
            initializeStartButton();
            initializeSettingsWindow();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void initializePlayersLabels()
        {
            /*Players header*/
            this.m_PlayersLabel.AutoSize = true;
            this.m_PlayersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_PlayersLabel.Location = new System.Drawing.Point(34, 120);
            this.m_PlayersLabel.Name = "PlayersLabel";
            this.m_PlayersLabel.Size = new System.Drawing.Size(99, 29);
            this.m_PlayersLabel.TabIndex = 0;
            this.m_PlayersLabel.Text = "Players:";
            this.m_PlayersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            /*Player 1*/
            this.m_PlayerOneLabel.AutoSize = true;
            this.m_PlayerOneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_PlayerOneLabel.Location = new System.Drawing.Point(40, 163);
            this.m_PlayerOneLabel.Name = "PlayerOneLabel";
            this.m_PlayerOneLabel.Size = new System.Drawing.Size(89, 25);
            this.m_PlayerOneLabel.TabIndex = 2;
            this.m_PlayerOneLabel.Text = "Player 1:";
            this.m_PlayerOneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            /*Player 2*/
            this.m_PlayerTwoCheckBox.AutoSize = true;
            this.m_PlayerTwoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_PlayerTwoCheckBox.Location = new System.Drawing.Point(45, 204);
            this.m_PlayerTwoCheckBox.Name = "PlayerTwoCheckBox";
            this.m_PlayerTwoCheckBox.Size = new System.Drawing.Size(115, 29);
            this.m_PlayerTwoCheckBox.TabIndex = 4;
            this.m_PlayerTwoCheckBox.Text = "Player 2:";
            this.m_PlayerTwoCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_PlayerTwoCheckBox.UseVisualStyleBackColor = true;
            this.m_PlayerTwoCheckBox.CheckedChanged += new System.EventHandler(this.PlayerTwoName_OnChecked);
        }

        private void initializePlayersTextBox()
        {
            /*Player 1*/
            this.m_PlayerOneNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_PlayerOneNameTextBox.Location = new System.Drawing.Point(189, 163);
            this.m_PlayerOneNameTextBox.MaxLength = 7;
            this.m_PlayerOneNameTextBox.Name = "PlayerOneNameTextBox";
            this.m_PlayerOneNameTextBox.Size = new System.Drawing.Size(163, 28);
            this.m_PlayerOneNameTextBox.TabIndex = 5;
            this.m_PlayerOneNameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);

            /*Player 2*/
            this.m_PlayerTwoNameTextBox.Enabled = false;
            this.m_PlayerTwoNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_PlayerTwoNameTextBox.Location = new System.Drawing.Point(189, 204);
            this.m_PlayerTwoNameTextBox.MaxLength = 7;
            this.m_PlayerTwoNameTextBox.Name = "PlayerTwoNameTextBox";
            this.m_PlayerTwoNameTextBox.Size = new System.Drawing.Size(163, 28);
            this.m_PlayerTwoNameTextBox.TabIndex = 6;
            this.m_PlayerTwoNameTextBox.Text = "Computer";
            this.m_PlayerTwoNameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
        }

        private void initializeBoardSizeLabels()
        {
            /*Board size header*/
            this.m_BorderSizeLabel.AutoSize = true;
            this.m_BorderSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_BorderSizeLabel.Location = new System.Drawing.Point(34, 21);
            this.m_BorderSizeLabel.Name = "BorderSizeLabel";
            this.m_BorderSizeLabel.Size = new System.Drawing.Size(146, 29);
            this.m_BorderSizeLabel.TabIndex = 7;
            this.m_BorderSizeLabel.Text = "Board Size:";
        }

        private void initializeBoardRadioButtons()
        {
            /*Six by Six*/
            this.m_SixSix.Checked = true;
            this.m_SixSix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_SixSix.Location = new System.Drawing.Point(45, 68);
            this.m_SixSix.Text = "6X6";
            this.m_SixSix.Name = "Six";
            this.m_SixSix.Size = new System.Drawing.Size(100, 28);
            this.m_SixSix.TabIndex = 9;
            this.m_SixSix.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);

            /*Eight by Eight*/
            this.m_EightEight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_EightEight.Location = new System.Drawing.Point(155, 68);
            this.m_EightEight.Text = "8X8";
            this.m_EightEight.Name = "Eight";
            this.m_EightEight.Size = new System.Drawing.Size(100, 28);
            this.m_EightEight.TabIndex = 10;
            this.m_EightEight.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);

            /*Ten by Ten*/
            this.m_TenTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_TenTen.Location = new System.Drawing.Point(265, 68);
            this.m_TenTen.Text = "10X10";
            this.m_TenTen.Name = "Ten";
            this.m_TenTen.Size = new System.Drawing.Size(100, 28);
            this.m_TenTen.TabIndex = 11;
            this.m_TenTen.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
        }

        private void initializeStartButton()
        {
            this.m_StartButton.Enabled = false;
            this.m_StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.m_StartButton.Location = new System.Drawing.Point(240, 260);
            this.m_StartButton.Name = "StartButton";
            this.m_StartButton.Size = new System.Drawing.Size(112, 38);
            this.m_StartButton.TabIndex = 12;
            this.m_StartButton.Text = "Done";
            this.m_StartButton.UseVisualStyleBackColor = true;
            this.m_StartButton.Click += new System.EventHandler(this.Done_Clicked);
        }

        private void initializeSettingsWindow()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(391, 320);
            this.Controls.Add(this.m_StartButton);
            this.Controls.Add(this.m_SixSix);
            this.Controls.Add(this.m_EightEight);
            this.Controls.Add(this.m_TenTen);
            this.Controls.Add(this.m_BorderSizeLabel);
            this.Controls.Add(this.m_PlayerTwoNameTextBox);
            this.Controls.Add(this.m_PlayerOneNameTextBox);
            this.Controls.Add(this.m_PlayerTwoCheckBox);
            this.Controls.Add(this.m_PlayerOneLabel);
            this.Controls.Add(this.m_PlayersLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Game Settings";
            this.TopMost = true;
        }
    }
}