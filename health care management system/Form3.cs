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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            tableLayoutPanel5.Hide();
            tableLayoutPanel8.Hide();
            tableLayoutPanel2.Hide();
            tableLayoutPanel6.Hide();
            listView1.Hide();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 open = new Form1();
            //this.Hide();
            open.ShowDialog();
            this.Close();

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string a = comboBox1.Text;
            if(string.IsNullOrEmpty(a))
            {
                errorProvider1.SetError(comboBox1, "Fill the details");
            }
            else
            {
                //give the values
                try
                {
                    listView1.Items.Clear();
                    string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                    string[] row = new string[5];
                    MySqlConnection connection = new MySqlConnection(connstr);
                   
                    string query = "select emp_name,emp_sex,qualification,experience,emp_id from employee where emp_id in(select doc_id from emp_doctor where specialisation = @special)";
                    
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@special", comboBox1.Text);
                  
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            row[0] = reader.GetString(0);
                            row[1] = reader.GetString(1);
                            row[2] = reader.GetString(2);
                            row[3] = Convert.ToString(reader.GetInt16(3));
                            row[4] = Convert.ToString(reader.GetInt16(4));

                            ListViewItem itm;

                            itm = new ListViewItem(row);
                            listView1.Items.Add(itm);
                            listView1.Show();

                        }
                        reader.Close();
                    }

                }
                catch (Exception er)
                {
                    MessageBox.Show("some thing went wrong in getting doctors " + er.Message);
                }
               
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                tableLayoutPanel5.Show();
                tableLayoutPanel8.Show();
            }
            else
            {
                tableLayoutPanel5.Hide();
                tableLayoutPanel8.Hide();

            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                tableLayoutPanel2.Show();
                tableLayoutPanel6.Show();
            }
            else
            {
                tableLayoutPanel2.Hide();
                tableLayoutPanel6.Hide();
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
           
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string a1 = textBox1.Text;
            string a2 = textBox2.Text;
            string a3 = textBox3.Text;
            int app_no;
            if(string.IsNullOrEmpty(a1))
            {
                errorProvider1.SetError(textBox1, "Fill the details");
            }
            if (string.IsNullOrEmpty(a2))
            {
                errorProvider1.SetError(textBox2, "Fill the details");
            }
            if (string.IsNullOrEmpty(a3))
            {
                errorProvider1.SetError(textBox3, "Fill the details");
            }
            else
            {

                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                MySqlConnection connection = new MySqlConnection(connstr);

                int max = 0, pre = 0;
                string query = "select max_patients from emp_doctor where doc_id=@id";
                MySqlCommand cmd2 = new MySqlCommand(query, connection);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt64(textBox1.Text));
                connection.Open();
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        max = reader.GetInt32(0);
                    }
                }
                reader.Close();
                connection.Close();

                query = "select count(pat_id) from appointment where doc_id =@id  and app_date = @date;";
                cmd2 = new MySqlCommand(query, connection);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt64(textBox1.Text));
                cmd2.Parameters.AddWithValue("@date", Convert.ToDateTime(textBox3.Text));
                connection.Open();
                reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        pre = reader.GetInt32(0);
                    }
                }
                reader.Close();
                connection.Close();
                if (max - pre > 0)
                {

                    try
                    {
                         connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                         connection = new MySqlConnection(connstr);
                        MySqlCommand cmd = new MySqlCommand
                        {
                            CommandType = System.Data.CommandType.Text,
                            CommandText = "insert into appointment  values (NULL,@patid,@docid,@appdate,'NOT YET CONSULTED','out',0,'false');"
                        };
                        cmd.Parameters.AddWithValue("@patid", Convert.ToInt32(textBox2.Text));
                        cmd.Parameters.AddWithValue("@docid", Convert.ToInt32(textBox1.Text));
                        cmd.Parameters.AddWithValue("@appdate", Convert.ToDateTime(textBox3.Text));
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine("something went wrong" + er.Message);
                    }
                    try
                    {
                         connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                         connection = new MySqlConnection(connstr);
                         query = "select app_no from appointment where pat_id=@patid and doc_id =@docid and app_date=@date;";
                         cmd2 = new MySqlCommand(query, connection);
                        cmd2.Parameters.AddWithValue("@patid", Convert.ToInt32(textBox2.Text));
                        cmd2.Parameters.AddWithValue("@docid", Convert.ToInt32(textBox1.Text));
                        cmd2.Parameters.AddWithValue("@date", Convert.ToDateTime(textBox3.Text));
                        connection.Open();
                         reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                app_no = reader.GetInt32(0);
                                MessageBox.Show("YOUR APPOINTMENT HAS BEEN BOOKED AND YOUR APPOINTMENT NUMBER IS: " + app_no);
                            }
                            else
                            {
                                MessageBox.Show("executed successfully else");
                            }

                        }
                        connection.Close();
                        reader.Close();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("something went wrong" + er.Message);
                    }


                }
                else
                {
                    MessageBox.Show("APPOINTMENTS EXCEEDED FOR THIS DAY FOR THE SELECTED DOCTOR");
                }
               

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int pat_id=0,app_no;
            string a4 = textBox4.Text;
            string a5 = textBox5.Text;
            string a6 = textBox6.Text;
            string a7 = textBox7.Text;
            string a8 = textBox8.Text;
            string a9 = textBox9.Text;
            if (string.IsNullOrEmpty(a4))
            {
                errorProvider1.SetError(textBox4, "Fill the details");
            }
            if (string.IsNullOrEmpty(a5))
            {
                errorProvider1.SetError(textBox5, "Fill the details");
            }
            if (string.IsNullOrEmpty(a6))
            {
                errorProvider1.SetError(textBox6, "Fill the details");
            }
            if (string.IsNullOrEmpty(a7))
            {
                errorProvider1.SetError(textBox7, "Fill the details");
            }
            if (string.IsNullOrEmpty(a8))
            {
                errorProvider1.SetError(textBox8, "Fill the details");
            }
            if (string.IsNullOrEmpty(a9))
            {
                errorProvider1.SetError(textBox9, "Fill the details");
            }
            else
            {
                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                MySqlConnection connection = new MySqlConnection(connstr);

                int max = 0, pre = 0;
                string query = "select max_patients from emp_doctor where doc_id=@id";
                MySqlCommand cmd2 = new MySqlCommand(query, connection);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt64(textBox8.Text));
                connection.Open();
                MySqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        max = reader.GetInt32(0);
                    }
                }
                reader.Close();
                connection.Close();

                query = "select count(pat_id) from appointment where doc_id =@id  and app_date = @date;";
                cmd2 = new MySqlCommand(query, connection);
                cmd2.Parameters.AddWithValue("@id", Convert.ToInt32(textBox8.Text));
                cmd2.Parameters.AddWithValue("@date", Convert.ToDateTime(textBox9.Text));
                connection.Open();
                reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        pre = reader.GetInt32(0);
                    }
                }
                reader.Close();
                connection.Close();

                if (max - pre > 0)
                {


                    try
                    {
                         connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                         connection = new MySqlConnection(connstr);
                        //query
                        //Checking before insertion

                        MySqlCommand cmd = new MySqlCommand
                        {
                            CommandType = System.Data.CommandType.Text,
                            CommandText = "insert into patient (pat_name,pat_sex,pat_age,pat_contact) values (@name,@sex,@age,@contact);"
                        };
                        cmd.Parameters.AddWithValue("@name", textBox4.Text);
                        cmd.Parameters.AddWithValue("@sex", textBox5.Text);
                        cmd.Parameters.AddWithValue("@age", Convert.ToInt16(textBox6.Text));
                        cmd.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox7.Text));
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception er)
                    {
                        Console.WriteLine("something went wrong" + er.Message);
                    }

                    try
                    {
                         connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                         connection = new MySqlConnection(connstr);
                         query = "select pat_id from patient where pat_contact =@contact and pat_name =@name;";
                         cmd2 = new MySqlCommand(query, connection);
                        cmd2.Parameters.AddWithValue("@contact", Convert.ToInt64(textBox7.Text));
                        cmd2.Parameters.AddWithValue("@name", textBox4.Text);
                        connection.Open();
                         reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                pat_id = reader.GetInt32(0);
                            }
                            else
                            {
                                MessageBox.Show("executed successfully else");
                            }

                        }
                        reader.Close();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("something went wrong" + er.Message);
                    }
                    try
                    {
                         connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                         connection = new MySqlConnection(connstr);

                        MySqlCommand cmd3 = new MySqlCommand
                        {
                            CommandType = System.Data.CommandType.Text,
                            CommandText = "insert into appointment values (NULL,@patid,@docid,@appdate,'NOT YET CONSULTED','out',0,'false');"
                        };
                        cmd3.Parameters.AddWithValue("@docid", Convert.ToInt32(textBox8.Text));
                        cmd3.Parameters.AddWithValue("@appdate", Convert.ToDateTime(textBox9.Text));
                        cmd3.Parameters.AddWithValue("@patid", pat_id);
                        cmd3.Connection = connection;
                        connection.Open();
                        cmd3.ExecuteNonQuery();

                        connection.Close();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("something went wrong" + er.Message);
                    }
                    try
                    {
                         connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                         connection = new MySqlConnection(connstr);
                         query = "select app_no from appointment where pat_id=@patid and doc_id =@docid and app_date=@date;";
                         cmd2 = new MySqlCommand(query, connection);
                        cmd2.Parameters.AddWithValue("@patid", pat_id);
                        cmd2.Parameters.AddWithValue("@docid", Convert.ToInt32(textBox8.Text));
                        cmd2.Parameters.AddWithValue("@date", Convert.ToDateTime(textBox9.Text));
                        connection.Open();
                         reader = cmd2.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                app_no = reader.GetInt32(0);
                                MessageBox.Show("YOUR APPOINTMENT HAS BEEN BOOKED AND YOUR APPOINTMENT NUMBER IS: " + app_no + " AND YOUR ID IS: " + pat_id);
                            }
                            else
                            {
                                MessageBox.Show("executed successfully else");
                            }

                        }
                        connection.Close();
                        reader.Close();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("something went wrong" + er.Message);
                    }
                }
                else
                {
                    MessageBox.Show("APPOINTMENTS EXCEEDED FOR THIS DAY FOR THE SELECTED DOCTOR");
                }
            }
        }
    }
}
