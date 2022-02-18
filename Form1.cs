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
using static ResourceCreatorv2.Misc;
using System.Xml.Serialization;

namespace ResourceCreatorv2
{
    public enum Mode
    {
        addon,
        replace,
        audio,
        modkit
    }
    public partial class ResourceCreator : Form
    {
        static bool converting = false;
        static string input = null;

        static Mode currentMode = Mode.addon;

        public ResourceCreator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileTicker.Interval = 1000;
            fileTicker.Start();

            if (File.Exists("config.xml"))
            {
                string rootDir = "";
                XmlSerializer ser = new XmlSerializer(typeof(string));
                using (FileStream fs = File.OpenRead("config.xml"))
                {
                    rootDir = (string)ser.Deserialize(fs);
                }

                rootDirtxb.Text = rootDir;
            }
        }

        public string getRootDir()
        {
            string text = null;
            rootDirtxb.Invoke(new MethodInvoker(delegate
            {
                text = rootDirtxb.Text;
            }));
            return text;
        }

        public string requestInput(string msg, string defaultValue = "")
        {
            namelbl.Invoke(new MethodInvoker(delegate
            {
                namelbl.Enabled = true;
                namelbl.Text = msg;

                nametxb.Enabled = true;
                nametxb.Text = defaultValue;

                convertbtn.Enabled = true;
                convertbtn.Text = "Next";
            }));
            
            input = null;
            while (input == null)
            {
                Thread.Sleep(100);
            }

            return input;
        }

        public void convertComplete()
        {
            namelbl.Invoke(new MethodInvoker(delegate
            {
                namelbl.Enabled = false;
                namelbl.Text = $"Input vehicle name:";

                nametxb.Enabled = false;
                nametxb.Text = "";

                convertbtn.Enabled = true;
                convertbtn.Text = "Start Converting";

                if (currentMode.Equals(Mode.modkit) && rootDirtxb.Text.Length > 1)
                {
                    fileslist.Items.Clear();
                    populateModKits(Misc.checkRootForDuplicateModKit(rootDirtxb.Text));
                }
            }));
            converting = false;
        }

        public void errorMsg(string message)
        {
            errorlbl.Invoke(new MethodInvoker(delegate
            {
                errorlbl.Text = message;
                errorlbl.Visible = true;
            }));
        }

        private void convertbtn_Click(object sender, EventArgs e)
        {
            if (!converting)
            {
                converting = true;
                errorlbl.Visible = false;

                if (currentMode.Equals(Mode.addon))
                {
                    Thread newThread = new Thread(AddonGenerator.start);
                    newThread.Start(this);
                }
                else if (currentMode.Equals(Mode.replace))
                {
                    Thread newThread = new Thread(ReplaceGenerator.Start);
                    newThread.Start(this);
                }
                else if (currentMode.Equals(Mode.modkit))
                {
                    Thread newThread = new Thread(ModkitFixer.Start);
                    newThread.Start(this);
                }
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
            if (!currentMode.Equals(Mode.modkit))
            {
                fileslist.Items.Clear();

                if (currentMode.Equals(Mode.addon))
                {


                    Utils.ProcessDirectory("./input", (file, obj) =>
                    {
                        if (file.EndsWith(".rpf"))
                        {
                            ((ListBox)obj).Items.Add(file.Replace("./input\\", ""));
                        }
                    }, fileslist);
                }
                else if (currentMode.Equals(Mode.replace))
                {
                    Utils.ProcessDirectory("./input", (file, obj) =>
                    {
                        if (file.EndsWith(".ytd") || file.EndsWith(".yft"))
                        {
                            ((ListBox)obj).Items.Add(file.Replace("./input\\", ""));
                        }
                    }, fileslist);
                }
            }
        }

        private void addonbtn_Click(object sender, EventArgs e)
        {
            if (!converting)
            {
                currentMode = Mode.addon;
                addongroup.Text = "Addon Vehicles";
            }
        }

        private void replacebtn_Click(object sender, EventArgs e)
        {
            if (!converting)
            {
                currentMode = Mode.replace;
                addongroup.Text = "Replace Vehicles";
            }
        }

        private void enginebtn_Click(object sender, EventArgs e)
        {
            if (!converting)
            {
                currentMode = Mode.audio;
                addongroup.Text = "Engine Audio";
            }
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (converting)
                return;

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    rootDirtxb.Text = fbd.SelectedPath;
                }
            }
        }


        void populateModKits(dupInfo dups)
        {
            foreach (KeyValuePair<string, List<string>> entry in dups.dupIds)
            {
                if (entry.Value.Count > 1)
                {
                    string duplicate = "";

                    foreach (string item in entry.Value)
                    {
                        string[] split = item.Split('\\');
                        string modelName = split[split.Length - 2];
                        duplicate += modelName + "|";
                    }

                    duplicate = duplicate.Remove(duplicate.Length - 1, 1); // remove trailing |

                    fileslist.Items.Add(duplicate + $"({entry.Key})");

                    Console.WriteLine(duplicate + $"({entry.Key})");
                }
            }
        }

        private void modkitbtn_Click(object sender, EventArgs e)
        {
            if (!converting)
            {
                currentMode = Mode.modkit;
                addongroup.Text = "Modkit Fixer";

                Thread t = new Thread(() =>
                {
                    fileslist.Invoke(new MethodInvoker(delegate
                    {
                        fileslist.Items.Clear();

                        if (rootDirtxb.Text.Length > 1)
                        {
                            dupInfo dups = Misc.checkRootForDuplicateModKit(rootDirtxb.Text);

                            populateModKits(dups);
                        }
                        else
                        {
                            errorMsg("Please set root dir!");
                        }
                    }));
                });
                t.Start();
            }
        }

        private void rootDirtxb_TextChanged(object sender, EventArgs e)
        {
            if (currentMode == Mode.modkit)
            {
                Thread t = new Thread(() =>
                {
                    fileslist.Invoke(new MethodInvoker(delegate
                    {
                        fileslist.Items.Clear();

                        if (rootDirtxb.Text.Length > 1)
                        {
                            dupInfo dups = Misc.checkRootForDuplicateModKit(rootDirtxb.Text);

                            populateModKits(dups);
                        }
                        else
                        {
                            errorMsg("Please set root dir!");
                        }
                    }));
                });
                t.Start();
            }

            using (StreamWriter sw = new StreamWriter("config.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(string));
                ser.Serialize(sw, rootDirtxb.Text);
            }
        }
    }
}
