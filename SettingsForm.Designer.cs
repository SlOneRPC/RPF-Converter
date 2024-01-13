namespace ResourceCreatorv2
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.minAcceptableTextureMem = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.minFileResizeSel = new System.Windows.Forms.NumericUpDown();
            this.browseBtn = new System.Windows.Forms.Button();
            this.rootDirtxb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minAcceptableTextureMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFileResizeSel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.minAcceptableTextureMem);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.minFileResizeSel);
            this.groupBox1.Location = new System.Drawing.Point(22, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 125);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resizing Settings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(272, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Minimum acceptable texture (image) memory usage (MB)";
            // 
            // minAcceptableTextureMem
            // 
            this.minAcceptableTextureMem.DecimalPlaces = 1;
            this.minAcceptableTextureMem.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.minAcceptableTextureMem.Location = new System.Drawing.Point(13, 81);
            this.minAcceptableTextureMem.Name = "minAcceptableTextureMem";
            this.minAcceptableTextureMem.Size = new System.Drawing.Size(114, 20);
            this.minAcceptableTextureMem.TabIndex = 2;
            this.minAcceptableTextureMem.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Minimum uncompressed file size to resize (MB)";
            // 
            // minFileResizeSel
            // 
            this.minFileResizeSel.DecimalPlaces = 1;
            this.minFileResizeSel.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.minFileResizeSel.Location = new System.Drawing.Point(10, 39);
            this.minFileResizeSel.Name = "minFileResizeSel";
            this.minFileResizeSel.Size = new System.Drawing.Size(114, 20);
            this.minFileResizeSel.TabIndex = 0;
            this.minFileResizeSel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // browseBtn
            // 
            this.browseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseBtn.Location = new System.Drawing.Point(493, 43);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(68, 29);
            this.browseBtn.TabIndex = 12;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click_1);
            // 
            // rootDirtxb
            // 
            this.rootDirtxb.Enabled = false;
            this.rootDirtxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rootDirtxb.Location = new System.Drawing.Point(22, 43);
            this.rootDirtxb.Multiline = true;
            this.rootDirtxb.Name = "rootDirtxb";
            this.rootDirtxb.Size = new System.Drawing.Size(465, 29);
            this.rootDirtxb.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "Vehicles Root Directory:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 216);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.rootDirtxb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minAcceptableTextureMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFileResizeSel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown minAcceptableTextureMem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown minFileResizeSel;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox rootDirtxb;
        private System.Windows.Forms.Label label4;
    }
}