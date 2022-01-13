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
            this.fileslist = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorlbl = new System.Windows.Forms.Label();
            this.convertbtn = new System.Windows.Forms.Button();
            this.namelbl = new System.Windows.Forms.Label();
            this.nametxb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.replacegroup = new System.Windows.Forms.GroupBox();
            this.filesreplace = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.replaceErrorMsg = new System.Windows.Forms.Label();
            this.replaceStartbtn = new System.Windows.Forms.Button();
            this.replacelbl = new System.Windows.Forms.Label();
            this.replacetxb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.fileTicker = new System.Windows.Forms.Timer(this.components);
            this.addongroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.replacegroup.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(557, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome to Vehicle Resource Creator";
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
            this.label2.Size = new System.Drawing.Size(359, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select your type of vehicle to convert";
            // 
            // addongroup
            // 
            this.addongroup.Controls.Add(this.fileslist);
            this.addongroup.Controls.Add(this.panel1);
            this.addongroup.Controls.Add(this.label3);
            this.addongroup.Location = new System.Drawing.Point(13, 117);
            this.addongroup.Name = "addongroup";
            this.addongroup.Size = new System.Drawing.Size(556, 218);
            this.addongroup.TabIndex = 4;
            this.addongroup.TabStop = false;
            this.addongroup.Text = "Addon Vehicles";
            // 
            // fileslist
            // 
            this.fileslist.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileslist.FormattingEnabled = true;
            this.fileslist.ItemHeight = 21;
            this.fileslist.Location = new System.Drawing.Point(13, 42);
            this.fileslist.Name = "fileslist";
            this.fileslist.Size = new System.Drawing.Size(197, 151);
            this.fileslist.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.errorlbl);
            this.panel1.Controls.Add(this.convertbtn);
            this.panel1.Controls.Add(this.namelbl);
            this.panel1.Controls.Add(this.nametxb);
            this.panel1.Location = new System.Drawing.Point(219, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 145);
            this.panel1.TabIndex = 6;
            // 
            // errorlbl
            // 
            this.errorlbl.BackColor = System.Drawing.Color.Transparent;
            this.errorlbl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.errorlbl.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorlbl.ForeColor = System.Drawing.Color.Red;
            this.errorlbl.Location = new System.Drawing.Point(0, 124);
            this.errorlbl.Name = "errorlbl";
            this.errorlbl.Size = new System.Drawing.Size(327, 21);
            this.errorlbl.TabIndex = 5;
            this.errorlbl.Text = "error msg";
            this.errorlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.errorlbl.Visible = false;
            // 
            // convertbtn
            // 
            this.convertbtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convertbtn.Location = new System.Drawing.Point(15, 85);
            this.convertbtn.Name = "convertbtn";
            this.convertbtn.Size = new System.Drawing.Size(298, 40);
            this.convertbtn.TabIndex = 4;
            this.convertbtn.Text = "Start Converting";
            this.convertbtn.UseVisualStyleBackColor = true;
            this.convertbtn.Click += new System.EventHandler(this.convertbtn_Click);
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
            // nametxb
            // 
            this.nametxb.Enabled = false;
            this.nametxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nametxb.Location = new System.Drawing.Point(15, 40);
            this.nametxb.Name = "nametxb";
            this.nametxb.Size = new System.Drawing.Size(298, 29);
            this.nametxb.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Files to Convert:";
            // 
            // replacegroup
            // 
            this.replacegroup.Controls.Add(this.filesreplace);
            this.replacegroup.Controls.Add(this.panel2);
            this.replacegroup.Controls.Add(this.label6);
            this.replacegroup.Location = new System.Drawing.Point(14, 117);
            this.replacegroup.Name = "replacegroup";
            this.replacegroup.Size = new System.Drawing.Size(570, 228);
            this.replacegroup.TabIndex = 8;
            this.replacegroup.TabStop = false;
            this.replacegroup.Text = "Replace Vehicles";
            this.replacegroup.Visible = false;
            // 
            // filesreplace
            // 
            this.filesreplace.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filesreplace.FormattingEnabled = true;
            this.filesreplace.ItemHeight = 21;
            this.filesreplace.Location = new System.Drawing.Point(13, 42);
            this.filesreplace.Name = "filesreplace";
            this.filesreplace.Size = new System.Drawing.Size(197, 151);
            this.filesreplace.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.replaceErrorMsg);
            this.panel2.Controls.Add(this.replaceStartbtn);
            this.panel2.Controls.Add(this.replacelbl);
            this.panel2.Controls.Add(this.replacetxb);
            this.panel2.Location = new System.Drawing.Point(219, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(327, 145);
            this.panel2.TabIndex = 6;
            // 
            // replaceErrorMsg
            // 
            this.replaceErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.replaceErrorMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.replaceErrorMsg.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.replaceErrorMsg.Location = new System.Drawing.Point(0, 124);
            this.replaceErrorMsg.Name = "replaceErrorMsg";
            this.replaceErrorMsg.Size = new System.Drawing.Size(327, 21);
            this.replaceErrorMsg.TabIndex = 5;
            this.replaceErrorMsg.Text = "error msg";
            this.replaceErrorMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.replaceErrorMsg.Visible = false;
            // 
            // replaceStartbtn
            // 
            this.replaceStartbtn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceStartbtn.Location = new System.Drawing.Point(15, 85);
            this.replaceStartbtn.Name = "replaceStartbtn";
            this.replaceStartbtn.Size = new System.Drawing.Size(298, 40);
            this.replaceStartbtn.TabIndex = 4;
            this.replaceStartbtn.Text = "Start Converting";
            this.replaceStartbtn.UseVisualStyleBackColor = true;
            this.replaceStartbtn.Click += new System.EventHandler(this.replaceStartbtn_Click);
            // 
            // replacelbl
            // 
            this.replacelbl.AutoSize = true;
            this.replacelbl.Enabled = false;
            this.replacelbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replacelbl.Location = new System.Drawing.Point(11, 16);
            this.replacelbl.Name = "replacelbl";
            this.replacelbl.Size = new System.Drawing.Size(153, 19);
            this.replacelbl.TabIndex = 3;
            this.replacelbl.Text = "Input vehicle name:";
            // 
            // replacetxb
            // 
            this.replacetxb.Enabled = false;
            this.replacetxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replacetxb.Location = new System.Drawing.Point(15, 40);
            this.replacetxb.Name = "replacetxb";
            this.replacetxb.Size = new System.Drawing.Size(298, 29);
            this.replacetxb.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Files to Convert:";
            // 
            // fileTicker
            // 
            this.fileTicker.Interval = 500;
            this.fileTicker.Tick += new System.EventHandler(this.fileTicker_Tick);
            // 
            // ResourceCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 339);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.replacebtn);
            this.Controls.Add(this.addonbtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.replacegroup);
            this.Controls.Add(this.addongroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ResourceCreator";
            this.Text = "Resource Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.addongroup.ResumeLayout(false);
            this.addongroup.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.replacegroup.ResumeLayout(false);
            this.replacegroup.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer fileTicker;
        private System.Windows.Forms.ListBox fileslist;
        private System.Windows.Forms.GroupBox replacegroup;
        private System.Windows.Forms.ListBox filesreplace;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label replaceErrorMsg;
        private System.Windows.Forms.Button replaceStartbtn;
        private System.Windows.Forms.Label replacelbl;
        private System.Windows.Forms.TextBox replacetxb;
        private System.Windows.Forms.Label label6;
    }
}

