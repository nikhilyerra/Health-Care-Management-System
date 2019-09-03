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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {




            label3.Text = Form1.a;
            label5.Hide();
            tableLayoutPanel3.Hide();
            label10.Hide();
            tableLayoutPanel4.Hide();
            label12.Hide();
            tableLayoutPanel5.Hide();
            button6.Hide();
            listView1.Hide();
            label20.Hide();
            tableLayoutPanel10.Hide();
            button7.Hide();
            richTextBox1.Hide();
            label15.Hide();


            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            string query = "select emp_name from employee where emp_id =@id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", Form1.a);
            cmd.CommandTimeout = 60;
        //    try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        label8.Text = reader.GetString(0);
                    }
                    else
                    {
                        label14.Text = "enter proper id";
                    }
                }
                connection.Close();
            }
          //  catch (Exception en)
            {
            //    MessageBox.Show("something went wrong: +" + en.Message);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 open = new Form1();
            this.Close();
            open.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string a = comboBox1.Text;
            label5.Hide();
            tableLayoutPanel3.Hide();
            label10.Hide();
            tableLayoutPanel4.Hide();
            label12.Hide();
            tableLayoutPanel5.Hide();
            button6.Hide();
            listView1.Hide();
            label20.Hide();
            tableLayoutPanel10.Hide();
            button7.Hide();
            richTextBox1.Hide();
            label15.Hide();
            if (string.IsNullOrEmpty(a))
            {
                errorProvider1.SetError(comboBox1, "Fill the details");
            }
            else
            {
                if(a== "BOOK AN APPOINTMENT")
                {
                    Form3 book = new Form3();
                    this.Hide();
                    book.ShowDialog();
                }
                if(a=="MEDICAL BILL UPDATE")
                {
                    label5.Show();
                    tableLayoutPanel3.Show();
                    button7.Show();

                }
                if(a=="DISCHARGE A PATIENT")
                {
                    label10.Show();
                    tableLayoutPanel4.Show();
                   
                }
                if(a=="BOOK A ROOM")
                {
                    label12.Show();
                    tableLayoutPanel5.Show();
                    button6.Show();
                }
            }
            
        }

       public void Button4_Click(object sender, EventArgs e)
        {
            string a1 = textBox3.Text;
            int roomno=0;
            if (string.IsNullOrEmpty(a1))
            {
                errorProvider1.SetError(textBox3, "Fill the details");
            }
            else
            {
                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                MySqlConnection connection = new MySqlConnection(connstr);
                MySqlCommand billcmd = new MySqlCommand("bill", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                billcmd.Parameters.AddWithValue("@appno", Convert.ToInt64(textBox3.Text));
              //  billcmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox3.Text));
                MySqlParameter param = new MySqlParameter("medical_bill", MySqlDbType.Int16)
                {
                    Direction = ParameterDirection.Output
                };
                billcmd.Parameters.Add(param);
                MySqlParameter param2 = new MySqlParameter("room_rent", MySqlDbType.Int16)
                {
                    Direction = ParameterDirection.Output
                };
                billcmd.Parameters.Add(param2);
                MySqlParameter param3 = new MySqlParameter("doctor_fees", MySqlDbType.Int16)
                {
                    Direction = ParameterDirection.Output
                };
                billcmd.Parameters.Add(param3);
                connection.Open();
                billcmd.ExecuteNonQuery();

                label22.Text = Convert.ToString(param3.Value);
                label23.Text = Convert.ToString(param.Value);
                label24.Text = Convert.ToString(param2.Value);
                int sum = Convert.ToInt16(param.Value) + Convert.ToInt16(param2.Value) + Convert.ToInt16(param3.Value);

                label28.Text = Convert.ToString(sum);
                label20.Show();
                tableLayoutPanel10.Show();
                connection.Close();

                //updating discharge date of  a patient 
                string query = "update patient_admit set date_discharged=date(sysdate()) where app_no=@appno";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@appno", Convert.ToInt64(textBox3.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                //updating a room occupancy
                query = "select room_no from patient_admit where app_no =@appno";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@appno", Convert.ToInt64(textBox3.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        roomno = reader.GetInt16(0);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception en)
                {
                      MessageBox.Show("something went wrong: +" + en.Message);
                }

                query = "update room set occupied = 0 where room_no =@roomno";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@roomno",roomno);
                cmd.CommandTimeout = 60;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show(textBox3.Text + " HAS BEEN DISCHARGED SUCCESFULLY");


            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
           // try
            {
                ListViewItem itm;
                listView1.Items.Clear();
                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                string[] row = new string[1];
                MySqlConnection connection = new MySqlConnection(connstr);
                string query = "select room_no  from room where occupied=0;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // string name = reader.GetString(0);
                        //  string sex = reader.GetString(1);
                        //  string qualification = reader.GetString(2);
                        //  int experience = reader.GetInt16(3);
                        //row = {name; sex,qualification,experience};

                        row[0] = Convert.ToString(reader.GetInt16(0));

                        
                        
                        itm = new ListViewItem(row);
                        listView1.Items.Add(itm);

                    }
                }
                listView1.Show();
            }
          //  catch (Exception er)
            {
          //      MessageBox.Show("some thing went wrong in getting doctors " + er.Message);
            }
           
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string a3 = textBox4.Text;
            string a4 = textBox5.Text;
            if (string.IsNullOrEmpty(a3))
            {
                errorProvider1.SetError(textBox4, "Fill the details");
            }
            if (string.IsNullOrEmpty(a4))
            {
                errorProvider1.SetError(textBox5, "Fill the details");
            }
            else
            {
                string pat_type="";
                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                MySqlConnection connection = new MySqlConnection(connstr);
                string query = "select pat_type from appointment where app_no=@appno";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox4.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       pat_type = reader.GetString(0);
                    }

                }
                connection.Close();
                reader.Close();
                if (pat_type == "in")
                {
                    // insertion of data into patient admit 
                    query = "insert into patient_admit values (@appno,@roomno,SYSDATE(),DATE'0000-00-00');";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@roomno", Convert.ToInt16(textBox5.Text));
                    cmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox4.Text));
                    cmd.CommandTimeout = 60;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    //  updating the no of pat in the room
                    query = "update room set occupied = 1 where room_no =@roomno";
                    cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@roomno", Convert.ToInt16(textBox5.Text));
                    cmd.CommandTimeout = 60;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("ROOM HAS BEEN BOOKED SUCCESFULLY");
                }
                else
                    MessageBox.Show("DOCTOR HAS NOT ASKED YOU TO ADMIT");


               
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string a2 = textBox1.Text;
            if(string.IsNullOrEmpty(a2))
            {
                errorProvider1.SetError(textBox3, "Fill the details");
            }
            else
            {
                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                MySqlConnection connection = new MySqlConnection(connstr);
                string query = "update appointment set med_price = med_price+@incre where app_no =@appno;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@appno", Convert.ToInt32(textBox1.Text));
                cmd.Parameters.AddWithValue("@incre", Convert.ToInt16(textBox2.Text));
                cmd.CommandTimeout = 60;
                connection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medical Bill has been updated successfully");
                connection.Close();
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            int appno = Convert.ToInt32(textBox1.Text);
            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            string query = "select description from appointment where app_no = @appno;";
            MySqlConnection connection = new MySqlConnection(connstr);
            MySqlCommand cmd6 = new MySqlCommand(query, connection);
            cmd6.Parameters.AddWithValue("@appno", appno);
            //cmd6.Parameters.AddWithValue("@appno",Convert.ToInt32(textBox4.Text));
            connection.Open();
            
            MySqlDataReader reader = cmd6.ExecuteReader(); 
            while (reader.Read())
            {
                richTextBox1.Text = reader.GetString(0);
            }
            richTextBox1.Show();
            label15.Show();
            reader.Close();
            connection.Close();
        }

    }
}
