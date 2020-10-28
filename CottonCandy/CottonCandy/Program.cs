using System;

namespace CottonCandy
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //FileLoader.WriteText("[STATUS " + DateTime.Now.ToLongTimeString() + "] Game has started!", "log.txt");
            using (var game = new CottonCandy())
                game.Run();
        }
    }
#endif
}
