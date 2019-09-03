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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            label14.Text = Form1.a;
            label3.Hide();
            tableLayoutPanel4.Hide();
            tableLayoutPanel5.Hide();
            tableLayoutPanel6.Hide();
            button4.Hide();
            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            string query = "select emp_name from employee where emp_id =@id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Form1.a);
            cmd.CommandTimeout = 60;
            try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        label15.Text = reader.GetString(0);
                    }
                    
                }
                else
                {
                    MessageBox.Show("enter proper id");
                }
                connection.Close();
                reader.Close();
            }
            catch (Exception en)
            {
                MessageBox.Show("something went wrong: +" + en.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 open = new Form1();
            this.Hide();
            open.ShowDialog();
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            string query = "select count(pat_id) from appointment where doc_id =@docid and app_date = date(SYSDATE()) and checked='false';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@docid", Form1.a);
            cmd.CommandTimeout = 60;
            try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        label3.Text = reader.GetString(0);
                    }
                    label3.Show();
                   
                }
                else
                {MessageBox.Show("enter proper id");
                }
                connection.Close();
                reader.Close();
            }
            catch (Exception en)
            {
                MessageBox.Show("something went wrong: +" + en.Message);
            }

        }
        private void Button3_Click(object sender, EventArgs e)
        {
            string a = textBox1.Text;
            if (string.IsNullOrEmpty(a))
            {
                errorProvider1.SetError(textBox1, "Fill the details");
            }
            else
            {
                 a = textBox1.Text;
                if (string.IsNullOrEmpty(a))
                {
                    errorProvider1.SetError(textBox1, "Fill the details");
                }
                else
                {
                    string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                    MySqlConnection connection = new MySqlConnection(connstr);
                    string query = "select pat_name,pat_sex,pat_age from patient natural join appointment  where app_no =@appno;";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox1.Text));
                    cmd.CommandTimeout = 60;
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                label7.Text = reader.GetString(0);
                                label10.Text = reader.GetString(1);
                                label8.Text = Convert.ToString(reader.GetInt16(2));
                            }
                            connection.Close();
                            reader.Close();
                            query = "select description from appointment where app_no = @appno;";
                            MySqlCommand cmd6 = new MySqlCommand(query, connection);
                            cmd6.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox1.Text));
                            connection.Open();
                            reader = cmd6.ExecuteReader();
                            while (reader.Read())
                            {
                                richTextBox1.Text = reader.GetString(0);
                            };
                            connection.Close();
                            reader.Close();
                            tableLayoutPanel4.Show();
                            tableLayoutPanel5.Show();
                            tableLayoutPanel6.Show();
                            button4.Show();
                        }
                        else
                        {
                            MessageBox.Show("enter proper id");
                            
                        }
                        
                    }
                    catch (Exception en)
                    {
                        MessageBox.Show("something went wrong: +" + en.Message);
                    }
                }
                
               
            }

        }

       

        private void Button4_Click(object sender, EventArgs e)
        {

            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            string query = "update appointment set description=@upval where app_no = @appno;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox1.Text));
            cmd.Parameters.AddWithValue("@upval", richTextBox1.Text);
            cmd.CommandTimeout = 60;
            connection.Open();
            cmd.ExecuteNonQuery();
            query = "update appointment set checked ='true' where app_no = @appno;";
            cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox1.Text));
            cmd.CommandTimeout = 60;
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show(" updated patient description Succesfully");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            string query = "update appointment set pat_type ='in' where app_no =@appno;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox1.Text));
            cmd.CommandTimeout = 60;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Patient status has been changed as IN");
        }
    }
}
