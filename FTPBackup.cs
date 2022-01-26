using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MenuSMPTUserConsoleApp
{
    class FTPBackup
    {
        //FTPBackup createFtpFolder = new FTPBackup();
       public string host = "ftp://82.214.114.2";
        public string path = "/HomeWork/Hristijanfile.txt";
        public string fileToUpload = "MyFile.txt";
        public string fileToDelete = "/HomeWork/";
        public string userId = "bojan_academy";
        public string password = "qjeK7#88";
        public string folderPath = "/HomeWork/Hristijanfile.txt";
        //create ftp folder
        public bool CreateFtpFolder()
        {
            string path = "/HomeWork.txt/Hristijanfile.txt";
            bool isCreated = true;
            try
            {
                //set-ups for creating folder 
                WebRequest request = WebRequest.Create(host + path);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(userId, password);
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                    Console.WriteLine("You have successfully created :" + path);
                }
            }
            catch (WebException ex)
            {
                isCreated = false;
                Console.WriteLine(ex.Message);
                Console.WriteLine("Error at: " + ex.StackTrace);
                Console.WriteLine("Read the error or press Enter to continue!");
                Console.ReadLine();
            }
            return isCreated;
        }
        //upload file
        public void FileToUpload()
        {
            string From = folderPath;
            string To = "ftp://82.214.114.2";
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(userId, password);
                    client.UploadFile(folderPath, WebRequestMethods.Ftp.UploadFile, From);
                    client.UploadFile(To, WebRequestMethods.Ftp.UploadFile, From);
                }
            }
            catch (System.Net.WebException webE)
            {

                Console.WriteLine("Error is : " + webE.Message);
                Console.WriteLine("ERROR:" + webE.StackTrace);
            }
        }
        //delete folder
        public void DeleteFTPFolder(string fileToDelete)  
            {
            
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(fileToDelete);  
            request.Method = WebRequestMethods.Ftp.RemoveDirectory;  
            request.Credentials = new System.Net.NetworkCredential(); ;  
            request.GetResponse().Close();
            DeleteFTPFolder("ftp://82.214.114.2/Directory/HomeWork.txt");
        }  

    }
}
