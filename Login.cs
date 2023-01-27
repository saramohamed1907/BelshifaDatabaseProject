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
    public partial class Login : Form
    {
	//Database user and Password 
        public static string patientemail;
        string ordb = "data source=orcl; user id=Belshifa; password=123;";
        OracleConnection conn;
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
	
	//When user click on login button 
        private void button1_Click(object sender, EventArgs e)
        {
            string mail = "", pass = "";
	//If mail or bassword is empty show message 
            if (textBox1.Text.Equals("") || textBox4.Text.Equals(""))
                MessageBox.Show("Pleaase Fill All Fields");
	//if mail and password are (admin , admin)  login as admin
           else if (textBox1.Text.Equals("admin") || textBox4.Text.Equals("admin"))
            {
                Dispose();
                Choose_Report c = new Choose_Report();
                c.Show();
            }
		//if mail and password are not (admin , admin)  search on database for these data
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("mail", textBox1.Text);
                cmd.Parameters.Add("pass", textBox4.Text);
                cmd.Parameters.Add("Email", OracleDbType.Varchar2, 20000).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Password", OracleDbType.Varchar2, 20000).Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                    mail = Convert.ToString(cmd.Parameters["Email"].Value.ToString());
                    patientemail = Convert.ToString(cmd.Parameters["Email"].Value.ToString());
                    pass = Convert.ToString(cmd.Parameters["Password"].Value.ToString());
                }
		//if not found
                catch
                {
                    MessageBox.Show("Email or Password Wrong");
                }
                if (mail == "" || pass == "")
                {
                    textBox1.Clear();
                    textBox4.Clear();
                }
                else
                {
                    Dispose();
                    Make_Order m = new Make_Order();
                    m.Show();
                }

            }
          
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }
    }
}
