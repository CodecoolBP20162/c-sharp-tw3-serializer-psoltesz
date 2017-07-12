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
        public string SerialNumber { get; set; }

        public Person(string name, string address, string phoneNumber, DateTime dateOfRecording)
        {
            Name = name;
            DateOfRecording = dateOfRecording;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("name", typeof(string));
            Address = (string)info.GetValue("address", typeof(string));
            PhoneNumber = (string)info.GetValue("phoneNumber", typeof(string));
            DateOfRecording = (DateTime)info.GetValue("dateOfRecording", typeof(DateTime));
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
            try
            {
                Person deserializedObject = (Person)formatter.Deserialize(stream);
                deserializedObject.SerialNumber = PersonControl.CheckRegexPattern(input);
                stream.Close();
                return deserializedObject;
            }
            catch { stream.Close(); return null; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name, typeof(string));
            info.AddValue("address", Address, typeof(string));
            info.AddValue("phoneNumber", PhoneNumber, typeof(string));
            info.AddValue("dateOfRecording", DateOfRecording, typeof(DateTime));
        }
    }
}
