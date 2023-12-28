using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ActivationContext;

namespace ResourceCreatorv2
{
    public static class Logger
    {
        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarning(string message, params object[] args)
        {
            LogWarning(string.Format(message, args));
        }

        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(string message, params object[] args)
        {
            LogError(string.Format(message, args));
        }


        public static void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        public static void LogInfo(string message, params object[] args)
        {
            LogInfo(string.Format(message, args));
        }
    }
}
