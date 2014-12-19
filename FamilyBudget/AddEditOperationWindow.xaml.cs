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
using System.Data.Entity;

namespace FamilyBudget
{
    /// <summary>
    /// Логика взаимодействия для AddEditOperationWindow.xaml
    /// </summary>
    public partial class AddEditOperationWindow : Window
    {
        string operationType, actionType;
        Operation selectedOperation;
        Context context = new Context();
        AccountManager accountManager = new AccountManager();

        public AddEditOperationWindow(string operationType, string actionType, Operation selectedOperation)
        {
            InitializeComponent();

            this.operationType = operationType;
            this.actionType = actionType;

            if (selectedOperation != null)
                this.selectedOperation = selectedOperation;

            comboBoxAccountToWithDraw.ItemsSource = accountManager.Context.Accounts.Local;
            comboBoxAccountToPut.ItemsSource = accountManager.Context.Accounts.Local;

            context.Currencies.Load();
            context.Categories.Load();
            comboBoxCurrency.ItemsSource = context.Currencies.Local;


            switch (operationType)
            {
                case "income":
                    var query = context.Categories
                        .Where(category => category.CategoryType == "income");
                    comboBoxCategory.ItemsSource = query.ToList();

                    comboBoxAccountToWithDraw.Visibility = Visibility.Hidden;
                    textBlockWithdraw.Visibility = Visibility.Hidden;

                    textBlockPut.Margin = new Thickness(0, -10, 0, 0);
                    comboBoxAccountToPut.Margin = new Thickness(15, -28, 0, 0);
                    break;
                case "expense":
                    query = context.Categories
                        .Where(category => category.CategoryType == "expense");
                    comboBoxCategory.ItemsSource = query.ToList();

                    comboBoxAccountToPut.Visibility = Visibility.Hidden;
                    textBlockPut.Visibility = Visibility.Hidden;

                    textBlockSum.Margin = new Thickness(0, -10, 0, 0);
                    textBoxSum.Margin = new Thickness(15, -28, 0, 0);
                    break;
                case "transfer":
                    comboBoxCategory.Visibility = Visibility.Hidden;
                    textBlockCategory.Visibility = Visibility.Hidden;

                    textBlockDate.Margin = new Thickness(0, -10, 0, 0);
                    datePickerDate.Margin = new Thickness(15, -28, 0, 0);
                    break;
            }

            if (actionType == "edit")
            {
                this.DataContext = selectedOperation;
                switch (operationType)
                {
                    case "income":
                        comboBoxCurrency.SelectedItem = context.Currencies.Local.ElementAt(selectedOperation.CurrencyId - 1);
                        comboBoxCategory.SelectedItem = context.Categories.Local.ElementAt((int)selectedOperation.CategoryId - 1);
                        comboBoxAccountToPut.SelectedItem = accountManager.Context.Accounts.Local
                            .Where(acc => acc.Id == (int)selectedOperation.AccountToPutId).Single();
                        break;
                    case "expense":
                        comboBoxCurrency.SelectedItem = context.Currencies.Local.ElementAt(selectedOperation.CurrencyId - 1);
                        comboBoxCategory.SelectedItem = context.Categories.Local.ElementAt((int)selectedOperation.CategoryId - 1);
                        comboBoxAccountToWithDraw.SelectedItem = accountManager.Context.Accounts.Local
                            .Where(acc => acc.Id == (int)selectedOperation.AccountToWithdrawId).Single();
                        break;
                    case "transfer":
                        comboBoxCurrency.SelectedItem = context.Currencies.Local.ElementAt(selectedOperation.CurrencyId - 1);
                        comboBoxAccountToPut.SelectedItem = context.Accounts.Local
                            .Where(acc => acc.Id == (int)selectedOperation.AccountToPutId).Single();
                        comboBoxAccountToWithDraw.SelectedItem = accountManager.Context.Accounts.Local
                            .Where(acc => acc.Id == (int)selectedOperation.AccountToWithdrawId).Single();
                        break;
                }
            }

        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            IOperationManager operationManager;

            switch (operationType)
            {
                case "income":
                    operationManager = new IncomeManager();
                    break;
                case "expense":
                    operationManager = new ExpenseManager();
                   break;
                case "transfer":
                    operationManager = new TransferManager();
                    break;
                default:
                    throw new Exception();
            }

            switch (actionType)
            {
                case "add":
                    operationManager.AddOperation(int.Parse(textBoxSum.Text), DateTime.Parse(datePickerDate.SelectedDate.ToString()), (Account)comboBoxAccountToPut.SelectedItem,
                (Account)comboBoxAccountToWithDraw.SelectedItem, (Currency)comboBoxCurrency.SelectedItem, (Category)comboBoxCategory.SelectedItem, textBoxComment.Text);
                    break;
                case "edit":
                    operationManager.EditOperation(selectedOperation, int.Parse(textBoxSum.Text), DateTime.Parse(datePickerDate.SelectedDate.ToString()), (Account)comboBoxAccountToPut.SelectedItem,
                (Account)comboBoxAccountToWithDraw.SelectedItem, (Currency)comboBoxCurrency.SelectedItem, (Category)comboBoxCategory.SelectedItem, textBoxComment.Text);
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
