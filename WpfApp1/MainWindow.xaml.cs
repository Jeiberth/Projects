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
using System.Data;
using System.Data.SqlClient;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /*
        static SqlConnection con;
        static SqlCommand cmd;
        */
        public static string id_login;
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            id_login = text_id.Text;
            LogInCode.Log(this);
           /*
            con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True");

            try
            {
                // Step 1: Open the connection
                con.Open();

                // Step 2: Create the select Query with parameters
                String Query = "SELECT age FROM person WHERE PASSWORD_=@Password AND ID=@ID";

                // Step 3: Create the command to execute
                cmd = new SqlCommand(Query, con);

                // Step 4: Set parameter values
                cmd.Parameters.AddWithValue("@Password", text_password.Text);
                cmd.Parameters.AddWithValue("@ID", text_id.Text);

                // Step 5: Prepare the data for the DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();

                // Step 6: Fill the DataSet
                da.Fill(dataSet);

                // Step 7: Check if there are rows in the DataSet
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    id_login = text_id.Text;

                    //Try 1
                    try
                    {

                        // Step 2: Create the select Query with parameters
                        Query = "SELECT * FROM person WHERE PASSWORD_=@Password AND ID=@ID AND ADMIN=1";

                        // Step 3: Create the command to execute
                        cmd = new SqlCommand(Query, con);

                        cmd.Parameters.AddWithValue("@Password", text_password.Text);
                        cmd.Parameters.AddWithValue("@ID", text_id.Text);

                        // Step 5: Prepare the data for the DataSet
                        da = new SqlDataAdapter(cmd);
                        dataSet = new DataSet();

                        // Step 6: Fill the DataSet
                        da.Fill(dataSet);

                        // Step 7: Check if there are rows in the DataSet
                        if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                        {
                            id_login = text_id.Text;
                            WindowAdmin windowAdmin = new WindowAdmin();
                            this.Close();
                            windowAdmin.Show();
                        }
                        else
                        {
                            id_login = text_id.Text;
                            WindowUser windowUser = new WindowUser();
                            this.Close();
                            windowUser.Show();
                        }

                        // Step 8: Close the connection
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    //try 1
                }
                else
                {
                    MessageBox.Show("The ID and/or password are wrong");
                }

                // Step 8: Close the connection
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
           */
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateAccountWindow createAccountWindow = new CreateAccountWindow();
            this.Close();
            createAccountWindow.Show();
        }

       
    }
}
