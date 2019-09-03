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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            tableLayoutPanel4.Hide();

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                tableLayoutPanel3.Show();
                tableLayoutPanel4.Hide();
            }

        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                tableLayoutPanel4.Show();
                tableLayoutPanel3.Hide();
            }
            else
            {
                tableLayoutPanel4.Hide();
            }

        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                tableLayoutPanel3.Hide();
                tableLayoutPanel4.Hide();
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            if (radioButton1.Checked)
                {
                // doctor :insert into employee,doctor
                MySqlCommand cmd = new MySqlCommand
                {
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "insert into employee (emp_id,emp_name,emp_age,emp_sex,emp_contact,salary,qualification,experience,emp_type) values (NULL,@name,@age,@sex,@contact,@sal,@qual,@exp,@type);"

                };
               // cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textBox1.Text));
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt16(textBox3.Text));
                cmd.Parameters.AddWithValue("@sex", textBox4.Text);
                cmd.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox5.Text));
                cmd.Parameters.AddWithValue("@sal", Convert.ToInt32(textBox6.Text));
                cmd.Parameters.AddWithValue("@qual", textBox7.Text);
                cmd.Parameters.AddWithValue("@exp", Convert.ToInt32(textBox8.Text));
                cmd.Parameters.AddWithValue("@type", "Doctor");
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                long emp_id=0;
                string query = "select emp_id from employee where emp_contact=@contact";
                MySqlCommand cmd2 = new MySqlCommand(query,connection);
                cmd2.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox5.Text));
                connection.Open();
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        emp_id = reader.GetInt32(0);
                    }

                }
                reader.Close();
                connection.Close();

                cmd = new MySqlCommand
                {
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "insert into emp_doctor (doc_id,specialisation,consultation,max_patients) values (@id,@special,@consul,@max);"

                };
                cmd.Parameters.AddWithValue("@id",emp_id);
                cmd.Parameters.AddWithValue("@special", textBox9.Text);
                cmd.Parameters.AddWithValue("@consul", textBox10.Text);
                cmd.Parameters.AddWithValue("@max", Convert.ToInt32(textBox11.Text));
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("SUCCESSFULLY EMPLOYED A NEW DOCTOR AND HIS ID IS: "+emp_id);

            }
            if (radioButton2.Checked)
            {
                //nurse :insert into employee,room
                MySqlCommand cmd = new MySqlCommand
                {
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "insert into employee (emp_id,emp_name,emp_age,emp_sex,emp_contact,salary,qualification,experience,emp_type) values (NULL,@name,@age,@sex,@contact,@sal,@qual,@exp,@type);"

                };
                //cmd.Parameters.AddWithValue("@id", Convert.ToInt64(textBox1.Text));
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt16(textBox3.Text));
                cmd.Parameters.AddWithValue("@sex", textBox4.Text);
                cmd.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox5.Text));
                cmd.Parameters.AddWithValue("@sal", Convert.ToInt32(textBox6.Text));
                cmd.Parameters.AddWithValue("@qual", textBox7.Text);
                cmd.Parameters.AddWithValue("@exp", Convert.ToInt16(textBox8.Text));
                cmd.Parameters.AddWithValue("@type", "Nurse");
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                long emp_id = 0;
                string query = "select emp_id from employee where emp_contact=@contact";
                MySqlCommand cmd2 = new MySqlCommand(query, connection);
                cmd2.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox5.Text));
                connection.Open();
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        emp_id = reader.GetInt32(0);
                    }

                }
                reader.Close();
                connection.Close();
                 query = "update room set nurse_id =@id where room_no=@room";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", emp_id);
                cmd.Parameters.AddWithValue("@room", Convert.ToInt32(textBox12.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                 query = "update room set nurse_id =@id where room_no=@room";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", emp_id);
                cmd.Parameters.AddWithValue("@room", Convert.ToInt32(textBox13.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                query = "update room set nurse_id =@id where room_no=@room";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", emp_id);
                cmd.Parameters.AddWithValue("@room", Convert.ToInt32(textBox14.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                query = "update room set nurse_id =@id where room_no=@room";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", emp_id);
                cmd.Parameters.AddWithValue("@room", Convert.ToInt32(textBox15.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("SUCCESSFULLY EMPLOYED A NEW NURSE AND ID IS: "+emp_id);
            }
            if (radioButton3.Checked)
            {
                //receptionist :insert into employee
                MySqlCommand cmd = new MySqlCommand
                {
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "insert into employee (emp_id,emp_name,emp_age,emp_sex,emp_contact,salary,qualification,experience,emp_type) values (NULL,@name,@age,@sex,@contact,@sal,@qual,@exp,@type);"

                };
              //  cmd.Parameters.AddWithValue("@id", Convert.ToInt64(textBox1.Text));
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@age", Convert.ToInt16(textBox3.Text));
                cmd.Parameters.AddWithValue("@sex", textBox4.Text);
                cmd.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox5.Text));
                cmd.Parameters.AddWithValue("@sal", Convert.ToInt32(textBox6.Text));
                cmd.Parameters.AddWithValue("@qual", textBox7.Text);
                cmd.Parameters.AddWithValue("@exp", Convert.ToInt16(textBox8.Text));
                cmd.Parameters.AddWithValue("@type", "Receptionist");
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                long emp_id = 0;
                string query = "select emp_id from employee where emp_contact=@contact";
                MySqlCommand cmd2 = new MySqlCommand(query, connection);
                cmd2.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox5.Text));
                connection.Open();
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        emp_id = reader.GetInt32(0);
                    }

                }
                reader.Close();
                connection.Close();

                MessageBox.Show("SUCCESSFULLY EMPLOYED A NEW RECEPTIONIST ID IS: "+emp_id);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1 open = new Form1();
            this.Hide();
            open.ShowDialog();
        }
    }
}
