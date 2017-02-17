using AkkaLogAgent.AgentLogConsumerServices;
using AkkaLogAgent.Services;
using NLog;
using System;
using System.IO;
using System.Windows.Forms;

namespace AkkaLogAgent.WinForm
{
    public partial class Form1 : Form
    {
        private readonly LogWatchServiceAgent _serviceAgent = new LogWatchServiceAgent();
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

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

        private void SetFolder(string folderPath)
        {
            Log.Debug("Seting folder " + folderPath + "...");
            FolderPathTxt.Text = folderPath;
            var files = Directory.GetFiles(folderPath);
            DisplayTxt.Text = string.Join(Environment.NewLine, files);
        }

        private void selectPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SetFolder(fbd.SelectedPath);
                }
                else
                {
                    ShowErrorMessage("no folder chosen!!");
                }
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.Debug("Stoppinging log monitoring in " + this.GetType().Name + "...");
            _serviceAgent.StopWatchingFiles();
            stopToolStripMenuItem.Visible = false;
            startToolStripMenuItem.Visible = true;
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SetFolder(@"D:\Logs");
            }
            catch (Exception)
            {
            }
        }

        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StartMonitoring(false);
        }

        private void StartMonitoring(bool debugMode)
        {
            if (string.IsNullOrEmpty(FolderPathTxt.Text))
                return;

            Text = debugMode ? "DEBUG MODE : Akka Log Agent" : "Akka Log Agent";

            Log.Debug("Starting log monitoring in " + this.GetType().Name + "...");
            _serviceAgent.StartWatchingFiles(FolderPathTxt.Text, RegexTxt.Text,
                new WinFormUiAgentLogConsumer(this, DisplayTxt, richTextBox1, indicatorPannel, debugMode,richTextBox2));
            stopToolStripMenuItem.Visible = true;
            startToolStripMenuItem.Visible = false;
        }

        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartMonitoring(true);
        }
    }
}