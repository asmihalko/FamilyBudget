using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FamilyBudget
{
    /// <summary>
    /// Логика взаимодействия для AddEditAccount.xaml
    /// </summary>
    public partial class AddEditAccount : Window
    {
        AccountManager accountManager = new AccountManager();
        string actionType;
        Account selectedAccount;
        public AddEditAccount(string actionType, Account selectedAccount)
        {
            this.actionType = actionType;
            this.selectedAccount = selectedAccount;
            InitializeComponent();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            switch(actionType)
            {
                case "add":
                    accountManager.AddAccount(textBoxAccountName.Text);
                    break;
                case "edit":
                    accountManager.EditAccount(selectedAccount, textBoxAccountName.Text);
                    break;
            }

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
