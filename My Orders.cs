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
    public partial class My_Orders : Form
    {
        string ordb = "data source=orcl; user id=Belshifa; password=123;";
        OracleConnection conn;
        public My_Orders()
        {
            InitializeComponent();
        }

        private void dgv_Films_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
	
	//Update status of order from pending to done when user confirm order
        private void button3_Click(object sender, EventArgs e)
        {
            int selected = dgv_Films.SelectedRows.Count;

               String query2 = @"update Orders O
                            set O.Status = 'Done'
                            where O.PatientEmail= :PatientEmail AND  O.Status = 'Pending' ";
                dgv_Films.DataSource = null;
                OracleDataAdapter adapter = new OracleDataAdapter(query2, ordb);
                adapter.SelectCommand.Parameters.Add("PatientEmail", Login.patientemail);
                OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
                DataSet ds = new DataSet();
                adapter.Fill(ds);


            String query1 = @"Select O.OrderId, O.Item, O.PatientEmail ,Status
                            From Orders O
                            where O.PatientEmail= :PatientEmail ";
            OracleDataAdapter adapter1 = new OracleDataAdapter(query1, ordb);
            adapter1.SelectCommand.Parameters.Add("PatientEmail", Login.patientemail);
            OracleCommandBuilder builder1 = new OracleCommandBuilder(adapter1);
            DataSet ds1 = new DataSet();
            adapter1.Fill(ds1);
            dgv_Films.DataSource = null;
            dgv_Films.Columns.Clear();
            dgv_Films.DataSource = ds1.Tables[0];
            adapter1.Update(ds1.Tables[0]);

            for (int j = 0; j < selected; j++)
            {
                String query3 = @"insert into ORDERS_MEDICINE values(:order_id , :medicine_name) ";
                OracleDataAdapter adapter3 = new OracleDataAdapter(query3, ordb);
                adapter3.SelectCommand.Parameters.Add("order_id", dgv_Films.Rows[j].Cells[0].Value.ToString());
                adapter3.SelectCommand.Parameters.Add("medicine_name", dgv_Films.Rows[j].Cells[1].Value.ToString());
                OracleCommandBuilder builder3 = new OracleCommandBuilder(adapter3);
                DataSet ds3 = new DataSet();
                try { adapter3.Fill(ds3); }
                catch { MessageBox.Show("This Order Already Placed"); }
                
            }
            MessageBox.Show("Done");
        }


	//show user orders 
        private void My_Orders_Load(object sender, EventArgs e)
        {

            String query1 = @"Select O.OrderId, O.Item, O.PatientEmail ,Status
                            From Orders O
                            where O.PatientEmail= :PatientEmail ";
            OracleDataAdapter adapter = new OracleDataAdapter(query1, ordb);
            adapter.SelectCommand.Parameters.Add("PatientEmail", Login.patientemail);
            OracleCommandBuilder builder = new OracleCommandBuilder(adapter);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dgv_Films.DataSource = null;
            dgv_Films.Columns.Clear();
            dgv_Films.DataSource = ds.Tables[0];
            //OracleCommand cmd = new OracleCommand();
            //OracleConnection conn = new OracleConnection();
            //conn.Open();
            //cmd.Connection = conn;
            //cmd.CommandText = "Select OrderId, Item, PatientEmail ,Status From Orders  where PatientEmail= :PatientEmail";
            //cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("PatientEmail", Login.patientemail);
            //OracleDataReader dr = cmd.ExecuteReader();
            //dgv_Films.DataSource = null;
            //dgv_Films.Columns.Clear();
            //dgv_Films.ColumnCount = 4;
            //dgv_Films.Columns[0].Name = "OrderId";
            //dgv_Films.Columns[1].Name = "Item";
            //dgv_Films.Columns[2].Name = "PatientEmail";
            //dgv_Films.Columns[3].Name = "Status";
            //while (dr.Read())
            //{
            //    dgv_Films.Rows.Add(dr.NextResult());
            //}
            //dr.Close();
        }
    }
}
