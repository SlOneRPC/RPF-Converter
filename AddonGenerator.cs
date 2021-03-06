using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private static string[] metasToReplace = new string[] { "vehicles.meta", "handling.meta", "carvariations.meta", "carcols.meta" };

        // Process files inside the subdirectories
        private static void ProcessMoveFile(string path)
        {
            try
            {
                foreach(KeyValuePair<string, string[]> extentionMap in extensions)
                {
                    foreach(string extention in extentionMap.Value)
                    {
                        if (path.EndsWith(extention))
                        {
                            string[] divided = path.Split('\\');

                            if (!modelNames.ContainsKey(latestModelName))
                            {
                                string updatedName = "";

                                while (true)
                                {
                                    updatedName = convertForm.requestInput($"Change model or press next:", latestModelName);

                                    if (!Directory.Exists($"./resource\\meta\\{updatedName}"))
                                    {
                                        if (convertForm.getRootDir().Length < 1 || !Misc.checkRootForModelName(updatedName, convertForm.getRootDir()))
                                            break;
                                        else
                                            convertForm.errorMsg("This model name is already in use in root dir!");
                                    }
                                    else
                                    {
                                        convertForm.errorMsg("This model has already been converted!");
                                    }
                                }

                                requiresModelRename = false;

                                if (updatedName != latestModelName)
                                {
                                    requiresModelRename = true;
                                    oldModelName = latestModelName.ToLower();
                                    latestModelName = updatedName;
                                }

                                string vehicleName = convertForm.requestInput($"Input vehicle name for {latestModelName}:");
                                modelNames.Add(latestModelName, vehicleName);
                            }

                            if (!Directory.Exists($"./resource\\{extentionMap.Key}\\{latestModelName}"))
                            {
                                Directory.CreateDirectory($"./resource\\{extentionMap.Key}\\{latestModelName}");
                            }

                            string file = divided[divided.Length - 1];

                            if (requiresModelRename)
                            {
                                if (file.Contains(oldModelName))
                                {
                                    file = file.Replace(oldModelName, latestModelName);
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

                            File.Move(path, $"./resource\\{extentionMap.Key}\\{latestModelName}\\{file}");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Moved file '{0}' -> {1}.", path, $"./resource/{extentionMap.Key}/{latestModelName}/{divided[divided.Length - 1]}");
                            Console.ForegroundColor = ConsoleColor.White;

                            return;
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Found non useful file '{0}'.", path);
                Console.ForegroundColor = ConsoleColor.White;
                File.Delete(path);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to move file '{0}'.", path);
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
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
                    Console.WriteLine("Input file is invalid ...");
                }

            }
        }

        private static void ExtractFilesInRPF(RpfFile rpf, string directoryOffset)
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
                                        Console.WriteLine("Invalid binary file size!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("data is null!");
                                    }
                                }
                                else if (data.Length == 0)
                                {
                                    Console.WriteLine("{0} : Decompressed output was empty.", entry.Path);
                                }
                                else
                                {
                                    Console.WriteLine("binary meme -> " + entry.NameLower);
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
                                        Console.WriteLine("{0} : Resource FileSize is 0.", entry.Path);
                                    }
                                    else
                                    {
                                        Console.WriteLine("{0} : {1}", entry.Path);
                                    }
                                }
                                else if (data.Length == 0)
                                {
                                    Console.WriteLine("{0} : Decompressed output was empty.", entry.Path);
                                }
                                else
                                {
                                    Console.WriteLine("Potential meme -> " + entry.NameLower);
                                    foreach (KeyValuePair<string, string[]> extentionMap in extensions)
                                    {
                                        foreach (string extention in extentionMap.Value)
                                        {
                                            if (entry.NameLower.EndsWith(extention))
                                            {
                                                Console.WriteLine("Resource meme -> " + entry.NameLower);

                                                if (extention.Equals(".ytd"))
                                                {
                                                    RpfFileEntry rpfent = entry as RpfFileEntry;

                                                    byte[] ytddata = rpfent.File.ExtractFile(rpfent);

                                                    bool needsResized = ytddata.Length > 5242880; // 5MB

                                                    YtdFile ytd = new YtdFile();
                                                    ytd.Load(ytddata, rpfent);

                                                    byte[] resizedData = Utils.DoResizing(ytd, needsResized);
                                                    if (resizedData != null)
                                                    {
                                                        File.WriteAllBytes(directoryOffset + entry.NameLower, resizedData);
                                                        break;
                                                    }
                                                }

                                                File.WriteAllBytes(directoryOffset + entry.NameLower, data);
                                                break;
                                            }
                                        }
                                    }

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
                                //recursive memes
                                ExtractFilesInRPF(subRPF, directoryOffset);
                            }
                            //yeet
                            File.Delete(directoryOffset + entry.NameLower);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception memes!");
                Console.WriteLine(e.Message);
            }
        }

        private static void MoveDirectory(string source, string target)
        {
            var sourcePath = source.TrimEnd('\\', ' ');
            var targetPath = target.TrimEnd('\\', ' ');
            var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories).GroupBy(s => Path.GetDirectoryName(s));
            foreach (var folder in files)
            {
                var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                Directory.CreateDirectory(targetFolder);
                foreach (var file in folder)
                {
                    try
                    {
                        var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                        if (File.Exists(targetFile)) File.Delete(targetFile);
                        File.Move(file, targetFile);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            Directory.Delete(source, true);
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

          
            //if (Directory.Exists("./resource"))
            //{
            //    Directory.Delete("./resource", true);
            //}


            if (Directory.Exists("./rpf-extracted"))
            {
                Directory.Delete("./rpf-extracted", true);

                Directory.CreateDirectory("./rpf-extracted");
            }
            else
            {
                Directory.CreateDirectory("./rpf-extracted");
            }

            Utils.ProcessDirectory("./input", (file, move) =>
            {
                ProcessFile(file);
            });

            MoveDirectory(".\\input", ".\\old-inputs");

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
