using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        modkit,
        resizer
    }

    public partial class ResourceCreator : Form
    {
        public static float minFileResize = 10;
        public static float minAcceptableTexureSize = 5;

        static bool converting = false;
        static string input = null;

        static Mode currentMode = Mode.addon;
        public SettingsForm settingForm;

        public ResizeUtils resizer { get; set; }

        public ResourceCreator()
        {
            InitializeComponent();
            settingForm = new SettingsForm();
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileTicker.Interval = 1000;
            fileTicker.Start();
        }

        public string getRootDir()
        {
            string output = null;
            addonbtn.Invoke(new MethodInvoker(delegate
            {
                output = settingForm.getRootDir();
            }));
            return output;
        }

        public float getMinFileResize()
        {
            return minFileResize;
        }

        public float getMinAcceptableTextureMem()
        {
            return minAcceptableTexureSize;
        }

        public string requestInput(string msg, string defaultValue = "")
        {
            InputForm input = new InputForm(msg, defaultValue);
            input.ShowDialog();


            //input = null;
            //while (input == null)
            //{
            //    Thread.Sleep(100);
            //}

            return input.output;
        }

        public void convertComplete()
        {
            startbtn.Invoke(new MethodInvoker(delegate
            {
                startbtn.Enabled = true;
                startbtn.Text = "Start Converting";

                if (currentMode.Equals(Mode.modkit) && getRootDir().Length > 1)
                {
                    fileslist.Items.Clear();
                    populateModKits(checkRootForDuplicateModKit(getRootDir()));
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

        private void fileTicker_Tick(object sender, EventArgs e)
        {
            List<string> fileToPreview = new List<string>();

            switch (currentMode)
            {
                case Mode.addon:
                    fileToPreview.Add(".rpf");
                    break;
                case Mode.replace:
                    fileToPreview.Add(".yft");
                    goto case Mode.resizer;
                case Mode.resizer:
                    fileToPreview.Add(".ytd");
                    fileToPreview.Add(".ydr");
                    break;
                default:
                    break;
            }

            if (fileToPreview.Count > 0)
            {
                fileslist.Items.Clear();

                Utils.ProcessDirectory("./input", (file, obj) =>
                {
                    foreach (string preview in fileToPreview)
                    {
                        if (file.EndsWith(preview))
                        {
                            ((ListBox)obj).Items.Add(file.Replace("./input\\", ""));
                        }
                    }
                }, fileslist);
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

        private void resizerbtn_Click(object sender, EventArgs e)
        {
            if (!converting)
            {
                currentMode = Mode.resizer;
                addongroup.Text = resizerbtn.Text;
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

                        if (getRootDir().Length > 1)
                        {
                            dupInfo dups = Misc.checkRootForDuplicateModKit(getRootDir());

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

                        if (getRootDir().Length > 1)
                        {
                            dupInfo dups = Misc.checkRootForDuplicateModKit(getRootDir());

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
                ser.Serialize(sw, getRootDir());
            }
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            if (!converting)
            {
                converting = true;
                errorlbl.Visible = false;

                resizer = new ResizeUtils(getMinFileResize(), getMinAcceptableTextureMem());

                startbtn.Text = "Converting..";
                startbtn.Enabled = false;

                Thread newThread;

                if (currentMode.Equals(Mode.addon))
                    newThread = new Thread(AddonGenerator.start);
                else if (currentMode.Equals(Mode.replace))
                    newThread = new Thread(ReplaceGenerator.Start);
                else if (currentMode.Equals(Mode.modkit))
                    newThread = new Thread(ModkitFixer.Start);
                else if (currentMode.Equals(Mode.resizer))
                    newThread = new Thread(TextureResizer.Start);
                else return;

                newThread.Start(this);
            }
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            settingForm.ShowDialog();
            settingForm = new SettingsForm();
        }
    }
}
