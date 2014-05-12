using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//necessary for the BinaryFormatter class usage.
using System.Runtime.Serialization.Formatters.Binary;

/*A simple serialization example*/

namespace SerializationExampleSimple
{
    //the data that we want persisted to an external file
    [Serializable]
    public class UserPrefs
    {
        public string WindowColor;
        public int FontSize;
    }

    class Program
    {
        static void Main(string[] args)
        {
            UserPrefs userData = new UserPrefs();
            userData.WindowColor = "Yellow";
            userData.FontSize = 50;

            /*The BinaryFormatter persists state data in a binary format.*/
            BinaryFormatter binFormat = new BinaryFormatter();

            //store the object in a local file
            using (Stream fStream = new FileStream("user.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, userData);
            }

            Console.ReadLine();
        }
    }
}
