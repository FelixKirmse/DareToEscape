using System;
using System.Windows.Forms;
using DareToEscape.Helpers;

namespace DareToEscape
{
#if WINDOWS || XBOX
    internal static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            using(var game = new DareToEscape())
            {
                game.Run();
            }
        }
    }
#endif
}