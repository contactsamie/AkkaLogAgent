using AkkaLogAgent.Services;
using System;
using System.Windows.Forms;

namespace AgentLogConsumerServices
{
    public class WinFormUiAgentLogConsumer : IAgentLogConsumer
    {
        private readonly Form _thisForm;
        private int RestartAfterCount { set; get; }
        private Timer Timer { set; get; }

        public WinFormUiAgentLogConsumer(Form form, RichTextBox displayTxt, RichTextBox richTextBox)
        {
            Timer = new Timer();
            _thisForm = form;
            DisplayTxt = displayTxt;
            RichTextBox = richTextBox;
            Timer.Interval = 20000;
            Timer.Tick += Timer_Tick;
            Timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (RestartAfterCount > 2)
            {
                //RESTART!!!!
            }
            RestartAfterCount = 0;
        }

        private RichTextBox DisplayTxt { get; set; }
        private RichTextBox RichTextBox { set; get; }

        public void OnEachLogUpdate(string logUpdate)
        {
            if (logUpdate.Contains("Disassociated [akka.tcp://"))
            {
                RestartAfterCount++;
            }
            ThreadHelperClass.SetText(_thisForm, RichTextBox, logUpdate);
        }

        public void OnBatchLogUpdate(string batchLogUpdate)
        {
            ThreadHelperClass.SetText(_thisForm, DisplayTxt, batchLogUpdate);
        }
    }
}