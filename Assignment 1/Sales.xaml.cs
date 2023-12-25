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

namespace WpfAppMSSQL
{
    /// <summary>
    /// Lógica de interacción para Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        public Sales()
        {
            InitializeComponent();
        }

        static SqlConnection con; // It is the connection adapter
        static SqlCommand cmd; // it the query proceeding adapter
        static SqlCommand cmdU;
        static SqlCommand cmdI;
        static SqlCommand cmdS;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(@"Server=LAPTOP-7ESOV3LA;Database=master;Trusted_Connection=True");
            con.Open();
            string Query = "Select * from Orders where p_name=@p_name";
            cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@p_name", p_name.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows != null)
            {
                string amount = dt.Rows[0]["p_amount"].ToString();
                if (float.Parse(amount) < float.Parse(p_amount.Text))
                {
                    MessageBox.Show("Only "+amount+"kg available");
                }
                else
                {

                    try
                    {                       
                        string QueryU = "Update Orders set p_amount=@p_amount where p_name=@p_name";
                        cmdU = new SqlCommand(QueryU, con);
                        cmdU.Parameters.AddWithValue("@p_amount", float.Parse(amount)-float.Parse(p_amount.Text));
                        cmdU.Parameters.AddWithValue("@p_name", p_name.Text);
                        int i = cmdU.ExecuteNonQuery();
                        if (i == 1)
                        {
                            MessageBox.Show("Inventory Updated!");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    try
                    {
                        string QueryI = "insert into Sales values(@c_id, @p_name, @p_amount)";

                        cmdI = new SqlCommand(QueryI, con);

                        cmdI.Parameters.AddWithValue("@p_name", p_name.Text);
                        cmdI.Parameters.AddWithValue("@c_id", c_id.Text);
                        cmdI.Parameters.AddWithValue("@p_amount", float.Parse(p_amount.Text));

                        cmdI.ExecuteNonQuery();
                        MessageBox.Show("Sale Made!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    try
                    {
                        string QueryS = "select * from Sales where c_id=@c_id";
                        cmdS = new SqlCommand(QueryS, con);
                        cmdS.Parameters.AddWithValue("@c_id", c_id.Text);
                        SqlDataAdapter daS = new SqlDataAdapter(cmdS);
                        DataTable dtS = new DataTable();
                        daS.Fill(dtS);

                        dbGridS.ItemsSource = dtS.AsDataView();
                        DataContext = daS;

                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
            else
            {
                MessageBox.Show("Product no available, insert other product");
            }



            con.Close();



        }

        private void sales_Click(object sender, RoutedEventArgs e)
        {
            MainWindow M = new MainWindow();
            this.Close();
            M.Show();
        }
    }
}
