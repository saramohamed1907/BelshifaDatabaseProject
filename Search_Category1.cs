using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;
namespace Belshifa
{
    public partial class Search_Category_Form : Form
    {
        Search_Category report;
        public Search_Category_Form()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            report = new Search_Category();
            foreach (ParameterDiscreteValue category in report.ParameterFields[0].DefaultValues)
                comboBox1.Items.Add(category.Value);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            report.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer1.ReportSource = report;
        }

        private void Search_Category_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
