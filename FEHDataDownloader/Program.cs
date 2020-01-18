using System;

namespace FEH_Data_Downloader {
    class Program {
        static void Main(string[] args) {
            new Downloader();

            Logger.Log("Download complete! Press a key to close.", Logger.LOG_LEVEL.SUCCESS);
            Console.ReadKey();
        }
    }
}
