using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SampleMqttClient
{
    public partial class ControlLog : UserControl
    {
        const int nLimitLines = 100;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);

        public ControlLog()
        {
            InitializeComponent();
        }

        private void TextBoxLogTop_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(TextBoxLog.Handle);
        }

        delegate void CrossThreadSafetyText(string text);
        public void SetLogText(string text)
        {
            if (TextBoxLog != null)
            {
                if (TextBoxLog.InvokeRequired)
                {
                    try
                    {
                        if (TextBoxLog != null)
                        {
                            TextBoxLog.Invoke(new CrossThreadSafetyText(SetLogText), text);
                        }

                    }
                    finally { }
                }
                else
                {
                    try
                    {
                        string AppendMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {text}\r\n";
                        TextBoxLog.AppendText(AppendMessage);

                        if (TextBoxLog.Lines.Length > nLimitLines)
                        {
                            LinkedList<string> tempLines = new(TextBoxLog.Lines);

                            while ((tempLines.Count - nLimitLines) > 0)
                            {
                                tempLines.RemoveFirst();
                            }

                            TextBoxLog.Lines = tempLines.ToArray();
                        }
                        TextBoxLog.Select(TextBoxLog.Text.Length, 0);
                        TextBoxLog.ScrollToCaret();
                    }
                    finally { }
                }
            }
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBoxLog.Text = string.Empty;
        }
    }
}
