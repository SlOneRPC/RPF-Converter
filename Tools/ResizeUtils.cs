using CodeWalker.GameFiles;
using CodeWalker.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ResourceCreatorv2
{
    public class ResizeUtils
    {
        public float minAcceptableFileSize { get; set; }
        public float minAcceptableTextureSize { get; set; }
        public ResizeUtils(float minAcceptableFileSize, float minAcceptableTextureSize)
        {
            this.minAcceptableFileSize = minAcceptableFileSize * 1000000;
            this.minAcceptableTextureSize = minAcceptableTextureSize * 1000000;
        }

        public bool ResizeFile(string path, RpfFileEntry entry)
        {
            byte[] fileData = entry.File.ExtractFile(entry);
            return ResizeFile(path, entry, ref fileData);
        }

        public bool ResizeFile(string path)
        {
            byte[] fileData = File.ReadAllBytes(path);
            string[] subPath = path.Split('\\');
            RpfFileEntry entry = Utils.CreateFileEntry(subPath[subPath.Length - 1], path, ref fileData);
            return ResizeFile(path, entry, ref fileData);
        }

        private bool ResizeFile(string path, RpfFileEntry entry, ref byte[] fileData)
        {
            if (path.EndsWith("ytd"))
            {
                YtdFile ytd = new YtdFile();
                ytd.Load(fileData, entry);

                if (ytd.TextureDict.MemoryUsage >= minAcceptableFileSize)
                {
                    byte[] reizedYTD = ResizeYTD(ytd);
                    if (reizedYTD != null)
                    {
                        File.WriteAllBytes(path, reizedYTD);
                        return true;
                    }
                }
            }
            else if (path.EndsWith("ydr"))
            {
                YdrFile ydr = new YdrFile();
                ydr.Load(fileData, entry);

                if (ydr.Drawable.ShaderGroup.TextureDictionary.MemoryUsage >= minAcceptableFileSize)
                {
                    byte[] reizedYDR = ResizeYDR(ydr);
                    if (reizedYDR != null)
                    {
                        File.WriteAllBytes(path, reizedYDR);
                        return true;
                    }
                }
            }
            return false;
        }

        public byte[] ResizeYTD(YtdFile ytd)
        {
            bool resized = false;
            TextureDictionary resizedDict = ResizeTextureDictonary(ytd.TextureDict, ref resized);

            if (resized)
            {
                ytd.TextureDict = resizedDict;
                Console.WriteLine("Done some ytd resize memes -> " + ytd.Name);
                return ytd.Save();
            }
            return null;
        }

        public byte[] ResizeYDR(YdrFile ydr)
        {
            bool resized = false;
            TextureDictionary resizedDict = ResizeTextureDictonary(ydr.Drawable.ShaderGroup.TextureDictionary, ref resized);

            if (resized)
            {
                ydr.Drawable.ShaderGroup.TextureDictionary = resizedDict;
                Console.WriteLine("Done some ydr resize memes -> " + ydr.Name);
                return ydr.Save();
            }
            return null;
        }

        private TextureDictionary ResizeTextureDictonary(TextureDictionary tex, ref bool resized)
        {
            Dictionary<uint, Texture> dict = new Dictionary<uint, Texture>();

            foreach (KeyValuePair<uint, Texture> texture in tex.Dict)
            {
                try
                {
                    if (texture.Value.MemoryUsage > minAcceptableTextureSize)
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
                        Console.WriteLine($"before -> {Utils.ConvertBytesToMegabytes(dds.Length).ToString("#.##")}MB");
                        Console.WriteLine($"after -> {Utils.ConvertBytesToMegabytes(resizedData.Length).ToString("#.##")}MB");
                        dict.Add(texture.Key, resizedTex);

                        // Yeet the file, we are done with it
                        File.Delete("./rpf-extracted/" + fileName);
                        resized = true;
                    }
                    else
                    {
                        dict.Add(texture.Key, texture.Value);
                        Console.WriteLine("Skipped {0}, Memory size {1}MB, Dimensions {2}", texture.Value.Name, Utils.ConvertBytesToMegabytes(texture.Value.MemoryUsage).ToString("#.##"), texture.Value.Height + "x" + texture.Value.Width);
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Could not resize -> " + texture.Value.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    dict.Add(texture.Key, texture.Value);
                }
            }

            if (resized)
            {
                TextureDictionary dic = new TextureDictionary();
                dic.Textures = new ResourcePointerList64<Texture>();
                dic.TextureNameHashes = new ResourceSimpleList64_uint();
                dic.Textures.data_items = dict.Values.ToArray();
                dic.TextureNameHashes.data_items = dict.Keys.ToArray();
                dic.BuildDict();
                return dic;
            }

            return null;
        }
    }
}
