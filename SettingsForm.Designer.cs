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
            this.groupBox1.Location = new System.Drawing.Point(29, 183);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(743, 84);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resizing Settings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 25);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(314, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Min acceptable texture (image) memory usage (MB)";
            // 
            // minAcceptableTextureMem
            // 
            this.minAcceptableTextureMem.DecimalPlaces = 1;
            this.minAcceptableTextureMem.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.minAcceptableTextureMem.Location = new System.Drawing.Point(292, 48);
            this.minAcceptableTextureMem.Margin = new System.Windows.Forms.Padding(4);
            this.minAcceptableTextureMem.Name = "minAcceptableTextureMem";
            this.minAcceptableTextureMem.Size = new System.Drawing.Size(152, 22);
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
            this.label6.Location = new System.Drawing.Point(13, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Min uncompressed file size to resize (MB)";
            // 
            // minFileResizeSel
            // 
            this.minFileResizeSel.DecimalPlaces = 1;
            this.minFileResizeSel.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.minFileResizeSel.Location = new System.Drawing.Point(13, 48);
            this.minFileResizeSel.Margin = new System.Windows.Forms.Padding(4);
            this.minFileResizeSel.Name = "minFileResizeSel";
            this.minFileResizeSel.Size = new System.Drawing.Size(152, 22);
            this.minFileResizeSel.TabIndex = 0;
            this.minFileResizeSel.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minAcceptableTextureMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFileResizeSel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown minAcceptableTextureMem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown minFileResizeSel;
    }
}