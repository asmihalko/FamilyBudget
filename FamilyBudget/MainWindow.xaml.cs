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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace FamilyBudget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IncomeManager incomeManager = new IncomeManager();
        ExpenseManager expenseManager = new ExpenseManager();
        TransferManager transferManager = new TransferManager();
        AccountManager accountManager = new AccountManager();

        public MainWindow()
        {
            InitializeComponent();


            accountManager.InitializeData();
            incomeManager.InitializeData();
            expenseManager.InitializeData();
            transferManager.InitializeData();

            dataGridAccounts.ItemsSource = accountManager.Context.Accounts.Local;
            dataGridIncomes.ItemsSource = incomeManager.Context.Operations.Local;
            dataGridExpenses.ItemsSource = expenseManager.Context.Operations.Local;
            dataGridTransfers.ItemsSource = transferManager.Context.Operations.Local;
        }

        private void AddIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            
            AddEditOperationWindow addWindow = new AddEditOperationWindow("income", "add", null);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                addWindow.Close();
        }

        private void AddExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditOperationWindow addWindow = new AddEditOperationWindow("expense", "add", null);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                addWindow.Close();
        }

        private void AddTransferButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditOperationWindow addWindow = new AddEditOperationWindow("transfer", "add", null);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                addWindow.Close();
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditAccount addWindow = new AddEditAccount("add", null);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                addWindow.Close();
        }

        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditAccount addWindow = new AddEditAccount("edit", (Account)dataGridAccounts.SelectedItem);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                addWindow.Close();
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridAccounts.SelectedIndex == -1)
                    throw new Exception("Вы не выбрали ни одного аккаунта");
                Context context = new Context();
                context.Operations.Load();

                foreach (var operation in context.Operations.Local)
                {
                    if (operation.AccountToPutId == ((Account)dataGridAccounts.SelectedItem).Id || operation.AccountToWithdrawId ==
                        ((Account)dataGridAccounts.SelectedItem).Id)
                        throw new Exception("Невозможно удалить аккаунт: т.к. нему привязаны операции");
                }

                accountManager.DeleteAccount((Account)dataGridAccounts.SelectedItem);
            }
            catch(Exception q)
            {
                MessageBox.Show(q.Message, "Ошибка удаления аккаунта", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void EditIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridIncomes.SelectedItem == null) throw new Exception("Выберите объект для редактирования!");
                AddEditOperationWindow addWindow = new AddEditOperationWindow("income", "edit", (Operation)dataGridIncomes.SelectedItem);
                addWindow.ShowDialog();

                if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                    addWindow.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteIncomeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridIncomes.SelectedItem == null) throw new Exception("Выберите объект для удаления!");
                incomeManager.DeleteOperation((Operation)dataGridIncomes.SelectedItem);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void AcceptIncomeFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridExpenses.SelectedItem == null) throw new Exception("Выберите объект для редактирования!");
              
            AddEditOperationWindow addWindow = new AddEditOperationWindow("expense", "edit", (Operation)dataGridExpenses.SelectedItem);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                addWindow.Close();
        }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private void DeleteExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridExpenses.SelectedItem == null) throw new Exception("Выберите объект удаления!");
            
            expenseManager.DeleteOperation((Operation)dataGridExpenses.SelectedItem);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private void AcceptExpenseFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void EditTransferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridTransfers.SelectedItem == null) throw new Exception("Выберите объект для редактирования!");
            
            AddEditOperationWindow addWindow = new AddEditOperationWindow("transfer", "edit", (Operation)dataGridTransfers.SelectedItem);
            addWindow.ShowDialog();

            if (addWindow.DialogResult.HasValue && addWindow.DialogResult.Value == true)
                addWindow.Close();
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private void DeleteTransferButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridTransfers.SelectedItem == null) throw new Exception("Выберите объект для удаления!");
            
            transferManager.DeleteOperation((Operation)dataGridTransfers.SelectedItem);
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private void AcceptTransferFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
