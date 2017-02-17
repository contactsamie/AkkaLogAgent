using AkkaLogAgent.Services;
using NLog;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AkkaLogAgent.AgentLogConsumerServices
{
    public class WinFormUiAgentLogConsumer : IAgentLogConsumer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly Form _thisForm;
        private int RestartAfterCount { set; get; }
        private Timer Timer { set; get; }
        private Panel IndicatorPanel { set; get; }
        private string _shorCurToExecute = @"R:\InventoryService\topshelf-install - Shortcut.lnk";

        public WinFormUiAgentLogConsumer(Form form, RichTextBox displayTxt, RichTextBox richTextBox,
            Panel indicatorPanel)
        {
            Timer = new Timer();
            Log.Debug("Starting " + nameof(WinFormUiAgentLogConsumer) + "...");
            MaxRestartCount = 1; //vary this
            TimerInterval = 10000;
            _thisForm = form;
            DisplayTxt = displayTxt;
            RichTextBox = richTextBox;
            IndicatorPanel = indicatorPanel;
            Timer.Interval = TimerInterval;
            Timer.Tick += Timer_Tick;
            OnStarted();
        }

        public int TimerInterval { get; set; }

        private void Timer_Tick(object sender, EventArgs e)
        {
            OnTimerTick();
        }

        public static bool Restarting = false;
        private readonly object _thislock = new object();
        private bool _justRecovered = true;

        private void OnTimerTick()
        {
            if (Restarting)
            {
                return;
            }
            if (RestartAfterCount > MaxRestartCount)
            {
                //lock (_thislock)
                //{
                Restarting = true;
                const string message = "Max restart counter exceeded, application will now be restarted!!!";
                Log.Debug(message);
                ThreadHelperClass.SetBackColor(_thisForm, IndicatorPanel, Color.Red);
                try
                {
                    ExecuteWindowsShortCut(_shorCurToExecute);
                    _justRecovered = true;
                    Log.Debug("Restart executed successfully");
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error trying to execute restart command");
                }
                //}
            }
            else
            {
                if (_justRecovered)
                {
                    Log.Debug("System just restarted. Everything looks good!");
                    _justRecovered = false;
                }

                ThreadHelperClass.SetBackColor(_thisForm, IndicatorPanel, Color.GreenYellow);
            }
            Restarting = false;
            RestartAfterCount = 0;
        }

        private static void ExecuteWindowsShortCut(string path)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                // WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = path
            };
            process.StartInfo = startInfo;
            process.Start();
        }

        private static void ExecuteWindowsCommand(string commandString)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + commandString
            };
            process.StartInfo = startInfo;
            process.Start();
        }

        public int MaxRestartCount { get; set; }

        private RichTextBox DisplayTxt { get; set; }
        private RichTextBox RichTextBox { set; get; }

        public void OnEachLogUpdate(string logUpdate)
        {
            if (logUpdate.Contains("Disassociated [akka.tcp://"))
            {
                RestartAfterCount++;
                var message = ">>>>>>>>>>>>>>>> RESTART COUNTER INCREMENTED NOW AT : " + RestartAfterCount;
                ThreadHelperClass.SetText(_thisForm, RichTextBox, message);
                Log.Debug(message);
            }
            ThreadHelperClass.SetText(_thisForm, RichTextBox, logUpdate);
        }

        public void OnBatchLogUpdate(string batchLogUpdate)
        {
            ThreadHelperClass.SetText(_thisForm, DisplayTxt, batchLogUpdate);
        }

        public void OnStoped()
        {
            Timer.Enabled = false;
            ThreadHelperClass.SetBackColor(_thisForm, IndicatorPanel, Color.Azure);
        }

        public void OnStarted()
        {
            Timer.Enabled = true;
            OnTimerTick();
        }
    }
}