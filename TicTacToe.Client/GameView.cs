using TicTacToe.Client.Controls;

namespace TicTacToe.Client
{
    public partial class GameView : Form
    {
        public GameView()
        {
            InitializeComponent();
            Controls.Add(new ConnectMenu(this));
        }

        public void AddViewControl(UserControl control)
        {
            Invoke(() => Controls.Add(control));
        }

        public void RemoveViewControl(UserControl control)
        {
            Invoke(() => Controls.Remove(control));
        }
    }
}