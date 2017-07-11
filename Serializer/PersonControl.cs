using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serializer
{
    class PersonControl
    {
        public string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Data\");
        public List<Person> persons = new List<Person>();
        public Stack<int> stack = new Stack<int>();

        public void DeserializeToList()
        {
            string[] files = Directory.GetFiles(path);
            foreach (string fileName in files)
            {
                Person person = Person.Deserialize(fileName);
                persons.Add(person);
            }
        }

        public Person GetEntryContent(int index)
        {
            try
            {
                return persons.ElementAt(index);
            }
            catch { return null; }
        }

        public void FillStack()
        {
            for (int i = 99; i >= 0; i--)
            {
                stack.Push(i);
            }
        }

        public string GetNextSerialNumber()
        {
            int number = stack.Pop();
            if (number < 10)
            {
                return "0" + number;
            }
            return number.ToString();
        }
    }
}
