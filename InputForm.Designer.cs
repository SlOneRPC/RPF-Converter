namespace ResourceCreatorv2
{
    partial class InputForm
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
            this.cancelbtn = new System.Windows.Forms.Button();
            this.namelbl = new System.Windows.Forms.Label();
            this.nametxb = new System.Windows.Forms.TextBox();
            this.okbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelbtn
            // 
            this.cancelbtn.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbtn.Location = new System.Drawing.Point(12, 70);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(175, 40);
            this.cancelbtn.TabIndex = 7;
            this.cancelbtn.Text = "Cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // namelbl
            // 
            this.namelbl.AutoSize = true;
            this.namelbl.Enabled = false;
            this.namelbl.Font = new System.Drawing.Font("Rockwell", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.Location = new System.Drawing.Point(12, 13);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(153, 19);
            this.namelbl.TabIndex = 6;
            this.namelbl.Text = "Input vehicle name:";
            // 
            // nametxb
            // 
            this.nametxb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nametxb.Location = new System.Drawing.Point(12, 35);
            this.nametxb.Name = "nametxb";
            this.nametxb.Size = new System.Drawing.Size(356, 29);
            this.nametxb.TabIndex = 5;
            // 
            // okbtn
            // 
            this.okbtn.Font = new System.Drawing.Font("Rockwell", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okbtn.Location = new System.Drawing.Point(193, 70);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(175, 40);
            this.okbtn.TabIndex = 8;
            this.okbtn.Text = "Ok";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 117);
            this.Controls.Add(this.okbtn);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.namelbl);
            this.Controls.Add(this.nametxb);
            this.Name = "InputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelbtn;
        private System.Windows.Forms.Label namelbl;
        private System.Windows.Forms.TextBox nametxb;
        private System.Windows.Forms.Button okbtn;
    }
}