namespace ScpiTest
{
    partial class formScpiGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formScpiGUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonCloseFile = new System.Windows.Forms.Button();
            this.buttonNewFile = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttostart = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxInsName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxFileName = new System.Windows.Forms.ComboBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonOpen);
            this.panel1.Controls.Add(this.buttonEdit);
            this.panel1.Controls.Add(this.buttonCloseFile);
            this.panel1.Controls.Add(this.buttonNewFile);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.buttostart);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Location = new System.Drawing.Point(6, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(569, 58);
            this.panel1.TabIndex = 0;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(440, 3);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(47, 50);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonCloseFile
            // 
            this.buttonCloseFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonCloseFile.Image")));
            this.buttonCloseFile.Location = new System.Drawing.Point(117, 3);
            this.buttonCloseFile.Name = "buttonCloseFile";
            this.buttonCloseFile.Size = new System.Drawing.Size(47, 50);
            this.buttonCloseFile.TabIndex = 5;
            this.buttonCloseFile.UseVisualStyleBackColor = true;
            // 
            // buttonNewFile
            // 
            this.buttonNewFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewFile.Image")));
            this.buttonNewFile.Location = new System.Drawing.Point(11, 3);
            this.buttonNewFile.Name = "buttonNewFile";
            this.buttonNewFile.Size = new System.Drawing.Size(47, 50);
            this.buttonNewFile.TabIndex = 3;
            this.buttonNewFile.UseVisualStyleBackColor = true;
            this.buttonNewFile.Click += new System.EventHandler(this.newTab_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(201, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(47, 50);
            this.button4.TabIndex = 4;
            this.button4.Text = "settings";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
            this.buttonStop.Location = new System.Drawing.Point(316, 3);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(47, 50);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttostart
            // 
            this.buttostart.Image = ((System.Drawing.Image)(resources.GetObject("buttostart.Image")));
            this.buttostart.Location = new System.Drawing.Point(254, 3);
            this.buttostart.Name = "buttostart";
            this.buttostart.Size = new System.Drawing.Size(47, 50);
            this.buttostart.TabIndex = 1;
            this.buttostart.UseVisualStyleBackColor = true;
            this.buttostart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(500, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(47, 50);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // tabControl
            // 
            this.tabControl.Location = new System.Drawing.Point(0, 147);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(655, 333);
            this.tabControl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Instrument";
            // 
            // comboBoxInsName
            // 
            this.comboBoxInsName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxInsName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxInsName.FormattingEnabled = true;
            this.comboBoxInsName.Location = new System.Drawing.Point(85, 87);
            this.comboBoxInsName.Name = "comboBoxInsName";
            this.comboBoxInsName.Size = new System.Drawing.Size(190, 21);
            this.comboBoxInsName.TabIndex = 3;
            this.comboBoxInsName.Leave += new System.EventHandler(this.comboBoxInsName_Leave);
            this.comboBoxInsName.SelectedValueChanged += new System.EventHandler(this.comboBoxInsName_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(303, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(335, 89);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(44, 20);
            this.textBoxPort.TabIndex = 5;
            this.textBoxPort.Text = "49152";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "SCPI file";
            // 
            // comboBoxFileName
            // 
            this.comboBoxFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxFileName.FormattingEnabled = true;
            this.comboBoxFileName.Location = new System.Drawing.Point(85, 114);
            this.comboBoxFileName.Name = "comboBoxFileName";
            this.comboBoxFileName.Size = new System.Drawing.Size(329, 21);
            this.comboBoxFileName.TabIndex = 7;
            this.comboBoxFileName.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFileName_SelectionChangeCommitted);
            this.comboBoxFileName.Leave += new System.EventHandler(this.comboBoxFileName_Leave);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(434, 113);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(47, 21);
            this.buttonBrowse.TabIndex = 8;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonBrowse);
            this.panelMain.Controls.Add(this.comboBoxFileName);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.textBoxPort);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.comboBoxInsName);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Location = new System.Drawing.Point(0, 1);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(655, 144);
            this.panelMain.TabIndex = 9;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.Location = new System.Drawing.Point(64, 3);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(47, 50);
            this.buttonOpen.TabIndex = 7;
            this.buttonOpen.UseVisualStyleBackColor = true;
            // 
            // formScpiGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 481);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.tabControl);
            this.Name = "formScpiGUI";
            this.Text = "ScpiTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formScpiGUI_FormClosing);
            this.Resize += new System.EventHandler(this.ScpiGUI_Resize);
            this.panel1.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttostart;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInsName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxFileName;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonNewFile;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonCloseFile;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonOpen;
    }
}

