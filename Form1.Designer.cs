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
            this.errorlbl = new System.Windows.Forms.Label();
            this.settingsbtn = new System.Windows.Forms.Button();
            this.startbtn = new System.Windows.Forms.Button();
            this.fileslist = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fileTicker = new System.Windows.Forms.Timer(this.components);
            this.enginebtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.modkitbtn = new System.Windows.Forms.Button();
            this.resizerbtn = new System.Windows.Forms.Button();
            this.addongroup.SuspendLayout();
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
            this.addongroup.Controls.Add(this.errorlbl);
            this.addongroup.Controls.Add(this.settingsbtn);
            this.addongroup.Controls.Add(this.startbtn);
            this.addongroup.Controls.Add(this.fileslist);
            this.addongroup.Controls.Add(this.label3);
            this.addongroup.Location = new System.Drawing.Point(13, 117);
            this.addongroup.Name = "addongroup";
            this.addongroup.Size = new System.Drawing.Size(556, 256);
            this.addongroup.TabIndex = 4;
            this.addongroup.TabStop = false;
            this.addongroup.Text = "Addon Vehicles";
            // 
            // errorlbl
            // 
            this.errorlbl.AutoSize = true;
            this.errorlbl.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorlbl.ForeColor = System.Drawing.Color.Red;
            this.errorlbl.Location = new System.Drawing.Point(223, 210);
            this.errorlbl.Name = "errorlbl";
            this.errorlbl.Size = new System.Drawing.Size(57, 21);
            this.errorlbl.TabIndex = 8;
            this.errorlbl.Text = "Error";
            this.errorlbl.Visible = false;
            // 
            // settingsbtn
            // 
            this.settingsbtn.Font = new System.Drawing.Font("Rockwell", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsbtn.Location = new System.Drawing.Point(412, 199);
            this.settingsbtn.Name = "settingsbtn";
            this.settingsbtn.Size = new System.Drawing.Size(115, 40);
            this.settingsbtn.TabIndex = 10;
            this.settingsbtn.Text = "🔧";
            this.settingsbtn.UseVisualStyleBackColor = true;
            this.settingsbtn.Click += new System.EventHandler(this.settingsbtn_Click);
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
            this.fileslist.Size = new System.Drawing.Size(521, 151);
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
            this.ClientSize = new System.Drawing.Size(577, 375);
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
        private System.Windows.Forms.Timer fileTicker;
        private System.Windows.Forms.ListBox fileslist;
        private System.Windows.Forms.Button enginebtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button modkitbtn;
        private System.Windows.Forms.Button resizerbtn;
        private System.Windows.Forms.Button startbtn;
        private System.Windows.Forms.Button settingsbtn;
        private System.Windows.Forms.Label errorlbl;
    }
}

