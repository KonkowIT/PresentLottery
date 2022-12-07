using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ListViewItem = System.Windows.Controls.ListViewItem;

namespace PresentLottery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Person> insertList = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            tbName.Focus();
            btn_reset.IsEnabled = false;
        }

        public List<Person> GetList()
        {
            return insertList;
        }

        public List<int> GetAllNumbers(List<Person> personList)
        {
            List<int> numb = new List<int>();
            foreach (Person per in personList)
            {
                numb.Add(per.Number);
            }
            return numb;
        }

        public static void ShowErrorPopUp(string WarningMessage)
        {
            System.Windows.Forms.MessageBox.Show(WarningMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string insertedName = tbName.Text;
            string insertedNumber = tbNumber.Password;
            int insertedNumberParsed;
            List<Person> iList = GetList();
            List<int> alreadyInserted = GetAllNumbers(iList);

            if (string.IsNullOrEmpty(insertedName) || insertedName == "Name")
            {
                ShowErrorPopUp("Please provide a name");
                return;
            }

            try
            {
                insertedNumberParsed = int.Parse(insertedNumber);
                if (insertedNumberParsed < 0)
                {
                    throw new Exception("Provided number is lower than 0");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message == "Input string was not in a correct format." ? "Please provide a correct number" : ex.Message;
                ShowErrorPopUp(msg);
                return;
            }

            if (alreadyInserted.Contains(insertedNumberParsed))
            {
                ShowErrorPopUp("Provided number is already added by somebody else");
                return;
            }

            Person newPerson = new Person(insertedName, insertedNumberParsed);
            iList.Add(newPerson);
            ListViewItem item = new ListViewItem
            {
                Content = newPerson
            };
            lvParticipants.Items.Add(item);
            tbName.Text = string.Empty;
            tbNumber.Password = string.Empty;

            if (btn_reset.IsEnabled == false)
            {
                btn_reset.IsEnabled = true;
            }

            tbName.Focus();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            List<Person> iList = GetList();
            lvParticipants.Items.Clear();
            lvResults.Items.Clear();
            iList.Clear();
            btn_add.IsEnabled = true;
            tbName.IsEnabled = true;
            tbNumber.IsEnabled = true;
            btn_reset.IsEnabled = false;
        }

        private void BtnShuffle_Click(object sender, RoutedEventArgs e)
        {
            if (lvResults.Items.Count > 0)
            {
                ShowErrorPopUp("Please press reset button first");
                return;
            }

            if (insertList.Count < 2)
            {
                ShowErrorPopUp("Please provide at least two persons");
                return;
            }

            btn_add.IsEnabled = false;
            List<Person> iList = GetList();
            List<int> allNumbers = GetAllNumbers(iList);
            List<Person> tempList = new List<Person>(iList);
            int index;

            foreach (Person p in tempList)
            {
                do
                {
                    index = new Random().Next(0, allNumbers.Count);
                }
                while (allNumbers[index] == p.Number);

                Person tempP = new Person(p.Name, allNumbers[index]);
                ListViewItem item = new ListViewItem
                {
                    Content = tempP
                };
                lvResults.Items.Add(item);
                iList.Remove(p);
                allNumbers.RemoveAt(index);
            }

            tbName.IsEnabled = false;
            tbNumber.IsEnabled = false;
        }
    }
}
