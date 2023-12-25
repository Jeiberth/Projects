using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Lógica de interacción para PersonalWindow.xaml
    /// </summary>
    public partial class PersonalWindow : Window
    {
        public PersonalWindow()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True");

            try
            {
                // Step 1: Open the connection
                con.Open();

                // Step 2: Create the first select query
                string query1 = "select * from PERSONAL_TRANSACTIONS where ID='" + MainWindow.id_login+"'";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                // Step 3: Update the first DataGrid ItemSource
                dbGrid.ItemsSource = dt1.AsDataView();

                /*
                // Step 4: Create the second select query
                string query2 = "select * from person where ID=" + MainWindow.id_login;
                SqlCommand cmd2 = new SqlCommand(query2, con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                // Step 5: Update the second DataGrid ItemSource
                dbGridData.ItemsSource = dt2.AsDataView();

                */

                // Step 6: Close the connection
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }

            try
            {
                // step 1: Open the Connection
                con.Open();
                // step 2: Generate the Query
                string Query = "Select AMOUNT from person where ID='" + MainWindow.id_login+"'";
                // Step 3: Generate the Command for SQL
                cmd = new SqlCommand(Query, con);

                // Step 4: Creating the Dataadapter to get the values properly
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Step 5: We are going to fill the textboxes with the retrieved
                // information
                try
                {
                    if (dt.Rows != null)
                    {
                        text_available.Text = dt.Rows[0]["AMOUNT"].ToString();
                        amount = float.Parse(text_available.Text);
                    }
                    else
                    {
                        MessageBox.Show("The ID does not match with any user");
                    }
                }
                catch (Exception a) { MessageBox.Show("The ID does not match with any user"); }

                // step 6: Close Connection
                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        static SqlConnection con;
        static SqlCommand cmd;
        private float amount;
        private float amountr;
        private string h;

        private void LogOutBottom_Click(object sender, RoutedEventArgs e)
        {
            WindowUser windowUser = new WindowUser();
            this.Close();
            windowUser.Show();
        }

        private void dbGridData_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (amount >= float.Parse(text_amount.Text))
            {

                try
                {
                    // step 1: Open the Connection
                    con.Open();
                    // step 2: Generate the Query
                    string Query = "Select AMOUNT from person where ID='" + text_receiver.Text+"'";
                    // Step 3: Generate the Command for SQL
                    cmd = new SqlCommand(Query, con);

                    // Step 4: Creating the Dataadapter to get the values properly
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Step 5: We are going to fill the textboxes with the retrieved
                    // information
                    try
                    {
                        if (dt.Rows != null)
                        {
                            h = dt.Rows[0]["AMOUNT"].ToString();
                            amountr = float.Parse(h);
                        }
                        else
                        {
                            MessageBox.Show("The ID does not match with any user");
                            con.Close();
                            return;
                        }
                    }
                    catch (Exception a) { MessageBox.Show("The ID does not match with any user"); con.Close();  return; }

                    // step 6: Close Connection
                    con.Close();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }



                // Update the information from the WPF 
                try
                {
                    // step 1: Open the Connection
                    con.Open();
                    // Step 2: Generate the Query
                    string Query = "Update person set AMOUNT=@AMOUNT where ID='" + text_receiver.Text+"'";
                    // step 3: generate the SQL command
                    cmd = new SqlCommand(Query, con);
                    // step 4: passing the update values
                    cmd.Parameters.AddWithValue("@AMOUNT", float.Parse(text_amount.Text)+amountr);

                    // step 5: Executing the Query/Command
                    int i = cmd.ExecuteNonQuery();
                    // if cmd executed successfull, it return 1, else 0
                    if (i == 1)
                    {
                        // step 6: Message to user
                        MessageBox.Show("Information Updated");
                    }
                    //step 7: Close the Connection
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Update the information from the WPF 
                try
                {
                    // step 1: Open the Connection
                    con.Open();
                    // Step 2: Generate the Query
                    string Query = "Update person set AMOUNT=@AMOUNT where ID='" + MainWindow.id_login+"'";
                    // step 3: generate the SQL command
                    cmd = new SqlCommand(Query, con);
                    // step 4: passing the update values
                    cmd.Parameters.AddWithValue("@AMOUNT", amount-float.Parse(text_amount.Text));

                    // step 5: Executing the Query/Command
                    int i = cmd.ExecuteNonQuery();
                    // if cmd executed successfull, it return 1, else 0
                    if (i == 1)
                    {
                        // step 6: Message to user
                        MessageBox.Show("Information Updated");
                    }
                    //step 7: Close the Connection
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }


                try
                {
                    //Step 1: Is to open the connection
                    con.Open();

                    //Step 2: generate the database query
                    String Query = "Insert into PERSONAL_TRANSACTIONS values(@ID, @TRANSACTION_, @AMOUNT, @ID_OTHER_ACCOUNT_INVOLVED)"; 

                    //Step 3: Create the command for Database
                    cmd = new SqlCommand(Query, con);

                    //Step 4: Assign value to the variables
                    cmd.Parameters.AddWithValue("@ID", MainWindow.id_login);
                    cmd.Parameters.AddWithValue("@TRANSACTION_", "Sending");
                    cmd.Parameters.AddWithValue("@AMOUNT", int.Parse(text_amount.Text));
                    cmd.Parameters.AddWithValue("@ID_OTHER_ACCOUNT_INVOLVED", text_receiver.Text);

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

                try
                {
                    //Step 1: Is to open the connection
                    con.Open();

                    //Step 2: generate the database query
                    String Query = "Insert into PERSONAL_TRANSACTIONS values(@ID, @TRANSACTION_, @AMOUNT, @ID_OTHER_ACCOUNT_INVOLVED)";

                    //Step 3: Create the command for Database
                    cmd = new SqlCommand(Query, con);

                    //Step 4: Assign value to the variables
                    cmd.Parameters.AddWithValue("@ID", text_receiver.Text);
                    cmd.Parameters.AddWithValue("@TRANSACTION_", "Reciving");
                    cmd.Parameters.AddWithValue("@AMOUNT", int.Parse(text_amount.Text));
                    cmd.Parameters.AddWithValue("@ID_OTHER_ACCOUNT_INVOLVED", MainWindow.id_login);

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

                try
                {
                    // Step 1: Open the connection
                    con.Open();

                    // Step 2: Create the first select query
                    string query1 = "select * from PERSONAL_TRANSACTIONS where ID='" + MainWindow.id_login+"'";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    // Step 3: Update the first DataGrid ItemSource
                    dbGrid.ItemsSource = dt1.AsDataView();

                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    // step 1: Open the Connection
                    con.Open();
                    // step 2: Generate the Query
                    string Query = "Select AMOUNT from person where ID='" + MainWindow.id_login+"'";
                    // Step 3: Generate the Command for SQL
                    cmd = new SqlCommand(Query, con);

                    // Step 4: Creating the Dataadapter to get the values properly
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Step 5: We are going to fill the textboxes with the retrieved
                    // information
                    try
                    {
                        if (dt.Rows != null)
                        {
                            text_available.Text = dt.Rows[0]["AMOUNT"].ToString();
                            amount = float.Parse(text_available.Text);
                        }
                        else
                        {
                            MessageBox.Show("The ID does not match with any user");
                        }
                    }
                    catch (Exception a) { MessageBox.Show("The ID does not match with any user"); }

                    // step 6: Close Connection
                    con.Close();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                text_amount.Text = string.Empty;
                text_receiver.Text = string.Empty;

            }
            else
                MessageBox.Show("You do not have enough money for make this transaction");

        }

        private void text_available_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
