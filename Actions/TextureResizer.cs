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
            if (!path.EndsWith(".ytd") && !path.EndsWith(".ydr"))
                return;

            convertForm.resizer.ResizeFile(path);

            string[] splitPath = path.Split('\\');

            string destination = $"./output\\{splitPath[splitPath.Length - 1]}";

            Utils.MoveFile(path, destination);
        }

        public static void Start(object form)
        {
            convertForm = (ResourceCreator)form;

            Console.WriteLine("Welcome to resource creator!");
            Console.WriteLine("Checking resource...");

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
            Console.WriteLine("congrats you made it!");
            Console.ForegroundColor = ConsoleColor.White;

            convertForm.convertComplete();
        }
    }
}
