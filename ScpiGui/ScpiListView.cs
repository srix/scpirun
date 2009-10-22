using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ScpiTest
{
    class ScpiListView : ListViewEx, ISource, IOutput
    {
        //Header index
        readonly int HDR_CURRENT = 0;
        readonly int HDR_COMMAND = 1;
        readonly int HDR_RESULT = 2;

        readonly Color COLOR_INVALID = Color.FromArgb(255, 128, 128);
        readonly Color COLOR_COMPLETED = Color.FromArgb(200, 200, 200);
        readonly Color COLOR_DEFAULT = Color.FromArgb(255, 255, 255);

        private ColumnHeader columnHeaderCurrent;
        private ColumnHeader columnHeaderCmd;
        private ColumnHeader columnHeaderResult;
        //this pointer tracks the currnt line which needs to be executed in Scpilistview
        private int m_ParserPointer;

        private Timer waitTimer;
        private ProgressBar waitProgressBar;

        delegate void ShowCommandDelegate(ScpiCommand scpiCmd);
        delegate void ShowResultDelegate(string scpiCmd);
        delegate void ShowErrorDelegate(string errorStr);
        delegate void ShowWaitDelegate(ScpiCommand scpiCmd);
        delegate bool GetNextLineDelegate(ref string readStr);

        ImageList imageList;

        public ScpiListView()
        {
            this.Dock = DockStyle.Fill;
            this.FullRowSelect = true;
            //this.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            //this.ShowGroups = true;
            this.View = System.Windows.Forms.View.Details;

            this.columnHeaderCurrent = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCmd = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderResult = new System.Windows.Forms.ColumnHeader();

            columnHeaderCurrent.Text = ">";
            columnHeaderResult.Text = "Result";
            columnHeaderCmd.Text = "Command";

            columnHeaderCurrent.Width = 50;
            columnHeaderCmd.Width = 350;
            columnHeaderResult.Width = 200;


            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCurrent,
            this.columnHeaderCmd,
            this.columnHeaderResult});

            waitTimer = new Timer();
            waitTimer.Interval = 1000;
            waitTimer.Tick += new System.EventHandler(waitTimer_Tick);

            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ScpiListView_MouseDoubleClick);

            waitProgressBar = new ProgressBar();

            this.m_ParserPointer = 0;

            //imageList = new ImageList();
            //imageList.Images.Add((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
            //this.imageList = imageList;

        }

        //This function is used by cross thread calls
        //public void ReadNextLine(ref string readStr, ref bool status)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        Object[] args = { readStr, status};
        //        this.Invoke(new ShowGetNextLineDelegate(this.ReadNextLine), args);
        //        readStr = (string)args[0];
        //        status = (bool)args[1];
        //    }
        //    else
        //    {
        //         status = true;
        //        if (m_ParserPointer < this.Items.Count)
        //        {
        //            readStr = this.Items[m_ParserPointer].SubItems[HDR_COMMAND].Text;
        //            m_ParserPointer++;
        //        }
        //        else
        //        {
        //            status = false;
        //        }
        //    }
        //}

        //This functin is used by GUI calls
        public bool GetNextLine(ref string readStr)
        {
            if (this.InvokeRequired)
            {
                Object[] args = { readStr};
                bool status = (bool)this.Invoke(new GetNextLineDelegate(this.GetNextLine), args);
                readStr = (string)args[0];
                return status;
            }
            else
            {
                bool status = true;
                if (m_ParserPointer < this.Items.Count)
                {
                    readStr = this.Items[m_ParserPointer].SubItems[HDR_COMMAND].Text;
                    m_ParserPointer++;
                }
                else
                {
                    status = false;
                    TabPage parentTab = (TabPage)this.Parent;
                    TabData tabData = (TabData)parentTab.Tag;
                    tabData.state = State.RunFinished;
                    m_ParserPointer = 0;
                    
                }
                return status;
            }
        }

       

        public void LoadFromFile(TabData tabData)
        {
            //clear the list view
            this.Items.Clear();

            //Create parser
            TextParser txtParser = new TextParser(tabData.fileName, tabData.parseStd);

            //Load the file in the listview
            ScpiCommand scpiCmd = new ScpiCommand();

            while (txtParser.GetNextLine(ref scpiCmd))
            {
                AddLine(scpiCmd);
            }

        }

        public void LoadFromEditor(ScpiEditor scpiEditor)
        {
            //clear the list view
            this.Items.Clear();

            //Create parser
            TextParser txtParser = new TextParser(scpiEditor, ParseStd.SFXSCPI);//SRIX:change pasrsetd:sfxscpi to variable

            //Load the file in the listview
            ScpiCommand scpiCmd = new ScpiCommand();

            while (txtParser.GetNextLine(ref scpiCmd))
            {
                AddLine(scpiCmd);
            }


        }

        public void ShowCommand(ScpiCommand scpiCmd)
        {

            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowCommandDelegate(this.ShowCommand),new object[]{scpiCmd});
            }
            else
            {

                ListViewItem curItem = this.Items[m_ParserPointer - 1];
                curItem.BackColor = COLOR_COMPLETED;
            }

        }
        public void ShowResult(string result)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowResultDelegate(this.ShowResult), new object[] { result });
            }
            else
            {
                string g = this.Items[m_ParserPointer - 1].SubItems[HDR_RESULT].Text;
                this.Items[m_ParserPointer - 1].SubItems[HDR_RESULT].Text = result;
            }
        }
        public void ShowError(string errorStr)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowErrorDelegate(this.ShowError), new object[] { errorStr });
            }
            else
            {
                MessageBox.Show(errorStr);
            }
        }

        private void AddLine(ScpiCommand scpiCmd)
        {
            ListViewItem item = new ListViewItem();
            item.SubItems.Add(scpiCmd.Str);//HDR_COMMAND
            item.SubItems.Add("");//HDR_RESULT

            if (ScpiCommandType.INVALID == scpiCmd.Type)
            {
                item.BackColor = COLOR_INVALID;
                item.UseItemStyleForSubItems = true;
            }


            this.Items.Add(item);
        }

        public void ShowWait(ScpiCommand scpiCmd)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowWaitDelegate(this.ShowWait), new object[] { scpiCmd });
            }
            else
            {
               
               
                string cmd = scpiCmd.Str;
                cmd = cmd.Remove(0, 2);
                cmd.Trim();
                int wait = Convert.ToInt32(cmd);

                waitProgressBar.Maximum = wait;
                waitProgressBar.Visible = true;
                // pg.s

                //for (int i = 0; i < wait; i++)
                //{
                //    //when the output of scpugui is directed to text file
                //    //Invalid handle error appears when drawtextprogressbar is first called
                //    //And scpigui stops there.
                //    System.Threading.Thread.Sleep(1000);
                //    pg.Value = i;
                //}

                this.AddEmbeddedControl(waitProgressBar, HDR_RESULT, m_ParserPointer - 1);

                TabPage parentTab = (TabPage)this.Parent;
                TabData tabData = (TabData)parentTab.Tag;
                tabData.scpiTestThread.Suspend();

                
                 waitTimer.Start();
                
            }
        }


        public void ReziseColumns()
        {

        }

        private void waitTimer_Tick(object sender, EventArgs e)
        {
            if (waitProgressBar.Value < waitProgressBar.Maximum)
            {
                waitProgressBar.Value++;
            }
            else
            {
                this.RemoveEmbeddedControl(waitProgressBar);
                waitTimer.Stop();
                ListViewItem curItem = this.Items[m_ParserPointer -1];
                curItem.BackColor = COLOR_COMPLETED;
                //waitProgressBar.Visible = false;
                waitProgressBar.Value = 0;

                TabPage parentTab = (TabPage)this.Parent;
                TabData tabData = (TabData)parentTab.Tag;
                tabData.scpiTestThread.Resume();
                
            }
        }

        private void ScpiListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem listViewItem = this.SelectedItems[0];
            // listViewItem.Image = new Image("file path to image");
              //  listView1.items.add(listImage);}
            //listViewItem.im
        }

        //public void ResetScpiList()
        //{
        //    m_ParserPointer = 0;
        //    for (int i = 0; i < this.Items.Count; i++)
        //    {
        //        this.Items[i].BackColor = COLOR_DEFAULT;
        //    }

        //}
    }
}
