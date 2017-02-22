using System;
using System.Windows.Forms;

namespace Keylogger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Visible = false;
            notifyIcon1.ShowBalloonTip(1000, "IMM CORP", "Listo!", ToolTipIcon.Info);
        }
        

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if(IsValidPassword())
                Application.Exit();
        }

        private void startTool_Click(object sender, EventArgs e)
        {
            if (IsValidPassword())
            {
                Program.kl.Enabled = true;
                startTool.Enabled = false;
                stopTool.Enabled = true;
                notifyIcon1.ShowBalloonTip(1000, "IMM CORP", "Started!", ToolTipIcon.Info);
            }
        }

        private void stopTool_Click(object sender, EventArgs e)
        {
            if (IsValidPassword())
            {
                Program.kl.Enabled = false;
                stopTool.Enabled = false;
                startTool.Enabled = true;
                notifyIcon1.ShowBalloonTip(1000, "IMM CORP", "Stopped!", ToolTipIcon.Info);
            }
        }

        private bool IsValidPassword()
        {
            var clave = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la clave", "Seguridad");
            return clave == "654123";
        }
    }
}
