using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace healthcare
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string[] row = new string[2];
            ListViewItem itm;
            tableLayoutPanel4.Hide();
            tableLayoutPanel5.Hide();
            tableLayoutPanel6.Hide();
            label3.Text = Form1.a;
            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            string query = "select emp_name from employee where emp_id =@id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Form1.a));
            cmd.CommandTimeout = 60;
           // try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        label11.Text = reader.GetString(0);
                    }
                    else
                    {
                        label14.Text = "enter proper id";
                    }
                }
                reader.Close();
                connection.Close();
                //displaying the room details for a given nurse id
                query = "select room_no,app_no from room natural join patient_admit where nurse_id =@id and occupied>0 and date_discharged=DATE'0000-00-00' ";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(Form1.a));
                cmd.CommandTimeout = 60;
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        row[0] = Convert.ToString(reader.GetInt16(0));
                        row[1] = Convert.ToString(reader.GetInt32(1));
                        itm = new ListViewItem(row);
                        listView1.Items.Add(itm);

                    }
                }
            }
           // catch (Exception en)
            {
           //     MessageBox.Show("something went wrong fif: +" + en.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 open = new Form1();
            this.Hide();
            open.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string a = textBox1.Text;
            if (string.IsNullOrEmpty(a))
            {
                errorProvider1.SetError(textBox1, "Fill the required details");
            }
            else
            {
                //update values first
                int pat_id = 0, doc_id=0;
                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                MySqlConnection connection = new MySqlConnection(connstr);
                string query = "select doc_id,description,pat_id from appointment where app_no=@id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                cmd.CommandTimeout = 60;
             //   try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            doc_id = (reader.GetInt32(0));
                            label16.Text = Convert.ToString(reader.GetInt32(0));
                            label17.Text = reader.GetString(1);
                            pat_id = reader.GetInt32(2); 
                        } 
                    }
                    reader.Close();
                    connection.Close();
                    connection.Open();
                    query = "select pat_name,pat_age from patient where pat_id =@id";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", pat_id);
                    cmd.CommandTimeout = 60;
                    MySqlDataReader reader2 = cmd.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            label22.Text = reader2.GetString(0);
                            label20.Text = Convert.ToString(reader2.GetInt16(1));
                        }

                    }
                    reader2.Close();
                    connection.Close();
                    connection.Open();
                    query = "select emp_name from employee where emp_id =@id";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", doc_id);
                    cmd.CommandTimeout = 60;
                    MySqlDataReader reader3 = cmd.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        while (reader3.Read())
                        {
                            label19.Text = reader3.GetString(0);
                        }
                       

                    }
                    reader3.Close();
                    connection.Close();
                   
                    tableLayoutPanel4.Show();
                    tableLayoutPanel5.Show();
                    tableLayoutPanel6.Show();
                }
            //    catch (Exception en)
                {
              //      MessageBox.Show("something went wrong: +" + en.Message);
                }

            }
        }
    }
}
