using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

/*Exploring custom serialization using ISerializable.  The program serializes the string data
 as all uppercase, and deserializes it to all lowercase.*/

namespace CustomSerialization
{
    [Serializable]
    public class StringData : ISerializable
    {
        private string dataItemOne = "First data block";
        private string dataItemTwo = "More data";

        public StringData() { }

        //protected constructor required when using ISerializable
        //Called when the stream is deserialized.
        protected StringData(SerializationInfo si, StreamingContext ctx)
        {
            //Rehydrate member variables from stream
            dataItemOne = si.GetString("First_Item").ToLower();
            dataItemTwo = si.GetString("dataItemTwo").ToLower();
        }

        //Called when to object is being serialized
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            //Fill up the SerializationInfo object with the formatted data.
            info.AddValue("First_Item", dataItemOne.ToUpper());
            info.AddValue("dataItemTwo", dataItemTwo.ToUpper());
        }

    }

    //This class does serialization attributes ONLY.
    [Serializable]
    class MoreData
    {
        private string dataItemOne = "Movies";
        private string dataItemTwo = "Popcorn";

        [OnSerializing] //called BEFORE the serialization process.
        private void OnSeralizing(StreamingContext context)
        {
            //Called during the serialization process
            dataItemOne = dataItemOne.ToUpper();
            dataItemTwo = dataItemTwo.ToUpper();
        }

        [OnDeserialized] //called immediately AFTER the Deserialization process.
        private void OnDeseralized(StreamingContext context)
        {
            //Called once the deserialization process is complete
            dataItemOne = dataItemOne.ToLower();
            dataItemTwo = dataItemTwo.ToLower();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Custom Serializationg ******\n");

            StringData myData = new StringData();

            MoreData moreData = new MoreData();

            //save to a local file in SOAP format
            SoapFormatter soapFormat = new SoapFormatter();

            using (Stream fStream = new FileStream("MyData.soap", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(fStream, myData);
            }

            SoapFormatter soapData2 = new SoapFormatter();
            using (Stream fStream = new FileStream("MoreData.soap", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapData2.Serialize(fStream, moreData);
            }

            Console.ReadLine();

        }
    }
}
