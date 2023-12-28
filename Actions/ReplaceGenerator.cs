using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;
using CodeWalker.GameFiles;
using CodeWalker.Utils;
using System.Windows.Forms;
using static ResourceCreatorv2.Logger;
namespace ResourceCreatorv2
{
    public class ReplaceGenerator
    {
        public static ResourceCreator convertForm;

        static Dictionary<string, string> modelNames = new Dictionary<string, string>();

        private static void ProcessFileMove(string path)
        {
            if (!path.EndsWith(".ytd"))
            {
                return;
            }

            if (!Directory.Exists("./resource"))
            {
                Directory.CreateDirectory("./resource");
            }

            string[] divided = path.Split('\\');
            string modelName = "memed";
            string oldModelName = divided[divided.Length - 1].Replace(".ytd", "");

            string[] fileEntries = Directory.GetFiles(path.Replace(divided[divided.Length - 1], ""));
            foreach (string fileName in fileEntries)
            {
                AddonGenerator.PerformExtensionsIterations((type, extention) =>
                {
                    if (fileName.EndsWith(extention))
                    {
                        if (!modelNames.ContainsKey(modelName))
                        {
                            while (true)
                            {
                                modelName = convertForm.requestInput($"Input model name name for {oldModelName}:");

                                if (!Directory.Exists($"./resource\\meta\\{modelName}"))
                                {
                                    if (convertForm.getRootDir().Length < 1 || !Misc.checkRootForModelName(modelName, convertForm.getRootDir()))
                                        break;
                                    else
                                        convertForm.errorMsg("This model name is already in use in root dir!");
                                }
                                else
                                {
                                    convertForm.errorMsg("This model has already been converted!");
                                }
                            }

                            string vehicleName = convertForm.requestInput($"Input vehicle name for {modelName}:");
                            modelNames.Add(modelName, vehicleName);

                            if (Directory.Exists($"./resource\\meta\\{modelName}"))
                            {
                                Directory.Delete($"./resource\\meta\\{modelName}", true);
                            }

                            if (Directory.Exists($"./resource\\stream\\{modelName}"))
                            {
                                Directory.Delete($"./resource\\stream\\{modelName}", true);
                            }
                        }

                        if (!Directory.Exists($"./resource\\{type}\\{modelName}"))
                        {
                            Directory.CreateDirectory($"./resource\\{type}\\{modelName}");
                        }

                        string destination = $"./resource\\{type}\\{modelName}\\{fileName.Split('\\')[fileName.Split('\\').Length - 1]}";

                        if (fileName.EndsWith(".ytd") || fileName.EndsWith(".ydr"))
                        {
                            convertForm.resizer.ResizeFile(fileName);
                        }

                        Utils.MoveFile(fileName, destination.Replace(oldModelName, modelName));

                        return false;
                    }
                    return true;
                });
                

                if (File.Exists(fileName))
                {
                    LogWarning("Found non useful file '{0}'.", fileName);
                    File.Delete(fileName);
                }
            }

            // Handle meta files
            if (!Directory.Exists("./replace-files"))
            {
                MessageBox.Show("You are missing essential replace files located in the 'replace-files' directory, please redownload the app!");
                return;
            }

            if (!Directory.Exists("./resource\\meta"))
            {
                Directory.CreateDirectory("./resource\\meta");
            }

            string[] metaFileEntries = Directory.GetFiles("./replace-files");
            foreach (string fileName in metaFileEntries)
            {
                if (!Directory.Exists($"./resource\\meta\\{modelName}"))
                {
                    Directory.CreateDirectory($"./resource\\meta\\{modelName}");
                }

                string[] fileSplit = fileName.Split('\\');
                string[] lines = File.ReadAllLines(fileName);

                if (fileName.EndsWith("vehicles.meta")) 
                {
                    lines[6] = lines[6].Replace("<modelName>volare</modelName>", $"<modelName>{modelName}</modelName>");
                    lines[7] = lines[7].Replace("<txdName>volare</txdName>", $"<txdName>{modelName}</txdName>");
                    lines[8] = lines[8].Replace("<handlingId>VOLARE</handlingId>", $"<handlingId>{modelName}</handlingId>");
                    lines[9] = lines[9].Replace("<gameName>volare</gameName>", $"<gameName>{modelName}</gameName>");
                }
                else if (fileName.EndsWith("handling.meta"))
                {
                    lines[5] = lines[5].Replace("<handlingName>volare</handlingName>", $"<handlingName>{modelName}</handlingName>");
                }
                else if (fileName.EndsWith("carvariations.meta"))
                {
                    lines[5] = lines[5].Replace("<modelName>volare</modelName>", $"<modelName>{modelName}</modelName>");
                }
                else
                {
                    continue;
                }

                File.WriteAllLines($"./resource\\meta\\{modelName}\\{fileSplit[fileSplit.Length - 1]}", lines);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Moved file '{0}' -> {1}.", fileName, $"./resource\\meta\\{modelName}\\{fileSplit[fileSplit.Length - 1]}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void Start(object form)
        {
            convertForm = (ResourceCreator)form;

            LogInfo("Welcome to resource creator!");
            LogInfo("Checking resource...");

            // Check directories
            if (!Directory.Exists("./input"))
            {
                Directory.CreateDirectory("./input");
            }

            Utils.ClearDirectory("./rpf-extracted");

            Utils.ProcessDirectory("./input", (file, move) =>
            {
                ProcessFileMove(file);
            });

            if (modelNames.Count > 0)
            {
                Utils.WriteOutputFile(modelNames);
            }
            else
            {
                convertForm.errorMsg("There was nothing to convert!");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("congrats you made it!");
            Console.ForegroundColor = ConsoleColor.White;

            convertForm.convertComplete();
        }
    }
}
