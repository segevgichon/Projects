using System;
using System.Windows.Forms;
using Ex05.GameLogic;

namespace Ex05.GameUI
{
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            initializeComponent();
        }
        private void PlayerTwoName_OnChecked(object sender, EventArgs e)
        {
            m_PlayerTwoNameTextBox.Enabled = !m_PlayerTwoNameTextBox.Enabled;
            m_PlayerTwoNameTextBox.Text = m_PlayerTwoNameTextBox.Enabled == true ? "" : "Computer";
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButtonChecked = sender as RadioButton;

            if (radioButtonChecked == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }
            if (radioButtonChecked.Checked)
            {
                switch (radioButtonChecked.Name)
                {
                    case "Six":
                        m_BoardSize = 6;
                        break;
                    case "Eight":
                        m_BoardSize = 8;
                        break;
                    case "Ten":
                        m_BoardSize = 10;
                        break;
                    default:
                        break;
                }
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            bool isPlayerOneValid = m_PlayerOneNameTextBox.Text.Trim() != string.Empty;
            bool isPlayerTwoValid = !m_PlayerTwoCheckBox.Checked ||
                (m_PlayerTwoCheckBox.Checked && m_PlayerTwoNameTextBox.Text.Trim() != string.Empty
                && m_PlayerOneNameTextBox.Text.Trim() != m_PlayerTwoNameTextBox.Text.Trim());
            m_StartButton.Enabled = isPlayerOneValid && isPlayerTwoValid;
        }

        private void Done_Clicked(object sender, EventArgs e)
        {
            if (!GameManager.ValidateUserName(this.PlayerOneName))
            {
                MessageBox.Show("Invalid name for player 1 - Name must have less than 10 characters and no spaces.");
            }
            else if (this.IsNotAgainstComputer && !GameManager.ValidateUserName(this.PlayerTwoName))
            {
                MessageBox.Show("Invalid name for player 2 - Name must have less than 10 characters and no spaces.");
            }
            else if (this.PlayerOneName == this.PlayerTwoName)
            {
                MessageBox.Show("Players can't have the same name.");
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
