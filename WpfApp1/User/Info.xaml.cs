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

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Info.xaml
    /// </summary>
    public partial class GetInfo : Window
    {
        
        static SqlConnection con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True"); // It is the connection adapter
        static SqlCommand cmd; // it the query proceeding adapter
        private int admin = 0;
        
        public GetInfo()
        {
            InitializeComponent();

            
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
                cmd.Parameters.AddWithValue("@id", MainWindow.id_login);

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
                        ID.Content = dt.Rows[0]["ID"].ToString();
                        FULL_NAME.Content = dt.Rows[0]["FULL_NAME"].ToString();
                        AGE.Content = dt.Rows[0]["AGE"].ToString();
                        EMAIL.Content = dt.Rows[0]["EMAIL"].ToString();
                        AMOUNT.Content = dt.Rows[0]["AMOUNT"].ToString();
                        PASSWORD_.Content = dt.Rows[0]["PASSWORD_"].ToString();
                        string ad = dt.Rows[0]["admin"].ToString();
                        if (ad == "1")
                            admin_.Content = "YES";
                        else
                            admin_.Content = "NO";

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

        private void LogOutBottom_Click(object sender, RoutedEventArgs e)
        {
            WindowUser windowUser = new WindowUser();
            this.Close();
            windowUser.Show();
        }
    }
}
