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
    /// Lógica de interacción para TransactionsAdmin.xaml
    /// </summary>
    public partial class TransactionsAdmin : Window
    {
        public TransactionsAdmin()
        {
            InitializeComponent();
            InitializeComponent();
            con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True");

            try
            {
                // Step 1: Open the connection
                con.Open();

                // Step 2: Create the first select query
                string query1 = "select * from PERSONAL_TRANSACTIONS";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                // Step 3: Update the first DataGrid ItemSource
                dbGrid_TRANSACTIONS.ItemsSource = dt1.AsDataView();
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
                string query1 = "select * from person";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                // Step 3: Update the first DataGrid ItemSource
                dbGrid_ACCOUNTS.ItemsSource = dt1.AsDataView();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        static SqlConnection con;
        static SqlCommand cmd;
        private float FROM_;
        private float TO_;
        private string h;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //b

            

                try
                {
                    // step 1: Open the Connection
                    con.Open();
                    // step 2: Generate the Query
                    string Query = "Select AMOUNT from person where ID='" +FROM.Text+"'";
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
                            FROM_ = float.Parse(h);
                        }
                        else
                        {
                            MessageBox.Show("The ID of the sender does not match with any user");
                            con.Close();
                            return;
                        }
                    }
                    catch (Exception a) { MessageBox.Show("The ID of the sender does not match with any user"); con.Close(); return; }

                    // step 6: Close Connection
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
                string Query = "Select AMOUNT from person where ID='" + TO.Text + "'";
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
                        TO_ = float.Parse(h);
                    }
                    else
                    {
                        MessageBox.Show("The ID of the receiver does not match with any user");
                        con.Close();
                        return;
                    }
                }
                catch (Exception a) { MessageBox.Show("The ID of the receiver does not match with any user"); con.Close(); return; }

                // step 6: Close Connection
                con.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (float.Parse(AMOUNT.Text) > FROM_)
            {
                MessageBox.Show("The sender does not have enough money");
                return;
            }

            // Update the information from the WPF 
            try
                {
                    // step 1: Open the Connection
                    con.Open();
                // Step 2: Generate the Query
                string Query = "Update person set AMOUNT=@AMOUNT where ID='" + FROM.Text + "'";
                    // step 3: generate the SQL command
                    cmd = new SqlCommand(Query, con);
                    // step 4: passing the update values
                    cmd.Parameters.AddWithValue("@AMOUNT", FROM_ - float.Parse(AMOUNT.Text));

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
                // step 1: Open the Connection
                con.Open();
                // Step 2: Generate the Query
                string Query = "Update person set AMOUNT=@AMOUNT where ID='" + TO.Text+"'";
                // step 3: generate the SQL command
                cmd = new SqlCommand(Query, con);
                // step 4: passing the update values
                cmd.Parameters.AddWithValue("@AMOUNT", TO_ + float.Parse(AMOUNT.Text));

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
                    cmd.Parameters.AddWithValue("@ID", FROM.Text);
                    cmd.Parameters.AddWithValue("@TRANSACTION_", "Sending");
                    cmd.Parameters.AddWithValue("@AMOUNT", int.Parse(AMOUNT.Text));
                    cmd.Parameters.AddWithValue("@ID_OTHER_ACCOUNT_INVOLVED", TO.Text);

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
                    cmd.Parameters.AddWithValue("@ID", TO.Text);
                    cmd.Parameters.AddWithValue("@TRANSACTION_", "Reciving");
                    cmd.Parameters.AddWithValue("@AMOUNT", int.Parse(AMOUNT.Text));
                    cmd.Parameters.AddWithValue("@ID_OTHER_ACCOUNT_INVOLVED", FROM.Text);

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
                    string query1 = "select * from PERSONAL_TRANSACTIONS";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);

                    // Step 3: Update the first DataGrid ItemSource
                    dbGrid_TRANSACTIONS.ItemsSource = dt1.AsDataView();

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
                string query1 = "select * from person";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                // Step 3: Update the first DataGrid ItemSource
                dbGrid_ACCOUNTS.ItemsSource = dt1.AsDataView();

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


            FROM.Text = string.Empty;
                TO.Text = string.Empty;
                AMOUNT.Text = string.Empty;


        //f

    }

        private void LogOutBottom_Click(object sender, RoutedEventArgs e)
        {
            WindowAdmin windowAdmin = new WindowAdmin();
            this.Close();
            windowAdmin.Show();
        }
    }
}
