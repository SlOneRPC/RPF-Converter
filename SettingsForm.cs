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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public string getRootDir()
        {
            return rootDirtxb.Text;
        }

        public float getMinFileResize()
        {
            return (float)minFileResizeSel.Value;
        }

        public float getMinAcceptableTextureMem()
        {
            return (float)minAcceptableTextureMem.Value;
        }

        private void browseBtn_Click_1(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    rootDirtxb.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
