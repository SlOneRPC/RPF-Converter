using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace ResourceCreatorv2
{
    public partial class ResourceCreator : Form
    {
        static bool converting1 = false;
        static bool converting2 = false;
        static string input = null;

        public ResourceCreator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileTicker.Interval = 1000;
            fileTicker.Start();
        }

        public string requestInput(string msg)
        {
            if (addongroup.Visible)
            {
                namelbl.Invoke(new MethodInvoker(delegate
                {
                    namelbl.Enabled = true;
                    namelbl.Text = msg;

                    nametxb.Enabled = true;
                    nametxb.Text = "";

                    convertbtn.Enabled = true;
                    convertbtn.Text = "Next";
                }));
            }
            else
            {
                replacelbl.Invoke(new MethodInvoker(delegate
                {
                    replacelbl.Enabled = true;
                    replacelbl.Text = msg;

                    replacetxb.Enabled = true;
                    replacetxb.Text = "";

                    replaceStartbtn.Enabled = true;
                    replaceStartbtn.Text = "Next";
                }));
            }

            input = null;
            while (input == null)
            {
                Thread.Sleep(100);
            }

            return input;
        }

        public void convertComplete()
        {
            if (addongroup.Visible)
            {
                namelbl.Invoke(new MethodInvoker(delegate
                {
                    namelbl.Enabled = false;
                    namelbl.Text = $"Input vehicle name:";

                    nametxb.Enabled = false;
                    nametxb.Text = "";

                    convertbtn.Enabled = true;
                    convertbtn.Text = "Start Converting";
                }));
                converting1 = false;
            }
            else
            {
                replacelbl.Invoke(new MethodInvoker(delegate
                {
                    replacelbl.Enabled = false;
                    replacelbl.Text = $"Input vehicle name:";

                    replacetxb.Enabled = false;
                    replacetxb.Text = "";

                    replaceStartbtn.Enabled = true;
                    replaceStartbtn.Text = "Start Converting";
                }));
                converting2 = false;
            }
        }

        public void errorMsg(string message)
        {
            if (addongroup.Visible)
            {
                errorlbl.Invoke(new MethodInvoker(delegate
                {
                    errorlbl.Text = message;
                    errorlbl.Visible = true;
                }));
            }
            else
            {
                errorlbl.Invoke(new MethodInvoker(delegate
                {
                    replaceErrorMsg.Text = message;
                    replaceErrorMsg.Visible = true;
                }));
            }
        }

        private void convertbtn_Click(object sender, EventArgs e)
        {
            if (!converting1)
            {
                converting1 = true;
                errorlbl.Visible = false;
                Thread newThread = new Thread(AddonGenerator.start);
                newThread.Start(this);
            }
            else
            {
                input = nametxb.Text;
                nametxb.Enabled = false;
                convertbtn.Enabled = false;
            }
            convertbtn.Text = "Converting..";
        }

        private void fileTicker_Tick(object sender, EventArgs e)
        {
            if (addongroup.Visible)
            {
                fileslist.Items.Clear();

                Utils.ProcessDirectory("./input", (file, obj) =>
                {
                    if (file.EndsWith(".rpf"))
                    {
                        ((ListBox)obj).Items.Add(file.Replace("./input\\", ""));
                    }
                }, fileslist);
            }
            else
            {
                filesreplace.Items.Clear();

                Utils.ProcessDirectory("./input", (file, obj) =>
                {
                    if (file.EndsWith(".ytd") || file.EndsWith(".yft"))
                    {
                        ((ListBox)obj).Items.Add(file.Replace("./input\\", ""));
                    }
                }, filesreplace);
            }
        }

        private void addonbtn_Click(object sender, EventArgs e)
        {
            replacegroup.Visible = false;
            addongroup.Visible = true;
        }

        private void replacebtn_Click(object sender, EventArgs e)
        {
            addongroup.Visible = false;
            replacegroup.Visible = true;
        }

        private void replaceStartbtn_Click(object sender, EventArgs e)
        {
            if (!converting2)
            {
                converting2 = true;
                replaceErrorMsg.Visible = false;
                Thread newThread = new Thread(ReplaceGenerator.Start);
                newThread.Start(this);
            }
            else
            {
                input = replacetxb.Text;
                replacetxb.Enabled = false;
                replaceStartbtn.Enabled = false;
            }
            replaceStartbtn.Text = "Converting..";
        }
    }
}
