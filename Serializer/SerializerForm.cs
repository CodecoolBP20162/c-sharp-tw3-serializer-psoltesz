using System;
using System.Windows.Forms;

namespace Serializer
{
    public partial class SerializerForm : Form
    {
        PersonControl personControl= new PersonControl();

        public SerializerForm()
        {
            InitializeComponent();
            personControl.FillStack();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            personControl.DeserializeToList();
            FillContent(personControl.GetEntryContent(0));
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(0));
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(0));
        }

        private void firstButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(0));
        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(personControl.persons.Count));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Person person = new Person(nameTextBox.Text, addressTextBox.Text, phoneTextBox.Text, DateTime.Now);
            person.Serialize(personControl.path + personControl.GetNextSerialNumber());
        }

        private void FillContent(Person person)
        {
            if (person != null)
            {
                nameTextBox.Text = person.Name;
                addressTextBox.Text = person.Address;
                phoneTextBox.Text = person.PhoneNumber;
            }
        }
    }
}
