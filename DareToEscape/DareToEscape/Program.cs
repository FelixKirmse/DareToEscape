using System;

namespace DareToEscape
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DareToEscape game = new DareToEscape())
            {
                game.Run();
            }
        }
    }
#endif
}

