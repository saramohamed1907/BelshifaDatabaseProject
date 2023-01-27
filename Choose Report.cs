using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Belshifa
{
    public partial class Choose_Report : Form 
    {
        public Choose_Report() 
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            Search_Category_Form s = new Search_Category_Form(); 
            s.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ROD_Form r = new ROD_Form();
            r.Show();
        }
    }
}
