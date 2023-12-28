using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ResourceCreatorv2.Misc;
using static ResourceCreatorv2.Logger;
namespace ResourceCreatorv2
{
    public class ModkitFixer
    {
        public static ResourceCreator convertForm;

        private const int MAX_MODKIT_ID = 75000; // used to be max 8 bit value

        private static int currentID = 1500;

        private static void ProcessFile(string path, string currentValue, List<string> ids)
        {
            for (int i = currentID; i <= MAX_MODKIT_ID; i++)
            {
                if (!ids.Contains(i.ToString()))
                {
                    string[] lines = File.ReadAllLines(path);

                    for (int lineID = 0; lineID < lines.Length; lineID++)
                    {
                        string line = lines[lineID];
                        if (line.Contains("<kitName"))
                        {
                            lines[lineID] = line.Replace(currentValue, i.ToString());
                        }
                        else if (line.Contains("<id") && line.Contains("value"))
                        {
                            lines[lineID] = line.Replace(currentValue, i.ToString());

                            File.WriteAllLines(path, lines);
                            break;
                        }
                    }

                    string[] pathSplit = path.Split('\\');
                    string carvariationsPath = path.Replace(pathSplit[pathSplit.Length - 1], "carvariations.meta");

                    if (File.Exists(carvariationsPath))
                    {
                        string[] carvariationsLines = File.ReadAllLines(carvariationsPath);

                        for (int lineID = 0; lineID < carvariationsLines.Length; lineID++)
                        {
                            string line = carvariationsLines[lineID];
                            if (line.Contains("<Item>") && line.Contains("modkit"))
                            {
                                carvariationsLines[lineID] = line.Replace(currentValue, i.ToString());
                                File.WriteAllLines(carvariationsPath, carvariationsLines);
                                break;
                            }
                        }
                    }
                    else
                    {
                        LogError("Can't find carvariations !!");
                    }

                    LogInfo("Updated -> " + pathSplit[pathSplit.Length - 2]);
                    currentID = i + 12;

                    return;
                }
            }
        }

        public static void Start(object form)
        {
            convertForm = (ResourceCreator)form;
            currentID = 1500;

            dupInfo dups = Misc.checkRootForDuplicateModKit(convertForm.getRootDir());

            List<string> ids = dups.dupIds.Keys.ToList();

            foreach (KeyValuePair<string, List<string>> id in dups.dupIds)
            {
                if (id.Value.Count > 1)
                {
                    foreach (string path in id.Value)
                    {
                        // We don't need to update the first one as this id can be used once
                        if (path == id.Value[0])
                            continue;

                        ProcessFile(path, id.Key, ids);
                    }
                }
            }

            convertForm.convertComplete();
        }
    }
}
