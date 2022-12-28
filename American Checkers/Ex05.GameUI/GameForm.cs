using System.Windows.Forms;

namespace Ex05.GameUI
{
    public partial class GameForm : Form
    {
        private GameSettingsForm m_SettingsForm;

        public GameForm()
        {
            m_SettingsForm = new GameSettingsForm();
            InitializeComponent();
        }

        public GameSettingsForm SettingsForm
        {
            get { return m_SettingsForm; }
        }
    }
}
