using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Belshifa
{
    public partial class Register : Form
    {
        public static int i;
        string ordb = "data source=orcl; user id=Belshifa; password=123;";
        OracleConnection conn;
        public Register()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_title_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {

            conn = new OracleConnection(ordb);
            conn.Open();

        }
	
	//Registration process 
        private void button1_Click(object sender, EventArgs e)
        {
            //check if data fields is empty 
            if (txt_title.Text.Equals("") || textBox1.Text.Equals("") || textBox4.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals(""))
                MessageBox.Show("Pleaase Fill All Fields");
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into users values (:username,:email,:password,:address,:phone)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("username", txt_title.Text);
                cmd.Parameters.Add("email", textBox1.Text);
                cmd.Parameters.Add("password", textBox4.Text);
                cmd.Parameters.Add("address", textBox2.Text);
                cmd.Parameters.Add("phone", textBox3.Text);
                try
                {
                  i= cmd.ExecuteNonQuery();
                }
                catch {
                    MessageBox.Show("Something Wrong");
                }
                if (i != 0 )
                {
                    MessageBox.Show("Done");
                    txt_title.Clear();
                    textBox1.Clear();
                    textBox4.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
	//clear textbox after finishing registartion
                else
                {
                    txt_title.Clear();
                    textBox1.Clear();
                    textBox4.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                }
                 

               
                   
            }
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
