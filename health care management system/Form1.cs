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
//password for patient: pat_12345

//password for doctor: doc_67890

//password for nurse: nur_13579

//password for receptionist: rec_24680
//password for admin : admin_dbms


namespace healthcare
{
    public partial class Form1 : Form
    {
        public static string a;
        public Form1()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
            label2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            textBox1.Hide();
            textBox2.Hide();
            button1.Hide();
            button2.Hide();
            label8.Hide();
            textBox3.Hide();
            button3.Hide();

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string type = "";
            try
            {
                string connstr = "datasource=127.0.0.1;port=3306;username=root;password=;database=hospital";
                MySqlConnection connection = new MySqlConnection(connstr);
                string query = "select emp_type from employee where emp_id =@id";
                 type = "";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt64(textBox1.Text));
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    type = reader.GetString(0);
                };
                connection.Close();
                reader.Close();
                a = textBox1.Text;
            }catch(Exception en)
            {
                MessageBox.Show("ENTER THE DETAILS CORRECTLY");
            }
            
            //string b = "";
            string c = textBox2.Text;
            if (string.IsNullOrEmpty(a))
            {
                errorProvider1.SetError(textBox1, "Fill the required details");   
            }
            
            if (string.IsNullOrEmpty(c))
            {
                errorProvider1.SetError(textBox2, "Fill the required details");
            }
            if (c == "pat_12345") 
            {
                label3.Text = "Appointment ID :";
                if (!string.IsNullOrEmpty(a))
                {
                    Form2 patient = new Form2();
                    this.Hide();
                    patient.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please Fill all the details");
                }
            }
            else if( c== "doc_67890" && type == "Doctor")
            {
                label3.Text = "Doctor ID : ";
                if (!string.IsNullOrEmpty(a))
                {
                    Form4 doctor = new Form4();
                    this.Hide();
                    doctor.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please Fill all the details");
                }
            }
            else if ( c== "nur_13579" && type == "Nurse")
            {
                label3.Text = "Nurse ID : ";
                if (!string.IsNullOrEmpty(a))
                {
                    Form5 nurse = new Form5();
                    this.Hide();
                    nurse.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please Fill all the details");
                }
            }
            else if ( c== "rec_24680" && type == "Receptionist")
            {
                label3.Text = "Receptionist ID : ";
                if (!string.IsNullOrEmpty(a))
                {
                    Form6 receptionist = new Form6();
                    this.Hide();
                    receptionist.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please Fill all the details");
                }
            }
            else
            {
                MessageBox.Show("ACCESS DENIED: ENTER THE DETAILS CORRECTLY");
            }
        }
       
        private void Button2_Click(object sender, EventArgs e)
        {
            Form3 book = new Form3();
            this.Hide();
            book.ShowDialog();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(RadioButton1.Checked==true)
            {
                label3.Show();
                textBox1.Show();
                label4.Show();
                textBox2.Show();
                button1.Show();
            }
            else
            {
                label3.Hide();
              //  textBox1.Hide();
               // label4.Hide();
              //  textBox2.Hide();
              //  button1.Hide();

            }

        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton2.Checked == true)
            {
                label2.Show();
                textBox1.Show();
                label4.Show();
                textBox2.Show();
                button1.Show();
            }
            else
            {
                label2.Hide();
               // textBox1.Hide();
                //label4.Hide();
                //textBox2.Hide();
                //button1.Hide();

            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton3.Checked == true)
            {
                label5.Show();
                textBox1.Show();
                label4.Show();
                textBox2.Show();
                button1.Show();
            }
            else
            {
                label5.Hide();
               // textBox1.Hide();
                //label4.Hide();
                //textBox2.Hide();
                //button1.Hide();

            }
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton4.Checked == true)
            {
                label6.Show();
                textBox1.Show();
                label4.Show();
                textBox2.Show();
                button1.Show();
                
            }
            else
            {
                label6.Hide();
               
                // textBox1.Hide();
                // label4.Hide();
                //textBox2.Hide();
                //button1.Hide();       
            }

        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton5.Checked == true)
            {
                button2.Show();
                label4.Hide();
                textBox2.Hide();
                textBox1.Hide();
                button1.Hide();
            }
            else
            {
                button2.Hide();
                
            }
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton6.Checked==true)
            {
                label8.Show();
                textBox3.Show();
                button3.Show();
                tableLayoutPanel2.Hide();
            }
            else
            {
                label8.Hide();
                textBox3.Hide();
                button3.Hide();
                tableLayoutPanel2.Show();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string p = textBox3.Text;
            if (p == "admin_dbms")
            {
                Form7 admin = new Form7();
                this.Hide();
                admin.ShowDialog();
            }
            else
            {
                MessageBox.Show("ACCESS DENIED");
            }
        }
    }
}
