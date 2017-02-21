using AkkaLogAgent.Common;
using AkkaLogAgent.ServiceDeployment;
using AkkaLogAgent.Services;
using System.Windows.Forms;

namespace AkkaLogAgent.WinForm
{
    public class WinFormAkkaLogAgentUI : IAkkaLogAgentUI
    {
        private DeployedServiceAkkaLogAgentUI DeployedServiceAkkaLogAgentUI { set; get; }
        private ToolStripTextBox FolderPathTxt { set; get; }
        private ToolStripTextBox RegexTxt { set; get; }
        private ToolStripMenuItem StopToolStripMenuItem { set; get; }
        private Form ParentForm { set; get; }
        private ToolStripStatusLabel ErrorNotificationTxt { set; get; }
        private ToolStripMenuItem StartToolStripMenuItem { set; get; }
        private Control DisplayTxt { set; get; }

        public WinFormAkkaLogAgentUI(ToolStripStatusLabel errorNotificationTxt, Control displayTxt, ToolStripTextBox folderPathTxt, ToolStripTextBox regexTxt, LogWatchServiceAgentService serviceAgent, ToolStripMenuItem stopToolStripMenuItem, ToolStripMenuItem startToolStripMenuItem, Form parentForm)
        {
          
            DisplayTxt = displayTxt;
            ErrorNotificationTxt = errorNotificationTxt;
            FolderPathTxt = folderPathTxt;
            RegexTxt = regexTxt;
            StopToolStripMenuItem = stopToolStripMenuItem;
            StartToolStripMenuItem = startToolStripMenuItem;
            ParentForm = parentForm;
            DeployedServiceAkkaLogAgentUI = new DeployedServiceAkkaLogAgentUI(serviceAgent);
        }

        public void StartMonitoring(bool debugMode, string path, string fileFilter, IAgentLogConsumer agentLogConsumer)
        {
            if (string.IsNullOrEmpty(FolderPathTxt.Text))
                return;

            ParentForm.Text = debugMode ? "DEBUG MODE : Akka Log Agent" : "Akka Log Agent";

            DeployedServiceAkkaLogAgentUI.StartMonitoring(debugMode, path, fileFilter, agentLogConsumer);
            StopToolStripMenuItem.Visible = true;
            StartToolStripMenuItem.Visible = false;
        }

        public void StopMonitoring()
        {
            DeployedServiceAkkaLogAgentUI.StopMonitoring();
            StopToolStripMenuItem.Visible = false;
            StartToolStripMenuItem.Visible = true;
        }
    }
}