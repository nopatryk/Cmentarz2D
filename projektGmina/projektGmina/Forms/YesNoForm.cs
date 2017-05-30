using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektGmina
{
    public partial class YesNoForm : Form
    {
        detailsForm df;
        string id;
        public YesNoForm(string id,detailsForm df)
        {
            this.df = df;
            this.id = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            df.deleteRow(id,false);
            df.Close();
            Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void YesNoForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            df.deleteRow(id, true);
            df.Close();
            Close();
        }
    }
}
