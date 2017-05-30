using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektGmina
{
    public partial class additionalForm : Form
    {
        string id_button;
        private string path, connectionString;
        public additionalForm(string id_button)
        {
            InitializeComponent();
            path = Directory.GetCurrentDirectory().ToString();
            connectionString = "provider=Microsoft.JET.OLEDB.4.0;" +
                          "data source = " + path + @"\gminaCmentarz.mdb; Jet OLEDB:Database Password=ugwierzbica";
            this.id_button = id_button;

            string text = null;

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT info FROM buttonList WHERE id_button =" + id_button + "";
                DataSet ds = new DataSet();

                OleDbDataAdapter da = new OleDbDataAdapter(sqlQuery, con);

                using (OleDbCommand command = new OleDbCommand(sqlQuery, con))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(ds);
                }

                DataTable dt = new DataTable();

                dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    text = dr["info"].ToString();
                }
                //dataGridView1.AllowUserToAddRows = false;


            }
            if (text.Equals(""))
            {
                panel_add.Visible = true;
                panel_view.Visible = false;
                additionalView.Text = "Brak dodatkowych uwag";
            }
            else
            {
                panel_add.Visible = false;
                panel_view.Visible = true;
                additionalView.Text = text;
                textBox1.Text = text;
            }

        }

        private void additionalForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel_view.Visible = false;
            panel_add.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "UPDATE buttonList SET buttonList.info = '"+textBox1.Text+"' WHERE buttonList.id_button=" + id_button;

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                }catch(Exception esa)
                {
                    Console.WriteLine(esa.Message);
                }

             }
            this.Close();
        }

    }
}
