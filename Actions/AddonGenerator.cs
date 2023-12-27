using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading;
using CodeWalker.GameFiles;
using CodeWalker.Utils;
namespace ResourceCreatorv2
{
    public class AddonGenerator
    {
        public static Dictionary<string, string[]> extensions = new Dictionary<string, string[]>()
        {
            { "meta",  new string[]{ ".meta", "clip_sets.xml" } },
            { "stream", new string[]{".ytd", ".yft", ".ydr" } }
        };

        static Dictionary<string, string> modelNames = new Dictionary<string, string>();

        static string latestModelName = "";

        public static ResourceCreator convertForm;

        // Model name replacement stuff
        private static bool requiresModelRename = false;
        private static string oldModelName = "";
        private static string[] metasToReplace = new string[] 
        { 
            "vehicles.meta", "handling.meta", "carvariations.meta", "carcols.meta" 
        };

        static string CheckForModelRename()
        {
            while (true)
            {
                string updatedName = convertForm.requestInput($"Change model or press next:", latestModelName);

                if (!Directory.Exists($"./resource\\meta\\{updatedName}"))
                {
                    if (convertForm.getRootDir().Length < 1 || !Misc.checkRootForModelName(updatedName, convertForm.getRootDir()))
                        return updatedName;
                    else
                        convertForm.errorMsg("This model name is already in use in root dir!");
                }
                else
                {
                    convertForm.errorMsg("This model has already been converted!");
                }
            }
        }

