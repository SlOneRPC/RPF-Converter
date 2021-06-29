using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using CodeWalker.GameFiles;
using CodeWalker.Utils;
namespace ResourceCreator
{
    class Program
    {
        static Dictionary<string, string[]> extensions = new Dictionary<string, string[]>()
        {
            { "meta",  new string[]{ ".meta", "clip_sets.xml" } },
            { "stream", new string[]{".ytd", ".yft", ".ydr" } }
        };

        static Dictionary<string, string> modelNames = new Dictionary<string, string>();

        static string latestModelName = "";

        // Process files inside the subdirectories
        public static void ProcessMoveFile(string path)
        {
            try
            {
                foreach(KeyValuePair<string, string[]> extentionMap in extensions)
                {
                    foreach(string extention in extentionMap.Value)
                    {
                        if (path.EndsWith(extention))
                        {
                            string[] divided = path.Split("\\");

                            if (!Directory.Exists($"./resource\\{extentionMap.Key}\\{latestModelName}"))
                            {
                                Directory.CreateDirectory($"./resource\\{extentionMap.Key}\\{latestModelName}");
                            }

                            if (!modelNames.ContainsKey(latestModelName))
                            {
                                Console.WriteLine("\nInput a name for the model: '{0}'.", latestModelName);
                                while (true)
                                {
                                    string realName = Console.ReadLine();
                                    if (realName != null && realName.Length > 0)
                                    {
                                        modelNames.Add(latestModelName, realName);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input, try again for model: '{0}'.", latestModelName);
                                    }
                                }
                            }

                            File.Move(path, $"./resource\\{extentionMap.Key}\\{latestModelName}\\{divided[divided.Length - 1]}");

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


        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory, bool move)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Processing directory {targetDirectory} ...");
            Console.ForegroundColor = ConsoleColor.White;
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                    if (move)
                        ProcessMoveFile(fileName);
                    else
                        ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, move);
        }

        public static void ProcessFile(string path)
        {
            if (path.EndsWith(".rpf"))
            {
                RpfFile rpf = new RpfFile(path, path);

                if (rpf.ScanStructure(null, null))
                {
                    ExtractFilesInRPF(rpf, "./rpf-extracted/");

                    // Move
                    ProcessDirectory("./rpf-extracted", true);
                }
                else
                {
                    Console.WriteLine("Input file is invalid ...");
                }

            }
        }

        static void ExtractFilesInRPF(RpfFile rpf, string directoryOffset)
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

                                                    Dictionary<uint, Texture> Dicts = new Dictionary<uint, Texture>();

                                                    bool somethingResized = false;
                                                    foreach (KeyValuePair<uint, Texture> texture in ytd.TextureDict.Dict)
                                                    {
                                                        if (texture.Value.Width > 1440 || needsResized && texture.Value.Width > 550) // Only resize if it is greater than 1440p or 550p if vehicle is oversized
                                                        {
                                                            byte[] dds = DDSIO.GetDDSFile(texture.Value);

                                                            string fileName = $"{texture.Value.Name}.dds";
                                                            fileName = String.Concat(fileName.Where(c => !Char.IsWhiteSpace(c)));

                                                            File.WriteAllBytes("./NConvert/" + fileName, dds);

                                                            Process p = new Process();
                                                            p.StartInfo.FileName = @"./NConvert/nconvert.exe";
                                                            p.StartInfo.Arguments = @"-out dds -resize 50% 50% -overwrite ./NConvert/" + fileName;
                                                            p.StartInfo.UseShellExecute = false;
                                                            p.StartInfo.RedirectStandardOutput = true;
                                                            p.Start();

                                                            //Wait for the process to end.
                                                            p.WaitForExit();

                                                            // Move file back
                                                            File.Move("./NConvert/" + fileName, directoryOffset + fileName);

                                                            byte[] resizedData = File.ReadAllBytes(directoryOffset + fileName);
                                                            Texture resizedTex = DDSIO.GetTexture(resizedData);
                                                            resizedTex.Name = texture.Value.Name;
                                                            Console.WriteLine(resizedData.Length.ToString());
                                                            Dicts.Add(texture.Key, resizedTex);

                                                            // Yeet the file, we are done with it
                                                            File.Delete(directoryOffset + fileName);
                                                            somethingResized = true;
                                                        }
                                                        else
                                                        {
                                                            Dicts.Add(texture.Key, texture.Value);
                                                        }
                                                    }
                                                    // No point rebuilding the ytd when nothing was resized
                                                    if (!somethingResized)
                                                        break;

                                                    TextureDictionary dic = new TextureDictionary();
                                                    dic.Textures = new ResourcePointerList64<Texture>();
                                                    dic.TextureNameHashes = new ResourceSimpleList64_uint();
                                                    dic.Textures.data_items = Dicts.Values.ToArray();
                                                    dic.TextureNameHashes.data_items = Dicts.Keys.ToArray();

                                                    dic.BuildDict();
                                                    ytd.TextureDict = dic;

                                                    byte[] resizedYtdData = ytd.Save();
                                                    File.WriteAllBytes(directoryOffset + entry.NameLower, resizedYtdData);

                                                    Console.WriteLine("Done some ytd resize memes -> " + entry.NameLower);
                                                    break;
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

        public static void MoveDirectory(string source, string target)
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
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to resource creator!");
            Console.WriteLine("Checking resource...");

            // Check directories
            if (!Directory.Exists("./input"))
            {
                Directory.CreateDirectory("./input");
            }

          
            if (Directory.Exists("./resource"))
            {
                Directory.Delete("./resource", true);
            }

            while (true) 
            {
                if (Directory.Exists("./rpf-extracted"))
                {
                    Directory.Delete("./rpf-extracted", true);

                    Directory.CreateDirectory("./rpf-extracted");
                }
                else
                {
                    Directory.CreateDirectory("./rpf-extracted");
                }

                ProcessDirectory("./input", false);

                MoveDirectory(".\\input", ".\\old-inputs");

                Directory.CreateDirectory("./input");

                Console.WriteLine("Type exit to finish or anything else to continue once file is in place");
                if (Console.ReadLine().ToLower().Equals("exit"))
                    break;
            }

            // Write memes to file
            string textOutput = "";
            foreach (KeyValuePair<string, string> modelName in modelNames)
            {
                textOutput += $"AddTextEntry(\"{modelName.Key}\", \"{modelName.Value}\")\n";
            }
            textOutput += "\n";
            foreach (KeyValuePair<string, string> modelName in modelNames)
            {
                textOutput += $"\"{modelName.Key}\": \"{modelName.Value}\"\n";
            }
            File.WriteAllText("./resource\\output.txt", textOutput);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("congrats you made it!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
