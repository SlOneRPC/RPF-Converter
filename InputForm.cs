using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResourceCreatorv2
{
    public partial class InputForm : Form
    {
        public string output { get; set; }

        public InputForm(string msg, string defaultValue)
        {
            InitializeComponent();

            namelbl.Text = msg;
            nametxb.Text = defaultValue;
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            output = nametxb.Text;
            this.Close();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            output = nametxb.Text;
            this.Close();
        }
    }
}
