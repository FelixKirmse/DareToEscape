using System;

namespace MapEditor
{
    internal static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using (var game = new MapEditor())
            {
                game.Run();
            }
        }
    }
}