        static string ProcessModelRename(string file, string path)
        {
            if (requiresModelRename)
            {
                if (file.Contains(oldModelName))
                {
                    return file.Replace(oldModelName, latestModelName);
                }
                else if (metasToReplace.Contains(file))
                {
                    string[] lines = File.ReadAllLines(path);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i].ToLower();
                        if (line.Contains(oldModelName))
                        {
                            lines[i] = Regex.Replace(lines[i], oldModelName, latestModelName, RegexOptions.IgnoreCase);
                        }
                    }

                    File.WriteAllLines(path, lines);
                }
            }
            return file;
        }

        // Process files inside the subdirectories
        private static void ProcessMoveFile(string path)
        {
            try
            {
                PerformExtensionsIterations((type, extention) =>
                {
                    if (path.EndsWith(extention))
                    {
                        string[] divided = path.Split('\\');
                        string file = divided[divided.Length - 1];

                        if (!modelNames.ContainsKey(latestModelName))
                        {
                            string updatedName = CheckForModelRename();

                            requiresModelRename = false;

                            if (updatedName != latestModelName)
                            {
                                requiresModelRename = true;
                                oldModelName = latestModelName.ToLower();
                                latestModelName = updatedName;
                            }

                            modelNames.Add(latestModelName, convertForm.requestInput($"Input vehicle name for {latestModelName}:"));
                        }

                        if (!Directory.Exists($"./resource\\{type}\\{latestModelName}"))
                        {
                            Directory.CreateDirectory($"./resource\\{type}\\{latestModelName}");
                        }

                        file = ProcessModelRename(file, path);

                        File.Move(path, $"./resource\\{type}\\{latestModelName}\\{file}");

                        Console.ForegroundColor = ConsoleColor.Green;
                        convertForm.LogMessage("Moved file '{0}' -> {1}.", path, $"./resource/{type}/{latestModelName}/{divided[divided.Length - 1]}");
                        Console.ForegroundColor = ConsoleColor.White;

                        return false;
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    convertForm.LogMessage("Found non useful file '{0}'.", path);
                    Console.ForegroundColor = ConsoleColor.White;
                    File.Delete(path);
                    return true;
                });
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                convertForm.LogMessage("Failed to move file '{0}'.", path);
                convertForm.LogMessage(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void PerformExtensionsIterations(Func<string, string, bool> extentionHandler)
        {
            foreach (KeyValuePair<string, string[]> extentionMap in extensions)
            {
                foreach (string extention in extentionMap.Value)
                {
                    if (!extentionHandler(extentionMap.Key, extention))
                        return;
                }
            }
        }

        private static void ProcessFile(string path)
        {
            if (path.EndsWith(".rpf"))
            {
                RpfFile rpf = new RpfFile(path, path);

                if (rpf.ScanStructure(null, null))
                {
                    ExtractFilesInRPF(rpf, "./rpf-extracted/");

                    // Move
                    Utils.ProcessDirectory("./rpf-extracted", (file, move) =>
                    {
                        ProcessMoveFile(file);
                    });
                }
                else
                {
                    convertForm.LogMessage("Input file is invalid ...");
                }

            }
        }

        private static void ExtractFilesInRPF(RpfFile rpf, string directoryOffset, int extractLevel = 0)
        {
            try
            {
                using (BinaryReader br = new BinaryReader(File.OpenRead(rpf.GetPhysicalFilePath())))
                {
                    foreach (RpfEntry entry in rpf.AllEntries)
                    {
                        if (!entry.NameLower.EndsWith(".rpf")) //don't try to extract rpf's, they will be done separately..
                        {
                            if (entry is RpfBinaryFileEntry)
                            {
                                RpfBinaryFileEntry binentry = entry as RpfBinaryFileEntry;
                                byte[] data = rpf.ExtractFileBinary(binentry, br);
                                if (data == null)
                                {
                                    if (binentry.FileSize == 0)
                                    {
                                        convertForm.LogMessage("Invalid binary file size!");
                                    }
                                    else
                                    {
                                        convertForm.LogMessage("data is null!");
                                    }
                                }
                                else if (data.Length == 0)
                                {
                                    convertForm.LogMessage("{0} : Decompressed output was empty.", entry.Path);
                                }
                                else
                                {
                                    convertForm.LogMessage("binary meme -> " + entry.NameLower);
                                    File.WriteAllBytes(directoryOffset + entry.NameLower, data);
                                }
                            }
                            else if (entry is RpfResourceFileEntry)
                            {
                                RpfResourceFileEntry resentry = entry as RpfResourceFileEntry;
                                byte[] data = rpf.ExtractFileResource(resentry, br);
                                data = ResourceBuilder.Compress(data); //not completely ideal to recompress it...
                                data = ResourceBuilder.AddResourceHeader(resentry, data);
                                if (data == null)
                                {
                                    if (resentry.FileSize == 0)
                                    {
                                        convertForm.LogMessage("{0} : Resource FileSize is 0.", entry.Path);
                                    }
                                    else
                                    {
                                        convertForm.LogMessage("{0} : {1}", entry.Path);
                                    }
                                }
                                else if (data.Length == 0)
                                {
                                    convertForm.LogMessage("{0} : Decompressed output was empty.", entry.Path);
                                }
                                else
                                {
                                    convertForm.LogMessage("Potential meme -> " + entry.NameLower);

                                    PerformExtensionsIterations((type, extention) =>
                                    {
                                        if (entry.NameLower.EndsWith(extention))
                                        {
                                            convertForm.LogMessage("Resource meme -> " + entry.NameLower);

                                            if (extention.Equals(".ytd") || extention.Equals(".ydr"))
                                            {
                                                RpfFileEntry rpfent = entry as RpfFileEntry;

                                                if (convertForm.resizer.ResizeFile(directoryOffset + entry.NameLower, rpfent))
                                                    return false;
                                            }

                                            File.WriteAllBytes(directoryOffset + entry.NameLower, data);
                                            return false;
                                        }

                                        return true;
                                    });

                                    // assume if file is ytd then this represents a new vehicle
                                    if (entry.NameLower.EndsWith(".ytd"))
                                    {
                                        latestModelName = entry.NameLower.Remove(entry.NameLower.Length - 4);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Write file first
                            RpfBinaryFileEntry binentry = entry as RpfBinaryFileEntry;
                            byte[] data = rpf.ExtractFileBinary(binentry, br);
                            File.WriteAllBytes(directoryOffset + entry.NameLower, data);

                            RpfFile subRPF = new RpfFile(directoryOffset + entry.NameLower, directoryOffset + entry.NameLower);

                            if (subRPF.ScanStructure(null, null))
                            {
                                string newExtractDir = $"./rpf-extracted-{extractLevel++}";

                                Directory.CreateDirectory(newExtractDir);

                                //recursive memes
                                ExtractFilesInRPF(subRPF, newExtractDir, extractLevel);

                                // Move
                                Utils.ProcessDirectory(newExtractDir, (file, move) =>
                                {
                                    ProcessMoveFile(file);
                                });

                                Directory.Delete(newExtractDir, true);
                            }
                            //yeet
                            File.Delete(directoryOffset + entry.NameLower);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                convertForm.LogMessage("Exception memes!");
                convertForm.LogMessage(e.Message);
            }
        }

        public static void start(object form)
        {
            convertForm = (ResourceCreator)form;

            convertForm.LogMessage("Welcome to resource creator!");
            convertForm.LogMessage("Checking resource...");

            // Check directories
            if (!Directory.Exists("./input"))
            {
                Directory.CreateDirectory("./input");
            }

            // Remove existing resized assets
            Utils.ClearDirectory("./rpf-extracted");

            Utils.ProcessDirectory("./input", (file, move) =>
            {
                ProcessFile(file);
            });

            // Create a copy of assets from last run
            Utils.MoveDirectory(".\\input", ".\\old-inputs");

            Directory.CreateDirectory("./input");

            if (modelNames.Count > 0)
            {
                Utils.WriteOutputFile(modelNames);
            }
            else
            {
                convertForm.errorMsg("There was nothing to convert!");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            convertForm.LogMessage("congrats you made it!");
            Console.ForegroundColor = ConsoleColor.White;

            convertForm.convertComplete();
        }
    }
}