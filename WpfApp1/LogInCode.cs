using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    public class LogInCode
    {

        static SqlConnection con;
        static SqlCommand cmd;
        public static string id_login;

        
        public static Boolean LogI(String Id, String Password_)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.text_password.Text = Password_;
            mainWindow.text_id.Text = Id;
            return LogInCode.Log(mainWindow);
        }

        public static Boolean Log(MainWindow x)
        {
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
                cmd.Parameters.AddWithValue("@Password", x.text_password.Text);
                cmd.Parameters.AddWithValue("@ID", x.text_id.Text);

                // Step 5: Prepare the data for the DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();

                // Step 6: Fill the DataSet
                da.Fill(dataSet);

                // Step 7: Check if there are rows in the DataSet
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    id_login = x.text_id.Text;

                    //Try 1
                    try
                    {

                        // Step 2: Create the select Query with parameters
                        Query = "SELECT * FROM person WHERE PASSWORD_=@Password AND ID=@ID AND ADMIN=1";

                        // Step 3: Create the command to execute
                        cmd = new SqlCommand(Query, con);

                        cmd.Parameters.AddWithValue("@Password", x.text_password.Text);
                        cmd.Parameters.AddWithValue("@ID", x.text_id.Text);

                        // Step 5: Prepare the data for the DataSet
                        da = new SqlDataAdapter(cmd);
                        dataSet = new DataSet();

                        // Step 6: Fill the DataSet
                        da.Fill(dataSet);

                        // Step 7: Check if there are rows in the DataSet
                        if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                        {
                            id_login = x.text_id.Text;
                            WindowAdmin windowAdmin = new WindowAdmin();
                            x.Close();
                            windowAdmin.Show();
                            con.Close();
                            return true;
                        }
                        else
                        {
                            id_login = x.text_id.Text;
                            WindowUser windowUser = new WindowUser();
                            x.Close();
                            windowUser.Show();
                            con.Close();
                            return true;
                        }

                        // Step 8: Close the connection
                        
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        con.Close();
                        return false;
                    }


                    //try 1
                }
                else
                {
                    MessageBox.Show("The ID and/or password are wrong");
                    con.Close();
                    return false;
                }

                // Step 8: Close the connection
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                return false;
            }

        }

    }
}
