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

            minFileResizeSel.Value = (decimal)ResourceCreator.minFileResize;
            minAcceptableTextureMem.Value = (decimal)ResourceCreator.minAcceptableTexureSize;
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

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResourceCreator.minAcceptableTexureSize = getMinAcceptableTextureMem();
            ResourceCreator.minFileResize = getMinFileResize();
        }
    }
}
