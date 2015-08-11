using System;

namespace Eleks.MoovTheSphero.Utils
{
    public static class Tracer
    {
        public static void Error(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ResetColor();
        }

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Trace(string message)
        {
            Console.WriteLine(message);
        }
    }
}
