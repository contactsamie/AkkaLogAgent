using AkkaLogAgent.AgentLogConsumerServices;
using AkkaLogAgent.Common;
using AkkaLogAgent.DefaultLogFileHandler;
using AkkaLogAgent.Services;
using NLog;
using System;
using System.IO;
using System.Windows.Forms;

namespace AkkaLogAgent.WinForm
{
    public partial class Form1 : Form
    {
        private readonly LogWatchServiceAgentService _serviceAgent = new LogWatchServiceAgentService(new DefaultLogFileUpdateHandler());
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private IAkkaLogAgentUI AkkaLogAgentUi { set; get; }

        public Form1()
        {
            InitializeComponent();
            AkkaLogAgentUi = new WinFormAkkaLogAgentUI(ErrorNotificationTxt, DisplayTxt, FolderPathTxt, RegexTxt, _serviceAgent, stopToolStripMenuItem, startToolStripMenuItem, this);
        }

        private void selectPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFolderPath();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AkkaLogAgentUi.StopMonitoring();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SelectFolderPath(@"D:\Logs");
            }
            catch (Exception)
            {
            }
        }

        public void SelectFolderPath(string folderPath = null)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        folderPath = fbd.SelectedPath;
                    }
                    else
                    {
                        ErrorNotificationTxt.Text = "no folder chosen!!";
                    }
                }
            }

            if (string.IsNullOrEmpty(folderPath)) return;

            Log.Debug("Seting folder " + folderPath + "...");
            FolderPathTxt.Text = folderPath;
            var files = Directory.GetFiles(folderPath);
            DisplayTxt.Text = string.Join(Environment.NewLine, files);
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
            AkkaLogAgentUi.StartMonitoring(debugMode, FolderPathTxt.Text, RegexTxt.Text, new WinFormUiAgentLogConsumer( this, DisplayTxt, richTextBox1, indicatorPannel, richTextBox2));
            stopToolStripMenuItem.Visible = true;
            startToolStripMenuItem.Visible = false;
        }

        private void debugModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartMonitoring(true);
        }
    }
}