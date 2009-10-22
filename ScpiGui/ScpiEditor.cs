using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ScpiTest
{
    class ScpiEditor:TextBox,ISource
    {
        private int m_ParserPointer;

        public ScpiEditor()
        {
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Multiline = true;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.WordWrap = false;

            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ScpiEditor_KeyUp);
            this.m_ParserPointer = 0;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ScpiEditor
            // 
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ScpiEditor_KeyUp);
            this.ResumeLayout(false);

        }

        private void ScpiEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey && e.KeyValue == 16)
            {
                MessageBox.Show(":");
            }
        }

        public bool GetNextLine(ref string readStr)
        {
            bool status = true;
            if (m_ParserPointer < this.Lines.Length)
            {
                readStr = this.Lines[m_ParserPointer];
                m_ParserPointer++;
            }
            else
            {
                status = false;
            }
            return status;
                    
        }

        
    }
}
