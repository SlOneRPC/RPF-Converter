using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWalker.GameFiles;
using CodeWalker.Utils;
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

        public static byte[] DoResizing(YtdFile ytd, bool needsForcedResized)
        {
            Dictionary<uint, Texture> Dicts = new Dictionary<uint, Texture>();

            bool somethingResized = false;
            foreach (KeyValuePair<uint, Texture> texture in ytd.TextureDict.Dict)
            {
                try
                {
                    if (texture.Value.Width > 1000 || needsForcedResized && texture.Value.Width > 200) // Only resize if it is greater than 1440p or 550p if vehicle is oversized
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
                        File.Move("./NConvert/" + fileName, "./rpf-extracted/" + fileName);

                        byte[] resizedData = File.ReadAllBytes("./rpf-extracted/" + fileName);
                        Texture resizedTex = DDSIO.GetTexture(resizedData);
                        resizedTex.Name = texture.Value.Name;
                        Console.WriteLine(resizedData.Length.ToString());
                        Dicts.Add(texture.Key, resizedTex);

                        // Yeet the file, we are done with it
                        File.Delete("./rpf-extracted/" + fileName);
                        somethingResized = true;
                    }
                    else
                    {
                        Dicts.Add(texture.Key, texture.Value);
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not resize -> " + texture.Value.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    Dicts.Add(texture.Key, texture.Value);
                }
            }
            // No point rebuilding the ytd when nothing was resized
            if (somethingResized)
            {
                TextureDictionary dic = new TextureDictionary();
                dic.Textures = new ResourcePointerList64<Texture>();
                dic.TextureNameHashes = new ResourceSimpleList64_uint();
                dic.Textures.data_items = Dicts.Values.ToArray();
                dic.TextureNameHashes.data_items = Dicts.Keys.ToArray();

                dic.BuildDict();
                ytd.TextureDict = dic;

                Console.WriteLine("Done some ytd resize memes -> " + ytd.Name);
                return ytd.Save();
            }

            return null;
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

            int prefix = 1;
            while (File.Exists($"./resource\\output{prefix}.txt"))
            {
                prefix++;
            }

            File.WriteAllText($"./resource\\output{prefix}.txt", textOutput);
        }
    }
}
