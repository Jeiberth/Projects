using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppMSSQL
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

        static SqlConnection con; // It is the connection adapter
        static SqlCommand cmd; // it the query proceeding adapter
        private void connection_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True");
            con.Open();
            MessageBox.Show("Connection Established");
            con.Close();
        }

        private void insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //step 1: Is to open the connection
                con.Open();
                //step 2: generate the database query
                string Query = "insert into Orders values(@p_name, @p_id, @p_amount, @p_price)";
                // step 3: Create the command for Database
                cmd = new SqlCommand(Query, con);
                // step 4: Assign values to the query variables 
                cmd.Parameters.AddWithValue("@p_name", p_name.Text);
                cmd.Parameters.AddWithValue("@p_id", p_id.Text);
                cmd.Parameters.AddWithValue("@p_amount", float.Parse(p_amount.Text));
                cmd.Parameters.AddWithValue("@p_price", float.Parse(p_price.Text));

                // step 5: Execute the Command/Query
                cmd.ExecuteNonQuery();
                // step 6: Successful Message
                MessageBox.Show("Insertion is Successful");
                // step 7: Close the connection
                con.Close();
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void show_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // step 1: Open the connection
                con.Open();
                // step 2: Create the select Query
                string Query = "select * from Orders";
                // step 3: Create the command to execute
                cmd = new SqlCommand(Query, con);
                // step 4 : Prepare the data for datagrid
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // step 5 : Update the dataGrid Itemsource
                dbGrid.ItemsSource = dt.AsDataView();
                // step 6: Bind the data in the wpf frontend
                DataContext = da;
                // step 7: Close the connection
                con.Close();

            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void select_Click(object sender, RoutedEventArgs e)
        {
            // Search Student information
            try
            {
                // step 1: Open the Connection
                con.Open();
                // step 2: Generate the Query
                string Query = "Select * from Orders where p_id=@p_id";
                // Step 3: Generate the Command for SQL
                cmd = new SqlCommand(Query, con);

                // Pass the ID parameter value
                cmd.Parameters.AddWithValue("@p_id", p_id.Text);

                // Step 4: Creating the Dataadapter to get the values properly
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Step 5: We are going to fill the textboxes with the retrieved
                // information

                if(dt.Rows != null)
                {
                    p_name.Text = dt.Rows[0]["p_name"].ToString();
                    p_amount.Text = dt.Rows[0]["p_amount"].ToString();
                    p_price.Text = dt.Rows[0]["p_price"].ToString();
                }
                else
                {
                    MessageBox.Show("Order hasn't registered yet");
                }

                // step 6: Close Connection
                con.Close();

            } catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            // Update the information from the WPF 
            try
            {
                // step 1: Open the Connection
                con.Open();
                // Step 2: Generate the Query
                string Query = "Update Orders set p_name=@p_name, " +
                    "p_price=@p_price, p_amount=@p_amount where p_id=@p_id";
                // step 3: generate the SQL command
                cmd = new SqlCommand(Query, con);
                // step 4: passing the update values
                cmd.Parameters.AddWithValue("@p_name", p_name.Text);
                cmd.Parameters.AddWithValue("@p_id", p_id.Text);
                cmd.Parameters.AddWithValue("@p_amount", float.Parse(p_amount.Text));
                cmd.Parameters.AddWithValue("@p_price", float.Parse(p_price.Text));
                // step 5: Executing the Query/Command
                int i= cmd.ExecuteNonQuery();
                // if cmd executed successfull, it return 1, else 0
                if (i == 1)
                {
                    // step 6: Message to user
                    MessageBox.Show("Information Updated");
                }
                //step 7: Close the Connection
                con.Close();
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Step 1: Open the Connection
                con.Open();
                // Step 2: Generate the Query
                string Query = "Delete from Orders where p_id=@p_id";
                // Step 3: Generate the Command
                cmd = new SqlCommand(Query, con);
                // Step 4: Pass the Parameter's value
                cmd.Parameters.AddWithValue("@p_id", p_id.Text);
                // step 5: Execute the Query
                int i= cmd.ExecuteNonQuery();

                if (i == 1)
                {
                    MessageBox.Show("Entry deleted");
                }
                else
                {
                    MessageBox.Show("Check the ID properly");
                }
                // step 6: Close the Connection
                con.Close();
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void sales_Click(object sender, RoutedEventArgs e)
        {
            Sales sal = new Sales();
            this.Close();
            sal.Show();
        }
    }
}
