using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Demonstrating the uses of the StringWriter and StringReader classes.
namespace StringReaderWriterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with StringWriter/StringReader ******\n");

            //Create a StringWriter and emit character data to memory.
            using (StringWriter strWriter = new StringWriter())
            {
                strWriter.WriteLine("Don't forget to pay the bills...");
                //Get a copy of the contents (stored in a string) and output to the console.
                Console.WriteLine("Contents of StringWriter:\n {0}", strWriter);
            }
            Console.ReadLine();
        }
    }
}
