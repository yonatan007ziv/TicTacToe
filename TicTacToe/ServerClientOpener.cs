using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class ServerClientOpener : Form
    {
        public ServerClientOpener()
        {
            InitializeComponent();
        }
        private void openServerButton(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(hostThread));
            t.Start();
        }
        private void openClientButton(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(clientThread));
            t.Start();
        }

        private void hostThread()
        {
            var form = new TicTacToeServer();
            form.ShowDialog();
        }
        private void clientThread()
        {
            var form = new TicTacToe();
            form.ShowDialog();
        }
        private void CloseAll(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
