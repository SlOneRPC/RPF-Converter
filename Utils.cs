using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWalker.GameFiles;
using CodeWalker.Utils;
using static System.ActivationContext;

namespace ResourceCreatorv2
{
    public static class Utils
    {
        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory, Action<string, object> FileHandler, object flags = null)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                FileHandler(fileName, flags);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, FileHandler, flags);
        }

        public static void ClearDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);

                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }

        public static RpfFileEntry CreateFileEntry(string name, string path, ref byte[] data)
        {
            //this should only really be used when loading a file from the filesystem.
            RpfFileEntry e = null;
            uint rsc7 = (data?.Length > 4) ? BitConverter.ToUInt32(data, 0) : 0;
            if (rsc7 == 0x37435352) //RSC7 header present! create RpfResourceFileEntry and decompress data...
            {
                e = RpfFile.CreateResourceFileEntry(ref data, 0);//"version" should be loadable from the header in the data..
                data = ResourceBuilder.Decompress(data);
            }
            else
            {
                var be = new RpfBinaryFileEntry();
                be.FileSize = (uint)data?.Length;
                be.FileUncompressedSize = be.FileSize;
                e = be;
            }
            e.Name = name;
            e.NameLower = name?.ToLowerInvariant();
            e.NameHash = JenkHash.GenHash(e.NameLower);
            e.ShortNameHash = JenkHash.GenHash(Path.GetFileNameWithoutExtension(e.NameLower));
            e.Path = path;
            return e;
        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
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

        public static void MoveFile(string from, string to)
        {
            File.Move(from, to);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Moved file '{0}' -> {1}.", from, to);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteOutputFile(Dictionary<string, string> modelNames)
        {
            // Write memes to file
            string textOutput = "//Text entries";
            foreach (KeyValuePair<string, string> modelName in modelNames)
            {
                textOutput += $"AddTextEntry(\"{modelName.Key}\", \"{modelName.Value}\")\n";
            }
            textOutput += "\n//Json bindings\n";
            foreach (KeyValuePair<string, string> modelName in modelNames)
            {
                textOutput += $"\"{modelName.Key}\": \"{modelName.Value}\"\n";
            }
            textOutput += "\n//Changelog\n";
            foreach (KeyValuePair<string, string> modelName in modelNames)
            {
                textOutput += $"{modelName.Value}\n";
            }

            File.WriteAllText("./resource\\output.txt", textOutput);
        }
    }
}
