using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Lógica de interacción para CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        public CreateAccountWindow()
        {
            InitializeComponent();
        }

        static SqlConnection con;
        static SqlCommand cmd;
        private int admin = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True");
            try
            {
                //Step 1: Is to open the connection
                con.Open();

                //Step 2: generate the database query
                String Query = "Insert into PERSON values(@ID, @FULL_NAME, @AGE, @EMAIL, @AMOUNT, @PASSWORD, @ADMIN)"; //inside the query with put the variables
                //sql command line variables which basically function as an in between variable
                //for assigning value from the user input to the sql databse

                //Step 3: Create the command for Database
                cmd = new SqlCommand(Query, con);

                //Step 4: Assign value to the variables
                cmd.Parameters.AddWithValue("@ID", text_id.Text);
                cmd.Parameters.AddWithValue("@FULL_NAME", text_name.Text);
                cmd.Parameters.AddWithValue("@AGE", int.Parse(text_age.Text));
                cmd.Parameters.AddWithValue("@EMAIL", text_email.Text);
                cmd.Parameters.AddWithValue("@AMOUNT", float.Parse(text_amount.Text));
                cmd.Parameters.AddWithValue("@PASSWORD", text_password.Text);
                cmd.Parameters.AddWithValue("@ADMIN", admin);

                //step 5: Execute the Command/Query
                cmd.ExecuteNonQuery();
                //Step 6: Successful Message
                MessageBox.Show("Insertion is Succesful");
                //Step 7: Close the connection
                con.Close();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            WindowAdmin windowAdmin = new WindowAdmin();
            this.Close();
            windowAdmin.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            admin = 1;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            admin = 0;
        }

        private void LogOutBottom_Click(object sender, RoutedEventArgs e)
        {
            WindowAdmin windowAdmin = new WindowAdmin();
            this.Close();
            windowAdmin.Show();
        }
    }
}
