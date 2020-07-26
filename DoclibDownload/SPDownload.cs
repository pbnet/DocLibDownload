using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.IO; 


namespace WssDownload4
{
    public class SPDownload
    {
        public string _DownloadPath = ""; 
        public void Execute(string url,string listName, string downloadpath)
        {
            
            if (!Directory.Exists(downloadpath))
            {
            needkey:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Folder does not exist. Do you want to create it (Y/N) ?" );
                Console.ResetColor();
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.N)
                {
                    return;
                }
                else if (key.Key == ConsoleKey.Y)
                {
                    Directory.CreateDirectory(downloadpath);
                    
                }
                else
                    goto needkey;
                
            }
            _DownloadPath = downloadpath;
            Console.WriteLine("Connecting To Site: " + url); 
            using (SPSite site = new SPSite(url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[listName];
                    StartRecursiv(list.RootFolder, _DownloadPath);
                }
            }

        }

        protected void StartRecursiv(SPFolder folder, string FolderPath)
        {
            download(folder, FolderPath);
            foreach (SPFolder myfolder in folder.SubFolders)
            {
                recursiv(myfolder, FolderPath);
            } 
        }

        protected  void recursiv(SPFolder folder,string FolderPath) 
        {
            FolderPath += "\\" + folder.Name ;
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath); 
            }
            download(folder,FolderPath); 

            foreach (SPFolder myfolder in folder.SubFolders) 
            { 
                recursiv(myfolder,FolderPath); 
            } 
        }

        protected  void download(SPFolder folder,string FolderPath)
        {
            foreach (SPFile file in folder.Files)
            {
                Console.Write("Downloading ->" + FolderPath.Substring(_DownloadPath.Length) + "\\" + file.Name + ":");
                try
                {
                    byte[] filebytes = file.OpenBinary();
                    FileStream fs = new FileStream(FolderPath + "\\" + file.Name, FileMode.Create);
                    fs.Write(filebytes, 0, filebytes.Length);
                    fs.Close();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("OK " + "FileSize: " + filebytes.Length + " byte" );
                    Console.WriteLine("");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Failed");
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
            }
        }
    }
}


