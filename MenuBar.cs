using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MenuSMPTUserConsoleApp
{
    public class MenuBar
    {
        #region
        //setup-s for smtp client-server
        public static string username;
        public static string password;
        public static string destination { get; private set; }
        public static string subject { get; private set; }
        public static string body { get; private set; }
        public static int ExitCode { get; set; }
        #endregion
        //setting up method for menubar
        public void CreateMenuBar(string MenuBar)
        {
            //putting list for email
            bool quit = false;
            int option = -1;
            string userInput = null;
            string pass = "";
            //void Menu()
            //{
            MenuBar CreateMenuBar = new MenuBar();
            //set cmd color
            ConsoleColor green = ConsoleColor.Green;
            Console.ForegroundColor = green;
            //set user questions
            Console.WriteLine("\n Choose one of the options below: ");
            Console.WriteLine("\t 1) Send Email: ");
            Console.WriteLine("\t 2) Create FTP Backup: ");
            Console.WriteLine("\t 3) Quit");

            //While user not insert 3(quit) , execute the program
            while (!quit)
            {
                userInput = Console.ReadLine();
                if (!Int32.TryParse(userInput, out option) || !(option >= 1 && option <= 3) || (option == null))
                {
                    Console.WriteLine("Invalid input.Try again: ");
                    CreateMenuBar.CreateMenuBar("");
                }
                switch (option)
                {
                    case 1:
                        var email = "email@email.com";
                        Console.WriteLine("What is your email address?");
                        username = Convert.ToString(Console.ReadLine());
                        while (username == ("email@email.com")) 
                        {
                            continue;
                        }
                        if (username != email)
                        {
                            Console.WriteLine("try again!");
                            CreateMenuBar.CreateMenuBar("");
                        }
                        Console.Write("Enter your password: ");
                        ConsoleKeyInfo key;
                        do
                        {
                            key = Console.ReadKey(true);
                            // Backspace Should Not Work
                            if (key.Key != ConsoleKey.Backspace)
                            {
                                pass += key.KeyChar;
                                Console.Write("*");
                            }
                            else
                            {
                                Console.Write("\b");//in a character class matches a backspace
                            }
                        }
                        //stops receving keys once enter is pressed
                        while (key.Key != ConsoleKey.Backspace);
                        password = (Console.ReadLine());
                        Console.WriteLine("What is the subject?");
                        subject = Console.ReadLine();
                        Console.WriteLine("What is the body of your message?");
                        body = Console.ReadLine();
                        Console.WriteLine("Enter email address to send the message.");
                        destination = Console.ReadLine();
                        try
                        {
                            //set-up the smtp client and server
                            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))//587 TLS , 465 SSL or default type of port
                            {
                                client.EnableSsl = true;
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.UseDefaultCredentials = false;
                                client.Credentials = new NetworkCredential(username, password);
                                //mail set-up
                                MailMessage msgObj = new MailMessage();
                                msgObj.From = new MailAddress(username);
                                msgObj.Subject = subject;
                                msgObj.Body = body;
                                msgObj.To.Add(destination);
                                client.Send(msgObj);
                            }
                        }
                        catch (TimeoutException e)
                        {
                            Console.WriteLine(e.Message + "www.google.com!");
                            Console.WriteLine("Error" + e.StackTrace);
                        }
                        catch(System.ArgumentException e)
                        {
                            Console.WriteLine("Error is beacuse: " + e.Message);
                            Console.WriteLine("Search in : " + e.StackTrace);
                        }
                        break;
                        Console.WriteLine("\t Thank you for your time!");
                    case 2:
                        //creating folder in server
                        Console.WriteLine("[1]Enter Y or N to create Folders in a Server!");
                        var user1 = Convert.ToString(Console.ReadLine());
                        if(user1 != "Y" && user1 != "y")
                        {
                            Console.WriteLine("Thank you for your time");
                        }
                        else
                        {
                            FTPBackup ftpBackup = new FTPBackup();
                            ftpBackup.CreateFtpFolder();
                            Console.WriteLine("\t Folders successfully created at the specified path or already exists!");
                        }
                        //uploading file in server
                        Console.WriteLine("[2]Enter Y/N to upload/don't upload files in a erver!");
                        var user2 = Convert.ToString(Console.ReadLine());
                        if(user2 != "Y" && user2 != "y")
                        {
                            Console.WriteLine("Thank you for your time!");
                        }
                        else
                        {
                            FTPBackup ftpBackup = new FTPBackup();
                            ftpBackup.FileToUpload();
                            Console.WriteLine("\t Thank you for your time!");
                        }
                        //deleting folders
                        Console.WriteLine("[3]Enter Y or N if you want to delete folders in a Server!");
                        var user3 = Convert.ToString(Console.ReadLine());
                        if(user3 == "Y" && user3 == "y")
                        {
                            Console.WriteLine("Thank you for your time!");
                        }
                        else
                        {
                            FTPBackup ftpBackup = new FTPBackup();
                            Console.WriteLine("\t Deleting folders...");
                            ftpBackup.DeleteFTPFolder("");
                            Console.WriteLine("Succesfully delted!");
                        }
                        break;
                    case 3:
                        if (quit == true)
                        {
                            //using environment method for exit the console
                            int exitCode = Environment.ExitCode;
                            Console.WriteLine("\t Quitting...");
                            Environment.Exit(exitCode);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}