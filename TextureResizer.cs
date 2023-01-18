using CodeWalker.GameFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceCreatorv2
{
    public class TextureResizer
    {
        public static ResourceCreator convertForm;

        private static void ProcessFileMove(string path)
        {
            if (!path.EndsWith(".ytd"))
                return;

            byte[] data = File.ReadAllBytes(path);

            if (data.Length > 5242880)
            {
                Utils.YtdResize(path);
            }

            string[] splitPath = path.Split('\\');

            string destination = $"./output\\{splitPath[splitPath.Length - 1]}";

            File.Move(path, destination);

            Console.ForegroundColor = ConsoleColor.Green;
            convertForm.LogMessage("Moved file '{0}' -> {1}.", path, destination);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Start(object form)
        {
            convertForm = (ResourceCreator)form;

            convertForm.LogMessage("Welcome to resource creator!");
            convertForm.LogMessage("Checking resource...");

            // Check directories
            if (!Directory.Exists("./input"))
            {
                Directory.CreateDirectory("./input");
            }

            Utils.ClearDirectory("./rpf-extracted");
            Utils.ClearDirectory("./output");


            Utils.ProcessDirectory("./input", (file, move) =>
            {
                ProcessFileMove(file);
            });

            Console.ForegroundColor = ConsoleColor.Green;
            convertForm.LogMessage("congrats you made it!");
            Console.ForegroundColor = ConsoleColor.White;

            convertForm.convertComplete();
        }
    }
}
