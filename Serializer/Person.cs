using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

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
            Name = (string)info.GetValue("Name", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            PhoneNumber = (string)info.GetValue("PhoneNumber", typeof(string));
            DateOfRecording = (DateTime)info.GetValue("DateOfRecording", typeof(DateTime));
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
            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "(9)"; // Any Single Character 1
            string re3 = "(2)"; // Any Single Character 2

            Regex r = new Regex(re1 + re2 + re3, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(input);
            deserializedObject.SerialNumber = m.Groups[1].ToString() + m.Groups[2].ToString();
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
