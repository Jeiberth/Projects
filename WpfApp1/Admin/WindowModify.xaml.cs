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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para WindowModify.xaml
    /// </summary>
    public partial class WindowModify : Window
    {
        public WindowModify()
        {
            InitializeComponent();

        }

        static SqlConnection con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True"); // It is the connection adapter
        static SqlCommand cmd; // it the query proceeding adapter
        private int admin = 0;
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            // Search Student information
            try
            {
                // step 1: Open the Connection
                con.Open();
                // step 2: Generate the Query
                string Query = "Select * from person where ID=@id";
                // Step 3: Generate the Command for SQL
                cmd = new SqlCommand(Query, con);

                // Pass the ID parameter value
                cmd.Parameters.AddWithValue("@id", text_id.Text);

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
                        text_id.Text = dt.Rows[0]["ID"].ToString();
                        text_name.Text = dt.Rows[0]["FULL_NAME"].ToString();
                        text_age.Text = dt.Rows[0]["AGE"].ToString();
                        text_email.Text = dt.Rows[0]["EMAIL"].ToString();
                        text_amount.Text = dt.Rows[0]["AMOUNT"].ToString();
                        text_password.Text = dt.Rows[0]["PASSWORD_"].ToString();
                        string ad = dt.Rows[0]["admin"].ToString();
                        if (ad == "1")
                            checkbox_admin.IsChecked = true;
                        else
                            checkbox_admin.IsChecked = false;

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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            admin = 1;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            admin = 0;
        }

        
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            
            // Update the information from the WPF 
            try
            {
                // step 1: Open the Connection
                con.Open();
                // Step 2: Generate the Query
                string Query = "Update person set ID=@ID, admin=@ADMIN, " +
                    "FULL_NAME=@FULL_NAME, AGE=@AGE, EMAIL=@EMAIL, AMOUNT=@AMOUNT, PASSWORD_=@PASSWORD where ID=@ID";
                // step 3: generate the SQL command
                cmd = new SqlCommand(Query, con);
                // step 4: passing the update values

                cmd.Parameters.AddWithValue("@ID", text_id.Text);
                cmd.Parameters.AddWithValue("@FULL_NAME", text_name.Text);
                cmd.Parameters.AddWithValue("@AGE", int.Parse(text_age.Text));
                cmd.Parameters.AddWithValue("@EMAIL", text_email.Text);
                cmd.Parameters.AddWithValue("@AMOUNT", float.Parse(text_amount.Text));
                cmd.Parameters.AddWithValue("@PASSWORD", text_password.Text);
                cmd.Parameters.AddWithValue("@ADMIN", admin);

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
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                // Step 1: Open the Connection
                con.Open();
                // Step 2: Generate the Query
                string Query = "Delete from PERSON where ID=@ID";
                // Step 3: Generate the Command
                cmd = new SqlCommand(Query, con);
                // Step 4: Pass the Parameter's value
                cmd.Parameters.AddWithValue("@ID", text_id.Text);
                // step 5: Execute the Query
                int i = cmd.ExecuteNonQuery();

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
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void LogOutBottom_Click(object sender, RoutedEventArgs e)
        {
            WindowAdmin windowAdmin = new WindowAdmin();
            this.Close();
            windowAdmin.Show();
        }
    }
}
