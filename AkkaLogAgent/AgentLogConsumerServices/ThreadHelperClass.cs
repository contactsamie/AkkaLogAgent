using System.Drawing;
using System.Windows.Forms;

namespace AkkaLogAgent.AgentLogConsumerServices
{
    public static class ThreadHelperClass
    {
        private delegate void SetTextCallback(Form f, Control ctrl, string text);

        private delegate void SetBackColorCallback(Form f, Control ctrl, Color color);

        public static void SetText(Form form, Control ctrl, string text)
        {
            if (form == null) return;
            if (ctrl == null) return;
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (ctrl.InvokeRequired)
            {
                var d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void SetBackColor(Form form, Control ctrl, Color color)
        {
            if (form == null) return;
            if (ctrl == null) return;

            if (ctrl.InvokeRequired)
            {
                var d = new SetBackColorCallback(SetBackColor);
                form.Invoke(d, new object[] { form, ctrl, color });
            }
            else
            {
                ctrl.BackColor = color;
            }
        }
    }
}