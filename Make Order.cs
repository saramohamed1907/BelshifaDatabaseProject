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
    public partial class Make_Order : Form
    {
        string ordb = "data source=orcl; user id=Belshifa; password=123;";
        public static int i;
        public static int selectedrow;
        public Make_Order()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	//Select all medicines from database

        private void textBox1_TextChanged(object sender, EventArgs e)
        {           
            String query = @"Select M.MedicineName,M.Price 
                            From Medicines M
                            where M.MedicineName= :MedicineName ";
            dgv_Films.DataSource = null;
            OracleDataAdapter adapter = new OracleDataAdapter(query, ordb);
            adapter.SelectCommand.Parameters.Add("MedicineName", textBox1.Text);
            OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgv_Films.DataSource = null;
            dgv_Films.Columns.Clear();
            dgv_Films.DataSource = ds.Tables[0];
        }

        private void dgv_Films_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           selectedrow= e.RowIndex;
        }
	
	//insert in orders table when user choose medicine
        private void button1_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection(ordb);
            conn.Open();
            if (dgv_Films.SelectedRows.Count == 0)
                MessageBox.Show("Pleaase Select All Row");
            else
            {
                for (int j = 0; j <dgv_Films.SelectedRows.Count;j++)
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into cart values (CartId.nextval,:item,:patientemail)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("item", dgv_Films.Rows[j].Cells[0].Value.ToString());
                    cmd.Parameters.Add("patientemail", Login.patientemail);
                    try
                    {
                        i = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Something Wrong");
                    }
                    OracleCommand cmd2 = new OracleCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "insert into Orders values (OrderId.nextval,:item,:patientemail,DEFAULT, :Order_date)";
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.Add("item", dgv_Films.Rows[j].Cells[0].Value.ToString());
                    cmd2.Parameters.Add("patientemail", Login.patientemail);
                    cmd2.Parameters.Add("Order_date", DateTime.Now.ToString("dd-MMM-yyyy"));
                    try
                    {
                        i = cmd2.ExecuteNonQuery();
                    }   
                    catch
                    {
                        MessageBox.Show("Something Wrong");
                    }
                    if (i != 0)
                        MessageBox.Show("Item "+ (j+1) +" is Added");
                }
            }
        }

        private void Make_Order_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            My_Orders m = new My_Orders();
            m.Show();
        }

	//Show alternative medicines if the medicine that user ask is not avillable
        private void button2_Click(object sender, EventArgs e)
        {
            string CategoryName;
            OracleConnection conn = new OracleConnection(ordb);
            conn.Open();
            if (textBox1.Text.Length==0)
                MessageBox.Show("Pleaase Write Mediciine Name");
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetCategory";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("MedicineName", textBox1.Text);
                cmd.Parameters.Add("CategoryName", OracleDbType.Varchar2, 30000).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                CategoryName = Convert.ToString(cmd.Parameters["CategoryName"].Value.ToString());
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "GetAlternatives";
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("CategoryName", CategoryName);
                cmd2.Parameters.Add("Alt ", OracleDbType.RefCursor, ParameterDirection.Output);
                OracleDataReader dr = cmd2.ExecuteReader();
                dgv_Films.DataSource = null;
                dgv_Films.Columns.Clear();
                dgv_Films.ColumnCount = 1;  
                dgv_Films.Columns[0].HeaderText = "Alternatives";
                while (dr.Read())
                {
                    dgv_Films.Rows.Add(dr[0].ToString());
                }
                dr.Close(); 


            }
        }


    }
    }

