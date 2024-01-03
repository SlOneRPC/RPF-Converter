using CodeWalker.GameFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ResourceCreatorv2.Logger;
namespace ResourceCreatorv2.Actions
{
    public class ModelChecker
    {
        private const int ACCEPTABLE_MEMORY_USUAGE = 50;
        private static Dictionary<string, double> failedModels = new Dictionary<string, double>();

        public static void ProcessFile(string path)
        {
            if (path.EndsWith(".ytd") || path.EndsWith(".ydr"))
            {
                byte[] fileData = File.ReadAllBytes(path);
                string[] subPath = path.Split('\\');
                RpfFileEntry entry = Utils.CreateFileEntry(subPath[subPath.Length - 1], path, ref fileData);
                string filePath = subPath[subPath.Length - 2] + "\\" + subPath[subPath.Length - 1];
                try
                {
                    double memoryUsage = 0.00;

                    if (path.EndsWith("ytd"))
                    {
                        YtdFile ytd = new YtdFile();
                        ytd.Load(fileData, entry);

                        memoryUsage = Utils.ConvertBytesToMegabytes(ytd.TextureDict.MemoryUsage);
                    }
                    else if (path.EndsWith("ydr"))
                    {
                        YdrFile ydr = new YdrFile();
                        ydr.Load(fileData, entry);

                        if (ydr.Drawable.ShaderGroup.TextureDictionary == null)
                        {
                            LogInfo("File does not contain a texture dictionary {0}", filePath);
                            return;
                        }

                        memoryUsage = Utils.ConvertBytesToMegabytes(ydr.Drawable.ShaderGroup.TextureDictionary.MemoryUsage);
                    }

                    if (memoryUsage >= ACCEPTABLE_MEMORY_USUAGE)
                    {
                        failedModels.Add(filePath, memoryUsage);
                        LogError("Path {0} exceeds maximum allowed memory usage allowed={1} : usage={2}MB of vram", filePath, ACCEPTABLE_MEMORY_USUAGE, memoryUsage);
                        return;
                    }
                    else
                    {
                        LogInfo("Valid model texture size {0} : {1}MB", filePath, memoryUsage);
                    }
                } 
                catch (Exception)
                {
                    LogError("Invalid model detected {0}", path);
                }
            }
        }

        public static void Start(string vehicleDirectory)
        {
            Utils.ProcessDirectory(vehicleDirectory, (file, move) =>
            {
                ProcessFile(file);
            });


            if (failedModels.Count > 0)
            {
                LogInfo("\n\nFailed assets (threshold 50MB of vram):");
                foreach (KeyValuePair<string, double> model in failedModels)
                {
                    LogInfo("Path {0} failed due to vram usage of {1}MB", model.Key, model.Value);
                }
                Environment.Exit(1);
            }
        }
    }
}
