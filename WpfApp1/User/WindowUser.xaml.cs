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
    /// Lógica de interacción para WindowUser.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        public WindowUser()
        {
            InitializeComponent();
        }

        private void See_Click(object sender, RoutedEventArgs e)
        {
            GetInfo info = new GetInfo();
            this.Close();
            info.Show();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            PersonalWindow personalWindow = new PersonalWindow();
            this.Close();
            personalWindow.Show();
        }

        private void LogOutBottom_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
