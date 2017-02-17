using AkkaLogAgent.Services;
using NLog;
using System;
using System.Collections.Generic;
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
        private const string _shorCurToExecute = @"R:\InventoryService\topshelf-install - Shortcut.lnk";
        private bool DebugMode { set; get; }
        private RichTextBox CommonAppLogView { set; get; }
        public List<string> AppLogs { set; get; }

        public void AppLog(string message)
        {
            if (!DebugMode)
                return;
            AppLogs.Add(message);
            AppLogs.Reverse();
            ThreadHelperClass.SetText(_thisForm, CommonAppLogView, string.Join(Environment.NewLine, AppLogs));
            AppLogs.Reverse();
        }

        public WinFormUiAgentLogConsumer(Form form, RichTextBox bulkLogView, RichTextBox lastEventLogView,
            Panel indicatorPanel, bool debugMode, RichTextBox commonAppLogView)
        {
            AppLogs=new List<string>();
            CommonAppLogView = commonAppLogView;
            DebugMode = debugMode;
            Timer = new Timer();
            Log.Debug("Instantiating " + nameof(WinFormUiAgentLogConsumer) + "...");
            MaxRestartCount = 1; //vary this
            TimerInterval = 10000;
            _thisForm = form;
            DisplayTxt = bulkLogView;
            RichTextBox = lastEventLogView;
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
                Restarting = true;
                const string message = "Max restart counter exceeded, application will now be restarted!!!";
                Log.Debug(message);
                ThreadHelperClass.SetBackColor(_thisForm, IndicatorPanel, Color.Red);
                try
                {
                    if (!DebugMode)
                    {
                        ExecuteWindowsShortCut(_shorCurToExecute);
                    }
                    _justRecovered = true;
                    Log.Debug("Restart executed successfully");
                    AppLog("Restart executed successfully");
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error trying to execute restart command");
                    AppLog("Error trying to execute restart command " + e.Message + " - " + e.InnerException?.Message);
                }
            }
            else
            {
                if (_justRecovered)
                {
                    Log.Debug("System just restarted. Everything looks good!");
                    AppLog("System just restarted. Everything looks good!");
                    _justRecovered = false;
                }

                ThreadHelperClass.SetBackColor(_thisForm, IndicatorPanel, Color.GreenYellow);
            }
            Restarting = false;
            RestartAfterCount = 0;
        }

        private  void ExecuteWindowsShortCut(string path)
        {
            var updateMessage = "executing short cut "+ path + " in " + nameof(WinFormUiAgentLogConsumer) + "...";
            AppLog(updateMessage);
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
            var updateMessage = "On OnEachLogUpdate in " + nameof(WinFormUiAgentLogConsumer) + "...";
            AppLog(updateMessage);
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
            var message = "On BatchLogUpdate in " + nameof(WinFormUiAgentLogConsumer) + "...";
            AppLog(message);
            ThreadHelperClass.SetText(_thisForm, DisplayTxt, batchLogUpdate);
        }

        public void OnStoped()
        {
            var message = "On Stopped in " + nameof(WinFormUiAgentLogConsumer) + "...";
            Log.Debug(message);
            AppLog(message);
            Timer.Enabled = false;
            ThreadHelperClass.SetBackColor(_thisForm, IndicatorPanel, Color.Azure);
        }

        public void OnStarted()
        {
            var message = "On Started in " + nameof(WinFormUiAgentLogConsumer) + "...";
            Log.Debug(message);
            AppLog(message);
            Timer.Enabled = true;
            OnTimerTick();
        }
    }
}