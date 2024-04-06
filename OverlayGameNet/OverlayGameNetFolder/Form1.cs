using OverlayGameNet.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OverlayGameNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_openOverlay_Click(object sender, EventArgs e)
        {
            GameOverlay.TimerService.EnableHighPrecisionTimers();
            using (var gameOverlayApp = new GameOverlayApp())
            {
                Task.Run(() => gameOverlayApp.Run());
            }
        }
    }
}
