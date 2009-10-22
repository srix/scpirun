using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ScpiTest
{
    delegate void ResumeDelegate();

    public enum State
    {
        Running,
        Paused,
        Edit,
        View,
        RunFinished
    }
   
     
    public partial class formScpiGUI : Form
    {

        public formScpiGUI()
        {
            InitializeComponent();
            newTab_Click(this, new EventArgs());

            //read MRU tragets from settings file and add to comboBoxInsName
            string strMruTargets = Properties.Settings.Default.MruTargets;
            comboBoxInsName.Items.AddRange(strMruTargets.Split(';'));

            //read MRU tragets from settings file and add to comboBoxFileName
            string strMruFileNames = Properties.Settings.Default.MruFileNames;
            comboBoxFileName.Items.AddRange(strMruFileNames.Split(';'));

            //remove this
           // CheckForIllegalCrossThreadCalls = false;
        }

        private void newTab_Click(object sender, EventArgs e)
        {
            //Add tab page
            TabPage newTabPage = new TabPage();
            ScpiListView newListView = new ScpiListView();
            newTabPage.Controls.Add(newListView);
            TabData tabData = new TabData();
            newTabPage.Tag = tabData;
            tabControl.Controls.Add(newTabPage);

            //Set focus
            tabControl.SelectedTab = newTabPage;

            //Clear Details
            comboBoxFileName.Text = "";
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "SCPI files (*.txt;*.scpi)|*.txt;*.scpi|All files (*.*)|*.*";
            if (fileDlg.ShowDialog() != DialogResult.Cancel)
            {
                comboBoxFileName.Text = fileDlg.FileName;
                LoadFile(fileDlg.FileName);

            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            TabData tabData = (TabData)tabControl.SelectedTab.Tag;

            switch (tabData.state)
            {
                case State.Edit:
                    FinishEdit();
                    Start();
                    break;
                case State.View:
                    Start();
                    break;
                case State.RunFinished:
                    LoadFile(tabData.fileName);
                    Start();
                    break;
               // case State.Running:
                 //   Pause();
                    break;
            }
        }

        private void Start()
        {
            buttostart.Image = Properties.Resources.PausePressed;
            buttonStop.Enabled = true;

            TabPage curTabPage = GetCurTabPage();
            TabData tabData = (TabData)curTabPage.Tag;
            tabData.state = State.Running;
            tabData.machineName = comboBoxInsName.Text;
            tabData.port = Convert.ToInt32(textBoxPort.Text);
            tabData.fileName = comboBoxFileName.Text;
            tabData.parseStd = ParseStd.TEK;

            //disable the input fields
            comboBoxFileName.Enabled = false;
            comboBoxInsName.Enabled = false;
            textBoxPort.Enabled = false;

            //ScpiListView scpiListView = GetCurrentListview();
            //scpiListView.ResetScpiList();
            try
            {
                Thread scpiTestThread = new Thread(this.ScpiRunner);
               
                tabData.scpiTestThread = scpiTestThread;

                //store the modified struct data back to the tag
                //curTabPage.Tag = tabData;

                scpiTestThread.Start(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                //Enable the input fields
                comboBoxFileName.Enabled = true;
                comboBoxInsName.Enabled = true;
                textBoxPort.Enabled = true;
                
                //set the play icon
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formScpiGUI));
                buttostart.Image = ((System.Drawing.Image)(resources.GetObject("buttostart.Image")));
                buttonStop.Enabled = false;
            }
        }

        private void Pause()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formScpiGUI));
            buttostart.Image = ((System.Drawing.Image)(resources.GetObject("buttostart.Image")));
            TabData tabData = (TabData)tabControl.SelectedTab.Tag;
            tabData.state = State.Paused;

           // tabData.scpiTestThread.Suspend();

        }

        private void Stop()
        {
        }

        private void ScpiGUI_Resize(object sender, EventArgs e)
        {
            tabControl.Top = panelMain.Bottom;
            tabControl.Left = panelMain.Left;
            tabControl.Width = this.Width-2;
            tabControl.Height = this.Height - panelMain.Height-50;

        }

        private void comboBoxInsName_SelectedValueChanged(object sender, EventArgs e)
        {
            int i = 0;
        }

        private void comboBoxInsName_Leave(object sender, EventArgs e)
        {
            if (comboBoxInsName.Text.Trim() != "")
            {
                // Add to the history if it hasn't been added already
                if (!comboBoxInsName.Items.Contains(comboBoxInsName.Text))
                { comboBoxInsName.Items.Add(comboBoxInsName.Text); }
            }
        }

        private void comboBoxFileName_Leave(object sender, EventArgs e)
        {
            if (comboBoxFileName.Text.Trim() != "")
            {
                // Add to the history if it hasn't been added already
                if (!comboBoxFileName.Items.Contains(comboBoxFileName.Text))
                { comboBoxFileName.Items.Add(comboBoxFileName.Text); }
            }
        }


        private void SaveSettings()
        {
            //Save traget instrument names
            System.Collections.IEnumerator  enumerator = comboBoxInsName.Items.GetEnumerator();
            string mruInstrumentNames="";
            while(enumerator.MoveNext()==true)
            {
                mruInstrumentNames += enumerator.Current+";";
            }
            //remove the trailing ';'
            mruInstrumentNames.Trim(';');

            Properties.Settings.Default.MruTargets = mruInstrumentNames;

            //Save MRU file names
            enumerator = comboBoxFileName.Items.GetEnumerator();
            string mruFileNames = "";
            while (enumerator.MoveNext() == true)
            {
                mruFileNames += enumerator.Current + ";";
            }
            //remove the trailing ';'
            mruFileNames.Trim(';');

            Properties.Settings.Default.MruFileNames = mruFileNames;


            Properties.Settings.Default.Save();
        }

        private void formScpiGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Edit();
            
        }

        private void Edit()
        {
            TabPage curTabPage = GetCurTabPage();
            ScpiEditor scpiEditor = new ScpiEditor();

            TabData tabData = (TabData) curTabPage.Tag;
            tabData.state = State.Edit;

            //copy Scpilistview commands to scpi editor
            ScpiListView curScpiListView = (ScpiListView)curTabPage.Controls[0];

            string readStr = "";
            string editorTxt = "";
            while (curScpiListView.GetNextLine(ref readStr) == true)
            {
                editorTxt += readStr + System.Environment.NewLine;
            }
            //TODO:remove the extra new line
            //editorTxt.Remove(editorTxt.LastIndexOf(System.Environment.NewLine));

            scpiEditor.Text = editorTxt;

            curTabPage.Controls.Remove(curScpiListView);
            curScpiListView.Dispose();
            curTabPage.Controls.Add(scpiEditor);
        }

        private void FinishEdit()
        {
            TabPage curTabPage = GetCurTabPage();
            ScpiEditor scpiEditor = (ScpiEditor)curTabPage.Controls[0];

            ScpiListView scpiListView = new ScpiListView();
            scpiListView.LoadFromEditor(scpiEditor);

            curTabPage.Controls.RemoveAt(0);
            scpiEditor.Dispose();
            curTabPage.Controls.Add(scpiListView);

        }

        /// <summary>
        /// launches a thred that runs scpitest. scpitest needs to be tun in a 
        /// seperate thread to enable pause and breakpoint features
        /// </summary>
        private void ScpiRunner()
        {
            //Always get the curtabpage and work on it instead of directly
            //working on tabControl.SelectedTab. This helps if the selected
            //tab chnages evne before the function gets completed
            //TabPage curTabPage = tabControl.SelectedTab;
            //ScpiListView curScpilistView = (ScpiListView)curTabPage.Controls[0];
            ScpiListView curScpilistView = (ScpiListView)this.Invoke(new GetCurrentListviewDelegate(this.GetCurrentListview), null);
            TabData tabData = (TabData)this.Invoke(new GetCurTabDataDelegate(this.GetCurTabData), null);

            try
            {
                //AutoResetEvent syncEvent = new AutoResetEvent(false);
                TextParser parser = new TextParser(curScpilistView, ParseStd.TEK);
                ScpiTest scpiTest = new ScpiTest(parser, curScpilistView, tabData.machineName, tabData.port);
                //tabData.syncEvent = syncEvent;
              scpiTest.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void comboBoxFileName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadFile(comboBoxFileName.Text);
            
            TabPage curTabPage = tabControl.SelectedTab;
            TabData tabData =(TabData)curTabPage.Tag ;
            tabData.fileName = comboBoxFileName.Text;
            curTabPage.Tag = tabData;
        }

        private void LoadFile(string fileName)
        {
            TabPage curTabPage = GetCurTabPage();
            TabData tabData = (TabData)curTabPage.Tag;
            tabData.fileName = fileName;
            tabData.parseStd = ParseStd.TEK;
            tabData.state = State.View;
            

            curTabPage.Text = System.IO.Path.GetFileName(fileName);


            ScpiListView scpiListView = (ScpiListView)tabControl.SelectedTab.Controls[0];
            scpiListView.LoadFromFile(tabData);

        }

        

        //private void CommitTabData()
        //{
        //    TabData tabData = new TabData();
        //    tabData.machineName = comboBoxInsName.Text;
        //    tabData.port = Convert.ToInt32(textBoxPort.Text);
        //    tabData.fileName = comboBoxFileName.Text;
        //    tabData.parseStd = ParseStd.TEK;
        //    //tabData.scpiTestThread = 
        //    //tabData.state=

        //    TabPage curTabPage = tabControl.SelectedTab;
        //    curTabPage.Tag = tabData;
        //}

        //private void ShowTabData()
        //{
        //    TabPage curTabPage = tabControl.SelectedTab;
        //    TabData tabData = (TabData)curTabPage.Tag;

        //    comboBoxInsName.Text = tabData.machineName;
        //    comboBoxFileName.Text = tabData.fileName;
        //    textBoxPort.Text = tabData.port.ToString();
            
        //}

        #region Functions for cross thread calls
        private ScpiListView GetCurrentListview()
        {
            //Always get the curtabpage and work on it instead of directly
            //working on tabControl.SelectedTab. This helps if the selected
            //tab chnages evne before the function gets completed
            TabPage curTabPage = tabControl.SelectedTab;
            return (ScpiListView)curTabPage.Controls[0];
        }
        private TabData GetCurTabData()
        {           
            TabPage curTabPage = tabControl.SelectedTab;
            return (TabData)curTabPage.Tag;
        }

        delegate ScpiListView GetCurrentListviewDelegate();
        delegate TabData GetCurTabDataDelegate();

        
        #endregion

        private void buttonSave_Click(object sender, EventArgs e)
        {
            
            TabPage curTabPage = GetCurTabPage();
            TabData tabData = (TabData)curTabPage.Tag;

            if (State.Edit == tabData.state)
            {
                ScpiEditor scpiEditor = (ScpiEditor)curTabPage.Controls[0];
                //Use StreamWriter class.
                System.IO.StreamWriter sw = new System.IO.StreamWriter(tabData.fileName);

                //Use writeline methode to write the text and
                //in para.. put your text, i have used  textBox1's text  
                sw.WriteLine(scpiEditor.Text);

                //always close your stream
                sw.Close();

            }


        }

        //GEt the curtabpage using a separate function so any
        //invoke lgoc if required later can be encapsulated.
        private TabPage GetCurTabPage()
        {
            return tabControl.SelectedTab;
        }

       

       
       
    }
    public class TabData
    {
        public string machineName;
        public int port;
        public string fileName;
        public ParseStd parseStd;
        public State state;
        public Thread scpiTestThread;
        //public AutoResetEvent syncEvent;
    }

}