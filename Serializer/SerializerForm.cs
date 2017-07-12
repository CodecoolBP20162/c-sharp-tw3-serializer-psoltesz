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
            personControl.DeserializeToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(0));
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(personControl.StepBack()));
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(personControl.StepForward()));
        }

        private void firstButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(0));
        }

        private void lastButton_Click(object sender, EventArgs e)
        {
            FillContent(personControl.GetEntryContent(personControl.persons.Count - 1));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            personControl.SavePerson(nameTextBox.Text, addressTextBox.Text, phoneTextBox.Text);
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
