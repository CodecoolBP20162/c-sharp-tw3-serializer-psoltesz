using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Serializer
{
    class PersonControl
    {
        public List<Person> persons = new List<Person>();
        private string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Data\");
        private Stack<int> stack = new Stack<int>();
        private int indexPointer = 0;

        public void DeserializeToList()
        {
            string[] files = Directory.GetFiles(path);
            foreach (string fileName in files)
            {
                Person person = Person.Deserialize(fileName);
                persons.Add(person);
            }
        }

        public int DetermineHighestNumberInFilename()
        {
            string[] files = Directory.GetFiles(path);
            List<int> numbers = new List<int>();
            try
            {
                foreach (string fileName in files)
                {
                    numbers.Add(Convert.ToInt32(CheckRegexPattern(fileName)));
                }
                return numbers.Max();
            }
            catch { return 0; }
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
            int limit = DetermineHighestNumberInFilename();
            for (int i = 99; i > limit; i--)
            {
                stack.Push(i);
            }
        }

        public void SavePerson(string name, string address, string phone)
        {
            Person person = new Person(name, address, phone, DateTime.Now);
            person.Serialize(BuildPersonPath());
            persons.Add(person);
        }

        public int StepForward()
        {
            if (indexPointer != persons.Count - 1)
            {
                indexPointer++;
            }
            return indexPointer;
        }

        public int StepBack()
        {
            if (indexPointer != 0)
            {
                indexPointer--;
            }
            return indexPointer;
        }

        private string GetNextSerialNumber()
        {
            int number = stack.Pop();
            if (number < 10)
            {
                return "0" + number;
            }
            return number.ToString();
        }

        private string BuildPersonPath()
        {
            string pathToPerson = path + "person" + GetNextSerialNumber() + ".dat";
            return pathToPerson;
        }

        public static string CheckRegexPattern(string input)
        {
            string result;
            string re1 = ".*?";
            string re2 = "\\d+";
            string re3 = ".*?";
            string re4 = "(\\d+)";

            Regex r = new Regex(re1 + re2 + re3 + re4, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(input);
            return result = m.Groups[1].ToString();
        }
    }
}
