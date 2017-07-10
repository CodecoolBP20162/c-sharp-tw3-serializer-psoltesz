using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializer
{
    [Serializable]
    public class Person : ISerializable
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfRecording { get; set; }
        public int SerialNumber { get; set; }

        public Person(string name, string address, string phoneNumber, DateTime dateOfRecording)
        {
            Name = name;
            DateOfRecording = dateOfRecording;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
            DateOfRecording = (DateTime)info.GetValue("DateOfRecording", typeof(DateTime));
            // SerialNumber = StreamingContext. has to get serialnumber from filename; todo
        }

        public void Serialize(string output)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
        }

        public static Person Deserialize(string input)
        {
            Stream stream = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            Person deserializedObject = (Person)formatter.Deserialize(stream);
            stream.Close();
            return deserializedObject;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("Address", Name, typeof(string));
            info.AddValue("PhoneNumber", Name, typeof(string));
            info.AddValue("DateOfRecording", DateOfRecording, typeof(DateTime));
        }
    }
}
