using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading;
using CodeWalker.GameFiles;
using CodeWalker.Utils;
using static ResourceCreatorv2.Logger;

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

        public static ResourceCreator convertForm;

        // Model name replacement stuff
        private static bool requiresModelRename = false;
        private static string oldModelName = "";
        private static Dictionary<string, string> renamedModelMap = new Dictionary<string, string>();
        private static string[] metasToReplace = new string[] 
        { 
            "vehicles.meta", "handling.meta", "carvariations.meta", "carcols.meta" 
        };

        static string CheckForModelRename(string model)
        {
            while (true)
            {
                string updatedName = convertForm.requestInput($"Change model or press next:", model);

                if (!Directory.Exists($"./resource\\stream\\{updatedName}"))
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

        static string ProcessModelRename(string file, string path, string modelName)
        {
            if (requiresModelRename)
            {
                if (file.Contains(oldModelName))
                {
                    return file.Replace(oldModelName, modelName);
                }
                else if (metasToReplace.Contains(file))
                {
                    string[] lines = File.ReadAllLines(path);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i].ToLower();
                        if (line.Contains(oldModelName))
                        {
                            lines[i] = Regex.Replace(lines[i], oldModelName, modelName, RegexOptions.IgnoreCase);
                        }
                    }

                    File.WriteAllLines(path, lines);
                }
            }
            return file;
        }

        // Process files inside the subdirectories
        private static void ProcessMoveFile(string path, List<string> models)
        {
            if (models.Count <= 0)
                return;

            try
            {
                bool found = PerformExtensionsIterations((type, extention) =>
                {
                    if (path.EndsWith(extention))
                    {
                        string[] divided = path.Split('\\');
                        string file = divided[divided.Length - 1];

                        string cleanModelName = models.Count > 1 ? $"modpack-{models.Count}" : models[0];

                        if (type != "meta")
                        {
                            cleanModelName = file.Split('.')[0].Trim()
                                            .Replace("_hi", "")
                                            .Replace("+hi", "");

                            int splitPosition = cleanModelName.IndexOf("_");

                            if (splitPosition > 0)
                            {
                                cleanModelName = cleanModelName.Substring(0, splitPosition);
                            }

                            // re-map
                            if (renamedModelMap.ContainsKey(cleanModelName))
                                cleanModelName = renamedModelMap[cleanModelName];

                            if (!modelNames.ContainsKey(cleanModelName))
                            {
                                string updatedName = CheckForModelRename(cleanModelName);

                                requiresModelRename = false;

                                if (updatedName != cleanModelName)
                                {
                                    requiresModelRename = true;
                                    oldModelName = cleanModelName.ToLower();
                                    cleanModelName = updatedName;
                                    renamedModelMap.Add(oldModelName, cleanModelName);
                                }
                                
                                modelNames.Add(cleanModelName, convertForm.requestInput($"Input vehicle name for {cleanModelName}:"));
                            }
                        }

                        // Create output directory for vehicle if it doesn't exist
                        if (!Directory.Exists($"./resource\\{type}\\{cleanModelName}"))
                        {
                            Directory.CreateDirectory($"./resource\\{type}\\{cleanModelName}");
                        }

                        file = ProcessModelRename(file, path, cleanModelName);

                        Utils.MoveFile(path, $"./resource\\{type}\\{cleanModelName}\\{file}");
                        return false;
                    }

                    return true;
                });

                if (!found)
                {
                    LogWarning("Found non useful file '{0}'.", path);
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                LogError("Failed to move file '{0}'. \n{1}", path, e.Message);
            }
        }

        public static bool PerformExtensionsIterations(Func<string, string, bool> extentionHandler)
        {
            foreach (KeyValuePair<string, string[]> extentionMap in extensions)
            {
                foreach (string extention in extentionMap.Value)
                {
                    if (!extentionHandler(extentionMap.Key, extention))
                        return true;
                }
            }
            return false;
        }

        private static void ProcessFile(string path)
        {
            if (path.EndsWith(".rpf"))
            {
                RpfFile rpf = new RpfFile(path, path);

                if (rpf.ScanStructure(null, null))
                {
                    List<string> models = new List<string>();

                    ExtractFilesInRPF(rpf, "./rpf-extracted/", ref models);

                    renamedModelMap = new Dictionary<string, string>();

                    // Move
                    Utils.ProcessDirectory("./rpf-extracted", (file, move) =>
                    {
                        ProcessMoveFile(file, models);
                    });
                }
                else
                {
                    LogError("Input file is invalid ... {0}", path);
                }

            }
        }

        private static void ExtractFilesInRPF(RpfFile rpf, string directoryOffset, ref List<string> models, int extractLevel = 0)
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
                                        LogError("Invalid binary file size! {0}", rpf.GetPhysicalFilePath());
                                    }
                                    else
                                    {
                                        LogError("data is null! {0}", rpf.GetPhysicalFilePath());
                                    }
                                }
                                else if (data.Length == 0)
                                {
                                    LogError("{0} : Decompressed output was empty.", entry.Path);
                                }
                                else
                                {
                                    LogInfo("binary meme -> " + entry.NameLower);
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
                                        LogError("{0} : Resource FileSize is 0.", entry.Path);
                                    }
                                    else
                                    {
                                        LogError("{0} : {1}", entry.Path);
                                    }
                                }
                                else if (data.Length == 0)
                                {
                                    LogError("{0} : Decompressed output was empty.", entry.Path);
                                }
                                else
                                {
                                    LogInfo("Potential meme -> " + entry.NameLower);

                                    PerformExtensionsIterations((type, extention) =>
                                    {
                                        if (entry.NameLower.EndsWith(extention))
                                        {
                                            LogInfo("Resource meme -> " + entry.NameLower);

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
                                        models.Add(entry.NameLower.Remove(entry.NameLower.Length - 4));
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
                                //recursive memes
                                ExtractFilesInRPF(subRPF, directoryOffset, ref models, extractLevel);
                            }
                            //yeet
                            File.Delete(directoryOffset + entry.NameLower);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError("Error when attempting to extract -> {0}", rpf.GetPhysicalFilePath());
                LogError(e.Message);
            }
        }

        public static void start(object form)
        {
            convertForm = (ResourceCreator)form;
            
            Console.WriteLine("Welcome to resource creator!");
            Console.WriteLine("Checking resource...");

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
            Console.WriteLine("congrats you made it!");
            Console.ForegroundColor = ConsoleColor.White;

            convertForm.convertComplete();
        }
    }
}