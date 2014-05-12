using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


//An example of using the StreamWriter and StreamReader classes to write to and read from a file.
namespace StreamWriterReaderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with StreamWriter/StreamReader *****");

            //Get a StreamWriter and write string data.
            using (StreamWriter writer = File.CreateText("reminders.txt"))
            {
                writer.WriteLine("Take out the trash...");
                writer.WriteLine("Update repository...");
                writer.WriteLine("Pay bills...");
                writer.WriteLine("Don't forget these numbers");
                for (int i = 0; i < 10; i++)
                {
                    writer.Write(i + " ");
                }

                //Insert a new line
                writer.Write(writer.NewLine);
            }

            Console.WriteLine("Created file and wrote some thoughts.");

            //Now read data from file.
            Console.WriteLine("Here are your thoughts:\n");
            using (StreamReader sr = File.OpenText("reminders.txt"))
            {
                string input = null;
                while ((input = sr.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
            }

            Console.ReadLine();
        }
    }
}
