using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Runtime.Serialization.Formatters.Binary;   //BinaryFormatter
using System.Runtime.Serialization.Formatters.Soap;     //SoapFormatter
using System.Xml.Serialization;     //XmlSerializer



/*This simple console application demonstates the use of the BinaryFormatter, SoapFormatter, and XmlSerializer
 types in serializing and deserializing data.*/

namespace MultiFormatSerialization
{
    //simple classes to diplay the use of the various serializers

    [Serializable]
    public class Radio
    {
        public bool hasTweeters;
        public bool hasSubWoofers;
        public double[] stationPresets;

        [NonSerialized]
        public string radioID = "XF-552RR6";
    }

    [Serializable]
    public class Car
    {
        public Radio theRadio = new Radio();
        public bool isHatchBack;
    }

    [Serializable]
    public class JamesBondCar : Car
    {
        public bool canFly;
        public bool canSubmerge;
    }



    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Object Serialization ******");

            //create a JamesBondCar and set its state.
            JamesBondCar jbc = new JamesBondCar();
            jbc.canFly = true;
            jbc.canSubmerge = false;
            jbc.theRadio.stationPresets = new double[] { 89.3, 95.5, 88.1 };
            jbc.theRadio.hasTweeters = true;

            //Now save the car to a specific file in binary format.
            SaveAsBinaryFormat(jbc, "CarData.dat");
            SaveAsSoapFormat(jbc, "CarDataSOAP.soap");
            SaveAsXMLFormat(jbc, "CarDataXML.xml");

            LoadFromBinaryFile("CarData.dat");
            Console.ReadLine();
        }

        //Serialize the data and persist it to a file in a binary format using the BinaryFormatter type.
        static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("=> Saved car in binary format.");
        }

        //Deserialize the binary data located within the file and read back the contents using the BinaryFormatter type.
        static void LoadFromBinaryFile(string fileName) 
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = File.OpenRead(fileName))
            {
                JamesBondCar carFromDisk = (JamesBondCar)binFormat.Deserialize(fStream);
                Console.WriteLine("Can this car fly? : {0}", carFromDisk.canFly);
            }
        }

        //Serialize the data and persist it to a file in a SOAP format using the SoapFormatter type.
        static void SaveAsSoapFormat(object objGraph, string fileName)
        {
            SoapFormatter soapFormat = new SoapFormatter();

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("=> Saved car in SOAP format.");
        }

        //Serialize the data and persist it to a file in a binary format using the BinaryFormatter type.
        static void SaveAsXMLFormat(object objGraph, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(JamesBondCar));

            using (Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, objGraph);
            }
            Console.WriteLine("=> Saved car in xml format.");
        }
    }
}
