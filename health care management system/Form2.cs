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
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Text = Form1.a;
            tableLayoutPanel3.Hide();
            tableLayoutPanel4.Hide();
            tableLayoutPanel5.Hide();
            tableLayoutPanel6.Hide();
            string pattype = "";
            //string docname
            int roomno=1,nurid=2,docid=0;
            string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
            MySqlConnection connection = new MySqlConnection(connstr);
            int appno =Convert.ToInt32(label3.Text);


            string query = "select pat_name,doc_id  from appointment natural join patient where app_no = @appno;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@appno", appno);
            cmd.CommandTimeout = 60;
           // try
            {
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        label11.Text = reader.GetString(0);
                        docid = reader.GetInt32(1);
                    }
                }
                connection.Close();
                label29.Text = Convert.ToString(docid);
                reader.Close();
                query = "select emp_name from employee where emp_id=@docid";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@docid", docid);
                cmd.CommandTimeout = 60;
                connection.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        label30.Text = reader.GetString(0);
                        
                    }
                }
                connection.Close();
                reader.Close();


            }
           // catch (Exception en)
            {
             //   MessageBox.Show("something went wrong: +" + en.Message);
            }
            query = "select pat_type from appointment where app_no = @appno;";
            MySqlCommand cmd2 = new MySqlCommand(query, connection);
            cmd2.Parameters.AddWithValue("@appno",appno);
           // cmd2.CommandTimeout = 60;
           // try
            {
                connection.Open();

                MySqlDataReader reader = cmd2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         pattype = reader.GetString(0);
                    }
                    reader.Close();
                    connection.Close();
                        
                    if (pattype == "in")
                    {
                        query = "select room_no,date_admitted from patient_admit where app_no =@appno;";
                        MySqlCommand cmd3 = new MySqlCommand(query, connection);
                        cmd3.Parameters.AddWithValue("@appno", appno);
                        connection.Open();
                        reader = cmd3.ExecuteReader();
                        while (reader.Read())
                        {
                            roomno = reader.GetInt16(0);
                            DateTime date = reader.GetDateTime(1).Date;
                             label17.Text = Convert.ToString(date);
                             
                        }
                        label15.Text = roomno.ToString();
                        reader.Close();
                        connection.Close();

                        query = "select nurse_id from room where room_no = @roomno;";
                        MySqlCommand cmd4 = new MySqlCommand(query, connection);
                        cmd4.Parameters.AddWithValue("@roomno", roomno);
                        connection.Open();
                        reader = cmd4.ExecuteReader();
                        while (reader.Read())
                        {
                            nurid = reader.GetInt16(0);
                        }
                        
                        reader.Close();
                        connection.Close();

                        query = "select emp_name,emp_contact,experience from employee where emp_id = @nurid;";
                        MySqlCommand cmd5 = new MySqlCommand(query, connection);
                        cmd5.Parameters.AddWithValue("@nurid", nurid);
                        connection.Open();
                        reader = cmd5.ExecuteReader();
                        while (reader.Read())
                        {
                            label5.Text = reader.GetString(0);
                            label9.Text = reader.GetString(1);
                            label7.Text = reader.GetString(2);

                        }
                       
                        reader.Close();
                        connection.Close();
                        tableLayoutPanel3.Show();
                        tableLayoutPanel4.Show();
                        tableLayoutPanel5.Show();
                        tableLayoutPanel6.Show();

                    }
                    else
                    {
                        //do something for in patients i.e. na
                    }
                    query = "select description from appointment where app_no = @appno;";
                    MySqlCommand cmd6 = new MySqlCommand(query, connection);
                    cmd6.Parameters.AddWithValue("@appno", appno);
                    connection.Open();
                    reader = cmd6.ExecuteReader();
                    while (reader.Read())
                    {
                        label18.Text = reader.GetString(0);
                    }
                   
                    reader.Close();
                    connection.Close();

                    MySqlCommand billcmd = new MySqlCommand("bill", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    billcmd.Parameters.AddWithValue("@appno", appno);
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
                    connection.Close();
                }

            }
          //  catch (Exception en)
            {
          //      MessageBox.Show("something went wrong: +" + en.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 open = new Form1();
            this.Hide();
            open.ShowDialog();
            Close();
        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label29_Click(object sender, EventArgs e)
        {

        }
    }
}