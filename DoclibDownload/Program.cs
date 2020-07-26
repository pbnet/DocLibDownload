using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WssDownload4
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Doclib Download for SharePoint");
                Console.ResetColor();
                Console.WriteLine("Usage: doclibdownload <site url> <library name> <download path>");
                Console.WriteLine("Example: doclibdownload http://sharepoint.pbnet.pbnet.local SharedDocs c:\\downloads");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.ResetColor();
                return; 
            }
            try
            {
                SPDownload sp = new SPDownload(); 
                sp.Execute(args[0],args[1],args[2]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Operation completed.");
                Console.ResetColor();
                Console.ReadKey(); 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace); 
            }

        }
    }
}
