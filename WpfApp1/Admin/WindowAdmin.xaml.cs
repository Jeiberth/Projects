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

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        public WindowAdmin()
        {
            InitializeComponent();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            WindowModify windowModify = new WindowModify();
            this.Close();
            windowModify.Show();
        }

        private void See_Click(object sender, RoutedEventArgs e)
        {
            TransactionsAdmin transactionsAdmin = new TransactionsAdmin();
            this.Close();
            transactionsAdmin.Show();

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            this.Close();
            createAccountWindow.Show();
        }

        private void LogOutBottom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
