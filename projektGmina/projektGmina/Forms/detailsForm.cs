//
// Zrob to jesli istnieje juz jakis rekord to niech sie nie swieci ze jest zarezerwowany. teraz jest tak ze zmienia zarezerwowany na 0 jak dodaje nowy rekord 
// ale jak dodam rekord a potem didatkowe informacjee to znowu sie sweici wiec jak dodam dodatkowe informacje ale do grobu gdzie ktos lezy 
// niech ustawai reserved na 0
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektGmina
{
    public partial class detailsForm : Form
    {
        PrintPreviewDialog previewdlg = new PrintPreviewDialog();
        
        Form1 frm1;

        bool saveSuccesfull = true;
        private bool details;
        private string id_button;
        private string path, connectionString;
        private void detailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saveSuccesfull)
            {
                Form1 frm2 = new Form1(false, true);
                frm2.Show();
                frm1.Hide();
            }else
            {
                frm1.updateColor();
                
            }

        }

        DataSet ds;
        OleDbDataAdapter da;
        Panel pnl3;
        public detailsForm(string tag, Form1 frm1,Panel pnl3)
        {
            path = Directory.GetCurrentDirectory().ToString();
            connectionString = "provider=Microsoft.JET.OLEDB.4.0;" +
                          "data source = " + path + @"\gminaCmentarz.mdb; Jet OLEDB:Database Password=ugwierzbica";

            this.pnl3 = pnl3;
            this.frm1 = frm1;
            id_button = tag;
            

            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(registrationSpaceNumber, "7 /2007");

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlCommand = "SELECT * FROM dane Where id_button =" + id_button;
                    OleDbDataAdapter da = new OleDbDataAdapter(sqlCommand, con);

                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet, "buttonList");

                    DataTable dataTable = dataSet.Tables[0];

                    foreach (DataRow dr in dataTable.Rows)
                    {
                       textBox3.Text = dr["osoba_wykup_grob"].ToString();
                        textBox1.Text = dr["adres"].ToString();
                        textBox2.Text = dr["nr_faktury"].ToString();
                    }

                    if (dataTable.Rows.Count > 0)
                    {
                        details = true;

                    } else
                    {
                        
                        details = false;
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            printDocument1.PrintPage += new PrintPageEventHandler(printdocument1_PrintPage);

            dataGridView2.Visible = true;
            label1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            panel4.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
           
            panel1.Width = 569;
            Width = 569;
            panel4.Width = 569;
            dataGridView1.Width = 569;
            button3.Location = new Point(button3.Location.X - 247, button3.Location.Y);
            //label1.Text = "Ni ma __id="+id_button;

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT buttonList.number,buttonList.kid, buttonList.doubled, buttonList.alley, buttonList.row,buttonList.typ,buttonList.reserved,buttonList.additional,buttonList.info, buttonList.id_button" +
                                    " FROM buttonList where buttonList.id_button=" + id_button + "; ";

                 ds = new DataSet();

                da = new OleDbDataAdapter(sqlQuery, con);


                using (OleDbCommand command = new OleDbCommand(sqlQuery, con))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(ds);
                }

                DataTable dt = new DataTable();

                // DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();

                dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {

                    alleyComboBox.Text = dr["alley"].ToString();
                    rowComboBox.Text = dr["row"].ToString();
                    numberTextBox.Text = dr["number"].ToString();
                    typeComboBox.Text = (int)dr["doubled"] == 1 ? "Podwójny" : "Pojedynczy";
                    comboBox1.Text = dr["typ"].ToString();
                    if ((int)dr["kid"] == 1)
                    {
                        checkBox2.Checked = true;
                    }else
                    {
                        checkBox2.Checked = false;
                    }
                    if(!dr["additional"].ToString().Equals(""))
                    {
                        string text = dr["additional"].ToString();

                        textBox3.Text = text.Remove(text.IndexOf(":") - 5);
                        string adres = text.Substring(text.IndexOf(":")+2);
                        adres = adres.Substring(0,adres.IndexOf(":")-10);
                        textBox1.Text = adres;
                        string faktura = text.Substring(text.IndexOf("y:")+2);
                        faktura = faktura.Substring(0, faktura.IndexOf("u:")-11);
                        textBox2.Text = faktura;

                        registrationSpaceNumber.Text = text.Substring(text.LastIndexOf(":")+2);
                    }
                    if (!dr["info"].ToString().Equals(""))
                    {
                        label16.BackColor = Color.Red;
                    }else
                    {
                        label16.BackColor = SystemColors.Control;
                    }

                }
            }


        }
        private void getData()
        {
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT buttonList.number, buttonList.doubled, buttonList.alley, buttonList.row,buttonList.typ,buttonList.additional,buttonList.reserved, dane.*, buttonList.id_button" +
                                    " FROM buttonList INNER JOIN dane ON buttonList.[id_button] = dane.[id_button] where buttonList.id_button=" + id_button + "; ";

                ds = new DataSet();

                da = new OleDbDataAdapter(sqlQuery, con);

                using (OleDbCommand command = new OleDbCommand(sqlQuery, con))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    adapter.Fill(ds,"dan");
                }

                DataTable dt = new DataTable();

                // DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                    details = true;
                foreach (DataRow dr in dt.Rows)
                {
                    int rowCount = dataGridView1.NewRowIndex;
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[rowCount].Clone();

                    string typ;
                    if ((int)dr["doubled"] == 1)
                    {
                        typ = "Podwójny " + dr["typ"];
                    }
                    else
                    {
                        typ = "Pojedynczy " + dr["typ"];
                    }

                    label7.Text = "Informacje o miejscu pochowania:\n\nAleja: "+dr["alley"]+"\nRząd: "+dr["row"]+"\nNumer: "+dr["number"]+"\nTyp: "+typ;
                    //label1.Text = "id:" + dr["id"] + "\ntext:" + dr["tekst"] + "\nbutton Id:" + dr["id_button"];
                    /*
                    row.Cells[0].Value = dr["alley"];
                    row.Cells[1].Value = dr["row"];
                    row.Cells[2].Value = dr["number"];
                    */

                    row.Cells[0].Value = dr["Imie_Nazwisko"];
                    row.Cells[1].Value = dr["nr_ew_osoby"];
                    row.Cells[2].Value = dr["nr_ew_grobu"];
                    row.Cells[3].Value = dr["data_pochowania"];

                    row.Cells[4].Value = dr["osoba_wykup_grob"];
                    row.Cells[5].Value = dr["adres"];
                    row.Cells[6].Value = dr["nr_faktury"];
                    dataGridView1.Rows.Add(row);
                    
                }
                dataGridView1.AllowUserToAddRows = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            YesNoForm ys = new YesNoForm(id_button,this);
            ys.Show();

            saveSuccesfull = false;
         } // Usuń
        public void deleteRow(string id_button,bool all)
        {
            string path = Directory.GetCurrentDirectory().ToString();
            string connectionString = "provider=Microsoft.JET.OLEDB.4.0;" +
                          "data source = " + path + @"\gminaCmentarz.mdb; Jet OLEDB:Database Password=ugwierzbica";
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "DELETE FROM dane where id_button = " + id_button;

                //OleDbDataAdapter da = new OleDbDataAdapter(sqlQuery, connectionString);
                OleDbCommand cmd = new OleDbCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                    sqlQuery = "UPDATE buttonList SET buttonList.reserved = 0, buttonList.additional = '' where id_button = "+id_button;
                if (all)
                    sqlQuery = "DELETE FROM buttonList where id_button=" + id_button;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlQuery;

                cmd.ExecuteNonQuery();
                con.Close();
            }
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (details)
            {
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                label1.Visible = false;
               
                getData();
                Width = 1050;
                dataGridView2.Visible = false;
                panel4.Visible = true;
                Print(panel4);
                saveSuccesfull = true;
            }else
            {
                MessageBox.Show("Brak danych do wydruku.", "Uwaga!");
            }

            panel4.Visible = false;
            text = DateTime.Now.ToString();
        } // Drukuj
        private void button3_Click(object sender, EventArgs e)
        {
            string Alejka = alleyComboBox.Text;
            string rzad = rowComboBox.Text;
            string miejsce = numberTextBox.Text;
            string typ = typeComboBox.Text;
            string rodzaj = comboBox1.Text;

            string nr_ew_grobu = registrationSpaceNumber.Text;
            string name = secondNameBox.Text;
            string nr_ew_osoby = registrationPersonNumber.Text;
            string data_pochowania = dateOfBurial.Text;
            string osoba_wykup_grob = textBox3.Text;
            string adres = textBox1.Text;
            string nr_faktury = textBox2.Text;

            bool czyDziecko = checkBox2.Checked;

            if (edit)
            {
                try
                {
                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        con.Open();
                        dadap = new OleDbDataAdapter("SELECT id, Imie_nazwisko AS 'Imie Nazwisko', nr_ew_osoby AS 'Nr ew osoby', nr_ew_grobu AS 'Nr ew grobu', data_pochowania AS 'Data pochowania', osoba_wykup_grob AS 'Osoba wykup grób', adres, nr_faktury AS 'faktura' FROM DANE WHERE id_button = " + id_button, con);

                        cmdbuld = new OleDbCommandBuilder(dadap);

                        dadap.Update(dset, "dan");
                        MessageBox.Show("Zaaktualizowano!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                edit = false;
            }else
            {
                updateRow(Alejka, rzad, miejsce, typ, rodzaj, nr_ew_grobu, name, nr_ew_osoby, data_pochowania, osoba_wykup_grob, adres, nr_faktury,czyDziecko);
                //dataGridView2.Rows.Clear();

                MessageBox.Show("Informacje zostały zapisane.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
            getPerson();
        } // Zapisz

        Bitmap MemoryImage,PlanImage;
        public void GetPrintArea(Panel pnl,bool plan, Panel planPnl)
        {

            //this.Width = 800;
            pnl.Width = 1100;
            int wid = dataGridView1.Width;
            dataGridView1.Width = 1100;
            pnl.Font = new Font("Arial", 11.0f);

            MemoryImage = new Bitmap(pnl.Width, pnl.Height);

            
            pnl.BackColor = Color.White;
            dataGridView1.BackgroundColor = Color.White;
            panel4.BorderStyle = BorderStyle.None;

            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
            image.Add(MemoryImage);
            if (plan)
            {
                    frm1.blackColor(id_button);
                    pnl3.BackColor = Color.White;



                    PlanImage = new Bitmap(pnl3.Width, pnl3.Height);
                    pnl3.DrawToBitmap(PlanImage, new Rectangle(0, 0, pnl3.Width - 500, pnl3.Height));
                    frm1.updateColor();
                    pnl3.BackColor = SystemColors.Control;
                    image.Add(PlanImage);

            }
        }
        List<Bitmap> image = new List<Bitmap>();
        int i;
        string text = DateTime.Now.ToString();
        void printdocument1_PrintPage(object sender, PrintPageEventArgs e)
        {


            //Rectangle pagearea = e.PageBounds;

            //e.Graphics.DrawImage(MemoryImage, (pagearea.Width) - (this.panel1.Width ),this.panel1.Location.Y);
            //e.Graphics.DrawImage(MemoryImage, 0, 0);



            float x = (float)printDocument1.DefaultPageSettings.Bounds.X;
            float y = (float)printDocument1.DefaultPageSettings.Margins.Top - 100;
            float w = (float)printDocument1.DefaultPageSettings.Bounds.Width / 2;

            y = (float)(printDocument1.DefaultPageSettings.Bounds.Top + printDocument1.DefaultPageSettings.Bounds.Height - 30);
            if (i == 1)
            {
                y = (float)(printDocument1.DefaultPageSettings.Bounds.Top + printDocument1.DefaultPageSettings.Bounds.Height + 180);
                w = (float)printDocument1.DefaultPageSettings.Bounds.Width - 300;
            }
            float h = (float)30;
            
   

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;

            e.Graphics.DrawString(text,this.Font,Brushes.Gray, new RectangleF(x,y,w,h), format);
            e.Graphics.DrawImage(image[i],
                                        e.PageBounds.X,
                                        e.PageBounds.Y);
            
                if (i < image.Count-1)
                {

                i++;
                e.HasMorePages = true;
            }else
            {
                i = 0;
            }
                       

        }        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
     
        private void updateRow(string alejka,string rzad,string miejsce,
            string typ, string rodzaj, string nr_ew_grobu, string name,
            string nr_ew_osoby, string data_pochowania, string osoba_wykup_grob, string adres,string nr_faktury,bool czyDziecko)
        {
           
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = CommandType.Text;

                    int dub = 0; //czy podwojny
                    if (typeComboBox.Text.Equals("Podwójny"))
                    {
                        dub = 1;
                    }
                    if (typeComboBox.Text.Equals("Pojedynczy"))
                    {
                        dub = 0;
                    }
                    int kide = 0;
                    if (checkBox2.Checked)
                    {
                        kide = 1;
                    }
                    else
                    {
                        kide = 0;
                    }

                    cmd.CommandText = "UPDATE buttonList SET buttonList.doubled =@dub, buttonList.number = @number, buttonList.alley = @alley, buttonList.row = @row, buttonList.typ = @typ,buttonList.kid = "+kide+" WHERE buttonList.id_button=@id";

                    cmd.Parameters.AddWithValue("@dub", dub.ToString());
                    cmd.Parameters.AddWithValue("@number", miejsce);
                    cmd.Parameters.AddWithValue("@alley", alejka);
                    cmd.Parameters.AddWithValue("@row", rzad);
                    cmd.Parameters.AddWithValue("@typ", rodzaj);
                    cmd.Parameters.AddWithValue("@id", id_button);
                    /*
                   
                    cmd.Parameters.AddWithValue("@kid", "1");
                    */

                    cmd.Connection = con;
                    con.Open();
                   
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if(checkBox1.Checked == true)
                    {
                        string sqlCommande = "SELECT * FROM dane Where id_button =" + id_button;
                        OleDbDataAdapter daa = new OleDbDataAdapter(sqlCommande, con);

                        DataSet dataSett = new DataSet();
                        daa.Fill(dataSett, "dane");

                        DataTable dataTablee = dataSett.Tables[0];
                        int reserved = 1;
                        if (dataTablee.Rows.Count > 0)
                        {
                           reserved = 0;
                        }

                        cmd.CommandText = "UPDATE buttonList SET buttonList.reserved ='"+reserved+"', buttonList.additional = '"+osoba_wykup_grob+"Adres: "+ adres +"Nr faktury: "+nr_faktury+"nr ew. grobu: "+nr_ew_grobu+"' WHERE buttonList.id_button="+id_button;
                     
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                      
                        con.Close();
                    }else
                    {
                        cmd.CommandText = "INSERT INTO dane(id_button,Imie_nazwisko,nr_ew_osoby,data_pochowania,osoba_wykup_grob,adres,nr_faktury,nr_ew_grobu) VALUES(" + id_button + ",'" + name + "','" + nr_ew_osoby + "','" + data_pochowania + "','" + osoba_wykup_grob + "','" + adres + "','" + nr_faktury + "','" + nr_ew_grobu + "')";
                        cmd.Connection = con;
                        con.Open();
                        if (!name.Equals("") || !nr_ew_osoby.Equals(""))
                        {
                            cmd.ExecuteNonQuery();
                          
                        }

                        con.Close();

                        cmd.CommandText = "UPDATE buttonList SET buttonList.reserved ='0' WHERE buttonList.id_button=" + id_button;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }





                    string sqlCommand = "SELECT * FROM dane Where id_button =" + id_button;
                    OleDbDataAdapter da = new OleDbDataAdapter(sqlCommand, con);

                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet, "dane");

                    DataTable dataTable = dataSet.Tables[0];

                    if (dataTable.Rows.Count > 1)
                    {
                        cmd.CommandText = "UPDATE buttonList SET ButtonList.doubled = 1 WHERE buttonList.id_button="+id_button;
  
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                   

                }
                catch (Exception ee)
                {
                    Console.WriteLine("Błąd połączenie z bazą danych\nNie udało się zaaktualizowanie\n" + ee.Message);
                }
            }
            saveSuccesfull = false;
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        public void Print(Panel pnl)
        {
            Hide();
            //pannel = pnl;
            
            GetPrintArea(pnl,true,pnl3);
            previewdlg.Size = new Size(900, 760);
            previewdlg.PrintPreviewControl.Zoom = 1.0;
            printDocument1.DefaultPageSettings.Landscape = true;
            Margins mr = new Margins(5, 0, 0, 0);
            printDocument1.DefaultPageSettings.Margins = mr;
            previewdlg.Document = printDocument1;


            i = 0;
            
            PrintDialog pd = new PrintDialog();
            if (DialogResult.OK == pd.ShowDialog())
            {
                ((Form)previewdlg).WindowState = FormWindowState.Maximized;

                previewdlg.ShowDialog();
                // don't forget to detach the event handler when you are done

            }
            Close();

           detailsForm df = new detailsForm(id_button, frm1, pnl3);
           df.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            try
            {
                int n = int.Parse(numberTextBox.Text);
                numberTextBox.Text = n + 1 + "";
            }catch(Exception error)
            {
                MessageBox.Show("Spróbuj wpisać ręcznie\n\n\n"+error.Message, "Błąd");
            }
        }
        private void down_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(numberTextBox.Text);
                numberTextBox.Text = n - 1 + "";
            }catch(Exception error)
            {
                MessageBox.Show("Spróbuj wpisać ręcznie\n\n\n"+error.Message,"Błąd");
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                

                secondNameBox.Enabled = false;
                registrationPersonNumber.Enabled = false;
                dateOfBurial.Enabled = false;



            }else
            {
               
                secondNameBox.Enabled = true;
                registrationPersonNumber.Enabled = true;
                dateOfBurial.Enabled = true;
            }
        }
        private void label16_Click(object sender, EventArgs e)
        {
            additionalForm af = new additionalForm(id_button);
            af.Show();
        }
        OleDbCommandBuilder cmdbuld;
        DataSet dset;
        OleDbDataAdapter dadap;
   
        private void detailsForm_Load(object sender, EventArgs e)
        {
            getPerson();
        }
        public void getPerson()
        {
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();

                dset = new DataSet();
                dadap = new OleDbDataAdapter("SELECT id, Imie_nazwisko AS 'Imie Nazwisko', nr_ew_osoby AS 'Nr ew osoby', nr_ew_grobu AS 'Nr ew grobu', data_pochowania AS 'Data pochowania', osoba_wykup_grob AS 'Osoba wykup grób', adres, nr_faktury AS 'faktura' FROM DANE WHERE id_button = " + id_button,con);
                dadap.Fill(dset,"dan");

                DataTable dt = new DataTable();

                dt = dset.Tables[0];
                dataGridView2.DataSource = dset.Tables[0]; 
            }
            dataGridView2.Columns[0].Visible = false;

        }
        bool edit = false;

      
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            edit = true;
        }
        private void dataGridView2_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
        }
        private void printDocument1_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            if(i ==1)
            { 

                e.PageSettings.Landscape = false;
            }
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    con.Open();
                    dadap = new OleDbDataAdapter("SELECT id, Imie_nazwisko AS 'Imie Nazwisko', nr_ew_osoby AS 'Nr ew osoby', nr_ew_grobu AS 'Nr ew grobu', data_pochowania AS 'Data pochowania', osoba_wykup_grob AS 'Osoba wykup grób', adres, nr_faktury AS 'faktura' FROM DANE WHERE id_button = " + id_button, con);

                    cmdbuld = new OleDbCommandBuilder(dadap);

                    dadap.Update(dset, "dan");
                    MessageBox.Show("Zaaktualizowano!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }

}

