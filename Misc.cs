using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceCreatorv2
{
    public class Misc
    {
        public static bool checkRootForModelName(string modelName, string rootPath)
        {
            bool found = false;
            Utils.ProcessDirectory(rootPath, (file, obj) =>
            {
                if (file.EndsWith("vehicles.meta"))
                {
                    string[] lines = File.ReadAllLines(file);

                    foreach(string line in lines)
                    {
                        if (line.Contains("modelName"))
                        {
                            if (line.ToLower().Contains(modelName))
                            {
                                found = true;
                            }
                            return;
                        }
                    }

                }
            }, modelName);

            return found;
        }

        public struct dupInfo
        {
            public Dictionary<string, List<string>> dupIds;
        }

        public static dupInfo checkRootForDuplicateModKit(string rootPath)
        {
            Dictionary<string, List<string>> dups = new Dictionary<string, List<string>>();

            Utils.ProcessDirectory(rootPath, (file, obj) =>
            {
                if (file.EndsWith("carcols.meta"))
                {
                    string[] lines = File.ReadAllLines(file);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];

                        if (line.Contains("<id") && line.Contains("value") && lines[i - 1].Contains("kitName"))
                        {
                            int firstIndex = line.IndexOf('"') + 1;
                            string value = line.Substring(firstIndex, line.LastIndexOf('"') - firstIndex);

                            if (value == "0")
                            {
                                continue;
                            }

                            //value = BitConverter.GetBytes(int.Parse(value))[0].ToString(); // ids are stored as a byte by r* so convert it to 8 bits

                            Console.WriteLine("Found Modkit -> " + value);

                            if (dups.ContainsKey(value))
                            {
                                dups[value].Add(file);
                                continue;
                            }

                            dups.Add(value, new List<string>() { file });
                            continue;
                        }
                    }
                }
            });

            dupInfo info;
            info.dupIds = dups;

            return info;
        }
    }
}
