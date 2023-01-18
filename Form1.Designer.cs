namespace ResourceCreatorv2
{
    partial class ResourceCreator
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.addonbtn = new System.Windows.Forms.Button();
            this.replacebtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.addongroup = new System.Windows.Forms.GroupBox();
            this.startbtn = new System.Windows.Forms.Button();
            this.fileslist = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.logpanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.logtxtbx = new System.Windows.Forms.TextBox();
            this.inputpanel = new System.Windows.Forms.Panel();
            this.convertbtn = new System.Windows.Forms.Button();
            this.browseBtn = new System.Windows.Forms.Button();
            this.errorlbl = new System.Windows.Forms.Label();
            this.namelbl = new System.Windows.Forms.Label();
            this.rootDirtxb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nametxb = new System.Windows.Forms.TextBox();
            this.fileTicker = new System.Windows.Forms.Timer(this.components);
            this.enginebtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.modkitbtn = new System.Windows.Forms.Button();
            this.resizerbtn = new System.Windows.Forms.Button();
            this.addongroup.SuspendLayout();
            this.logpanel.SuspendLayout();
            this.inputpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(542, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to FiveM Resource Creator";
            // 
            // addonbtn
            // 
            this.addonbtn.Location = new System.Drawing.Point(12, 71);
            this.addonbtn.Name = "addonbtn";
            this.addonbtn.Size = new System.Drawing.Size(110, 33);
            this.addonbtn.TabIndex = 1;
            this.addonbtn.Text = "Addon Vehicle";
            this.addonbtn.UseVisualStyleBackColor = true;
            this.addonbtn.Click += new System.EventHandler(this.addonbtn_Click);
            // 
            // replacebtn
            // 
            this.replacebtn.Location = new System.Drawing.Point(128, 71);
            this.replacebtn.Name = "replacebtn";
            this.replacebtn.Size = new System.Drawing.Size(120, 33);
            this.replacebtn.TabIndex = 2;
            this.replacebtn.Text = "Replace Vehicle";
            this.replacebtn.UseVisualStyleBackColor = true;
            this.replacebtn.Click += new System.EventHandler(this.replacebtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select your type of conversion";
            // 
            // addongroup
            // 
            this.addongroup.Controls.Add(this.startbtn);
            this.addongroup.Controls.Add(this.fileslist);
            this.addongroup.Controls.Add(this.label3);
            this.addongroup.Controls.Add(this.inputpanel);
            this.addongroup.Controls.Add(this.logpanel);
            this.addongroup.Location = new System.Drawing.Point(13, 117);
            this.addongroup.Name = "addongroup";
            this.addongroup.Size = new System.Drawing.Size(556, 256);
            this.addongroup.TabIndex = 4;
            this.addongroup.TabStop = false;
            this.addongroup.Text = "Addon Vehicles";
            // 
            // startbtn
            // 
            this.startbtn.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startbtn.Location = new System.Drawing.Point(5, 199);
            this.startbtn.Name = "startbtn";
            this.startbtn.Size = new System.Drawing.Size(212, 40);
            this.startbtn.TabIndex = 9;
            this.startbtn.Text = "Start Converting";
            this.startbtn.UseVisualStyleBackColor = true;
            this.startbtn.Click += new System.EventHandler(this.startbtn_Click);
            // 
            // fileslist
            // 
            this.fileslist.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileslist.FormattingEnabled = true;
            this.fileslist.ItemHeight = 21;
            this.fileslist.Location = new System.Drawing.Point(6, 42);
            this.fileslist.Name = "fileslist";
            this.fileslist.Size = new System.Drawing.Size(211, 151);
            this.fileslist.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Files to Convert:";
            // 
            // logpanel
            // 
            this.logpanel.Controls.Add(this.label5);
            this.logpanel.Controls.Add(this.logtxtbx);
            this.logpanel.Location = new System.Drawing.Point(219, 14);
            this.logpanel.Name = "logpanel";
            this.logpanel.Size = new System.Drawing.Size(337, 236);
            this.logpanel.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "Log:";
            // 
            // logtxtbx
            // 
            this.logtxtbx.Enabled = false;
            this.logtxtbx.Location = new System.Drawing.Point(18, 27);
            this.logtxtbx.Multiline = true;
            this.logtxtbx.Name = "logtxtbx";
            this.logtxtbx.Size = new System.Drawing.Size(301, 205);
            this.logtxtbx.TabIndex = 6;
            // 
            // inputpanel
            // 
            this.inputpanel.Controls.Add(this.convertbtn);
            this.inputpanel.Controls.Add(this.browseBtn);
            this.inputpanel.Controls.Add(this.errorlbl);
            this.inputpanel.Controls.Add(this.namelbl);
            this.inputpanel.Controls.Add(this.rootDirtxb);
            this.inputpanel.Controls.Add(this.label4);
            this.inputpanel.Controls.Add(this.nametxb);
            this.inputpanel.Location = new System.Drawing.Point(219, 25);
            this.inputpanel.Name = "inputpanel";
            this.inputpanel.Size = new System.Drawing.Size(327, 225);
            this.inputpanel.TabIndex = 6;
            this.inputpanel.Visible = false;
            // 
            // convertbtn
            // 
            this.convertbtn.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convertbtn.Location = new System.Drawing.Point(70, 75);
            this.convertbtn.Name = "convertbtn";
            this.convertbtn.Size = new System.Drawing.Size(175, 40);
            this.convertbtn.TabIndex = 4;
            this.convertbtn.Text = "Apply";
            this.convertbtn.UseVisualStyleBackColor = true;
            this.convertbtn.Click += new System.EventHandler(this.convertbtn_Click);
            // 
            // browseBtn
            // 
            this.browseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseBtn.Location = new System.Drawing.Point(251, 142);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(68, 29);
            this.browseBtn.TabIndex = 8;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Visible = false;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // errorlbl
            // 
            this.errorlbl.BackColor = System.Drawing.Color.Transparent;
            this.errorlbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.errorlbl.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorlbl.ForeColor = System.Drawing.Color.Red;
            this.errorlbl.Location = new System.Drawing.Point(0, 204);
            this.errorlbl.Name = "errorlbl";
            this.errorlbl.Size = new System.Drawing.Size(327, 21);
            this.errorlbl.TabIndex = 5;
            this.errorlbl.Text = "error msg";
            this.errorlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.errorlbl.Visible = false;
            // 
            // namelbl
            // 
            this.namelbl.AutoSize = true;
            this.namelbl.Enabled = false;
            this.namelbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.Location = new System.Drawing.Point(11, 16);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(153, 19);
            this.namelbl.TabIndex = 3;
            this.namelbl.Text = "Input vehicle name:";
            // 
            // rootDirtxb
            // 
            this.rootDirtxb.Enabled = false;
            this.rootDirtxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rootDirtxb.Location = new System.Drawing.Point(15, 144);
            this.rootDirtxb.Multiline = true;
            this.rootDirtxb.Name = "rootDirtxb";
            this.rootDirtxb.Size = new System.Drawing.Size(230, 24);
            this.rootDirtxb.TabIndex = 6;
            this.rootDirtxb.Visible = false;
            this.rootDirtxb.TextChanged += new System.EventHandler(this.rootDirtxb_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Vehicles Root Directory:";
            this.label4.Visible = false;
            // 
            // nametxb
            // 
            this.nametxb.Enabled = false;
            this.nametxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nametxb.Location = new System.Drawing.Point(15, 40);
            this.nametxb.Name = "nametxb";
            this.nametxb.Size = new System.Drawing.Size(298, 29);
            this.nametxb.TabIndex = 2;
            // 
            // fileTicker
            // 
            this.fileTicker.Interval = 500;
            this.fileTicker.Tick += new System.EventHandler(this.fileTicker_Tick);
            // 
            // enginebtn
            // 
            this.enginebtn.Location = new System.Drawing.Point(439, 35);
            this.enginebtn.Name = "enginebtn";
            this.enginebtn.Size = new System.Drawing.Size(120, 33);
            this.enginebtn.TabIndex = 5;
            this.enginebtn.Text = "Engine Audio";
            this.enginebtn.UseVisualStyleBackColor = true;
            this.enginebtn.Visible = false;
            this.enginebtn.Click += new System.EventHandler(this.enginebtn_Click);
            // 
            // modkitbtn
            // 
            this.modkitbtn.Location = new System.Drawing.Point(380, 71);
            this.modkitbtn.Name = "modkitbtn";
            this.modkitbtn.Size = new System.Drawing.Size(120, 33);
            this.modkitbtn.TabIndex = 6;
            this.modkitbtn.Text = "Modkit Fixer";
            this.modkitbtn.UseVisualStyleBackColor = true;
            this.modkitbtn.Visible = false;
            this.modkitbtn.Click += new System.EventHandler(this.modkitbtn_Click);
            // 
            // resizerbtn
            // 
            this.resizerbtn.Location = new System.Drawing.Point(254, 71);
            this.resizerbtn.Name = "resizerbtn";
            this.resizerbtn.Size = new System.Drawing.Size(120, 33);
            this.resizerbtn.TabIndex = 7;
            this.resizerbtn.Text = "Texture Resizer";
            this.resizerbtn.UseVisualStyleBackColor = true;
            this.resizerbtn.Click += new System.EventHandler(this.resizerbtn_Click);
            // 
            // ResourceCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 384);
            this.Controls.Add(this.resizerbtn);
            this.Controls.Add(this.modkitbtn);
            this.Controls.Add(this.enginebtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.replacebtn);
            this.Controls.Add(this.addonbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addongroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ResourceCreator";
            this.Text = "Resource Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.addongroup.ResumeLayout(false);
            this.addongroup.PerformLayout();
            this.logpanel.ResumeLayout(false);
            this.logpanel.PerformLayout();
            this.inputpanel.ResumeLayout(false);
            this.inputpanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addonbtn;
        private System.Windows.Forms.Button replacebtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox addongroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button convertbtn;
        private System.Windows.Forms.Label namelbl;
        private System.Windows.Forms.TextBox nametxb;
        private System.Windows.Forms.Label errorlbl;
        private System.Windows.Forms.Panel inputpanel;
        private System.Windows.Forms.Timer fileTicker;
        private System.Windows.Forms.ListBox fileslist;
        private System.Windows.Forms.Button enginebtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox rootDirtxb;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.Button modkitbtn;
        private System.Windows.Forms.Button resizerbtn;
        private System.Windows.Forms.Button startbtn;
        private System.Windows.Forms.Panel logpanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox logtxtbx;
    }
}

