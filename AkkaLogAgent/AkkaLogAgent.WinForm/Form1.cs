using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AgentLogConsumerServices;
using AkkaLogAgent.Services;

namespace AkkaLogAgent.WinForm
{
    public partial class Form1 : Form
    {
        private readonly ServiceAgent _serviceAgent=new ServiceAgent();

        public Form1()
        {
            InitializeComponent();
           
        }
        public void ShowErrorMessage(string message)
        {
            ErrorNotificationTxt.Text = message;
        }

        public void ShowInfoMessage(string message)
        {
      
        }

     
     

        private void selectPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    FolderPathTxt.Text = fbd.SelectedPath;
                    var files = Directory.GetFiles(fbd.SelectedPath);
                    DisplayTxt.Text = string.Join(Environment.NewLine, files);
                }
                else
                {
                    ShowErrorMessage("no folder chosen!!");
                }
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _serviceAgent.StopWatchingFiles();
            stopToolStripMenuItem.Visible = false;
            startToolStripMenuItem.Visible =true ;
         
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(FolderPathTxt.Text))
                return;

            _serviceAgent.StartWatchingFiles(FolderPathTxt.Text,"", new WinFormUiAgentLogConsumer(this, DisplayTxt, richTextBox1));
            stopToolStripMenuItem.Visible = true;
            startToolStripMenuItem.Visible = false;
        }
    }
   
}