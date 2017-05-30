using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms.VisualStyles;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace projektGmina
{

    public partial class Form1 : Form
    {
        string name = "";
        ToolTip toolTip1 = new ToolTip();
        int oldBtnTop;
        bool vertic;
        bool isDragged = false;
        Point ptOffset, newPoint;
        private int x_loc, y_loc, old_locX, old_locY;
        private string path, connectionString;
        public bool details, check;
        private bool fromDetailsForm;

        public List<MyButton> buttons = new List<MyButton>();
        public Form1()
        {

        }
        public Form1(bool check, bool fromDetailsForm)
        {

   

            this.check = check;
            this.fromDetailsForm = fromDetailsForm;
            InitializeComponent();

            panel3.DoubleClick += Panel3_DoubleClick;
            panel3.MouseDown += Panel3_MouseDown;

            path = Directory.GetCurrentDirectory().ToString();
            connectionString = "provider=Microsoft.JET.OLEDB.4.0;" +
                          "data source = " + path + @"\gminaCmentarz.mdb;";

            draw_button();
 
        }    
        private void draw_button()
        {


            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sqlCommand = "SELECT * FROM buttonList";
                    OleDbDataAdapter da = new OleDbDataAdapter(sqlCommand,con);

                    DataSet dataSet = new DataSet();
                    da.Fill(dataSet, "buttonList");

                    DataTable dataTable = dataSet.Tables[0];
                    foreach (DataRow dr in dataTable.Rows)
                    {


                        if (1 == (int)dr["vertic"])
                        {
                            vertic = true;
                        } else
                        {
                            vertic = false;
                        }

                        int x = (int)dr["x"];
                        int y = (int)dr["y"];
                        MyButton btn = new MyButton();
                        {
                            btn.ForeColor = Color.Black;
                            btn.FlatStyle = FlatStyle.Standard;
                            btn.Font = new Font(this.Font.Name, 7.0F, this.Font.Style);
                            //btn.BackColor = Color.FromArgb(231,241,246);
                            btn.BackColor = Color.FromArgb(235 , 235, 245);
                            btn.Location = new Point(x, y);
                            btn.Name = "btn" + x + y;
                            btn.Tag = dr["id_button"];
                            btn.Cursor = Cursors.Hand;
                            btn.Tag2 = 0;
                            if (vertic == true)
                            {
                                btn.Size = new Size(20, 40);
                                if (1 == (int)dr["doubled"])
                                {
                                    btn.Text = "Pod.";
                                    btn.Tag2 = 1;
                                    btn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
                                }
                            }
                            if (dr["typ"].Equals("Ziemny") || dr["typ"].Equals("ziemny"))
                            {
                                btn.Text = "z";
                                btn.Tag3 = true;
                                btn.BackColor = Color.DarkGreen;
                            }

                            if (vertic == false)
                            {
                                btn.Size = new Size(40, 20);
                                if (1 == (int)dr["doubled"])
                                {
                                    btn.Text = "Pod.";
                                    btn.Tag2 = 1;
                                    btn.BackColor = System.Drawing.SystemColors.ControlDark;
                                }
                            }


 
                            if (dr["reserved"].ToString() == "1")
                            {
                                btn.Text = "wyk.";
                                btn.reserved = 1;
                                btn.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                            }

                            if (!dr["info"].ToString().Equals(""))
                            {
                                btn.Text += "/?";
                            }
                            string sqlQuery = "SELECT buttonList.number,buttonList.kid, buttonList.doubled, buttonList.alley, buttonList.row,buttonList.typ,buttonList.additional,buttonList.reserved, dane.*, buttonList.id_button" +
                                                  " FROM buttonList INNER JOIN dane ON buttonList.[id_button] = dane.[id_button] where buttonList.id_button=" + dr["id_button"].ToString() + "; ";



                           //  sqlQuery = "SELECT * FROM DANE WHERE id_button = "+ dr["id_button"].ToString();

                            DataSet ds1 = new DataSet();

                            
                            using (OleDbCommand command = new OleDbCommand(sqlQuery, con))
                            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                            {
                                adapter.Fill(ds1);
                            }

                            DataTable dt1 = new DataTable();
                            dt1 = ds1.Tables[0];
                            if (dt1.Rows.Count == 0 && btn.reserved == 0)
                            {
                                btn.Text = "wolny";
                                btn.empty = true;
                                btn.BackColor = Color.LightGoldenrodYellow;

                            }
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                 name = name + "\n"+dr1["Imie_nazwisko"].ToString();



                            }
                            btn.number = dr["number"].ToString();
   
                            
                            toolTip1.SetToolTip(btn, dr["alley"] + " / " + dr["row"] + " / " + dr["number"] + "\n" + name);
                            toolTip1.IsBalloon = true;
                            toolTip1.OwnerDraw = true;
                            name = "";


                            if ((int)dr["kid"] == 1)
                            {
                                btn.Text = "dziec";
                                btn.BackColor = Color.Yellow;
                                btn.kid = true;
                            }
                            btn.TabIndex = 0;
                            btn.Dock = DockStyle.None; 
                            btn.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                            btn.UseVisualStyleBackColor = false;
                            oldBtnTop = btn.Top;
                            btn.Visible = true;

                        }
                        btn.Click += new EventHandler(btn_Click);

                   
                        btn.MouseDown += btn_MouseDown;
                        btn.MouseUp += Btn_MouseUp;
                        btn.MouseMove += btn_MouseMove;

                        buttons.Add(btn);

                        panel3.Controls.Add(btn);

                        
                    }

                } catch (Exception e)
                {
                    MessageBox.Show("Błąd połączenie z bazą danych\n\n" + e.Message);
                }
            }
        }

        MyButton butek;

        private void Panel3_MouseDown(object sender, MouseEventArgs e)
        {
            x_loc = e.X;
            y_loc = e.Y;
        }
        private void Panel3_DoubleClick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {

                if(checkBox2.Checked == true)
                {
                    vertic = false;
                }else
                {
                    vertic = true;
                }

                Button btn = new Button();
                {
                    if(vertic == true)
                    {
                        btn.Location = new Point(x_loc - 10, y_loc - 20);
                        x_loc -= 10; y_loc -= 20;
                    }
                    else
                    {
                        btn.Location = new Point(x_loc - 20, y_loc - 10);
                        x_loc -= 20; y_loc -= 10;
                    }


                    btn.Name = "btn" + x_loc + y_loc;
                    if(vertic == true)
                        btn.Size = new Size(20, 40);
                    if(vertic == false)
                        btn.Size = new Size(40, 20);
                    if(checkBox3.Checked == true)
                    {
                        btn.Size = new Size(40, 40);
                    }
                    btn.TabIndex = 0;
                    btn.UseVisualStyleBackColor = true;
                    btn.Visible = true;
                }
                btn.Click += new EventHandler(btn_Click);

                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    try
                    {

                        OleDbCommand cmd = new OleDbCommand();
                        cmd.CommandType = CommandType.Text;
                        if (vertic)
                        {
                            cmd.CommandText = "INSERT INTO buttonList(x, y,vertic,doubled)VALUES(@x, @y,1,@doubled)";
                        }
                        else
                        {
                            cmd.CommandText = "INSERT INTO buttonList(x, y,vertic,doubled)VALUES(@x, @y,0,@doubled)";
                        }
                        
                         
                        cmd.Parameters.AddWithValue("@x", x_loc);
                        cmd.Parameters.AddWithValue("@y", y_loc);
                        if (checkBox3.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@doubled", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@doubled", 0);
                        }
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();

                        string sqlCommand = "SELECT * FROM buttonList where x="+x_loc+" and y="+y_loc;
                        OleDbDataAdapter da = new OleDbDataAdapter(sqlCommand, con);
                        DataSet dataSet = new DataSet();
                        da.Fill(dataSet, "buttonList");

                        DataTable dataTable = dataSet.Tables[0];
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            btn.Tag = dr["id_button"];
                        }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Błąd połączenie z bazą danych\n\n" + ee.Message);
                    }
                }

                Console.WriteLine(btn.Name);
                //panel3.Controls.Add(btn);

                Properties.Settings.Default.checkbox = checkBox1.Checked;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.checkbox2 = checkBox2.Checked;
                Properties.Settings.Default.Save();
                Hide();
                Form1 frm1 = new Form1(Properties.Settings.Default.checkbox,false);
                frm1.Show();

            }
        }

        bool ifsame;
        private void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            MyButton btn = (MyButton)sender;

            if (checkBox1.Checked == true)
            {
                isDragged = false;


                if (newPoint.X != 0 && newPoint.Y != 0)
                    using (OleDbConnection con = new OleDbConnection(connectionString))
                    {
                        int old_x_min, old_x_max, old_y_min, old_y_max;
                        old_x_min = old_locX - 5;
                        old_x_max = old_locX + 5;
                        old_y_min = old_locY - 5;
                        old_y_max = old_locY + 5;

                        int point_x_min, point_x_max, point_y_min, point_y_max;

                        point_x_min = newPoint.X - 9;
                        point_x_max = newPoint.X + 9;
                        point_y_max = newPoint.Y + 11;
                        point_y_min = newPoint.Y - 11;

                        string sqlCommand = "SELECT * from buttonList where x BETWEEN " + point_x_min + " and " + point_x_max + " and y BETWEEN " + point_y_min + " AND " + point_y_max + "";
                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlCommand, con);
                        Console.WriteLine(sqlCommand);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet, "buttonList");
                        DataTable dataTable = dataSet.Tables[0];



                        foreach (DataRow dr in dataTable.Rows)
                        {
                            if ((int)dr["id_button"] == (int)btn.Tag)
                            {
                                if(dataTable.Rows.Count == 1)
                                ifsame = true;

                            }else
                            {
                                ifsame = false;
                            }
                        }
                        if ((dataTable.Rows.Count == 0 && newPoint.Y > 34) || ifsame)
                        {
                            OleDbCommand cmd = new OleDbCommand();
                            cmd.CommandType = CommandType.Text;
                            // cmd.CommandText = "UPDATE buttonList SET buttonList.x = " + newPoint.X + ", buttonList.y = " + newPoint.Y + " WHERE(((buttonList.[id_button]) = (SELECT id_button from buttonList where x BETWEEN " + old_x_min + " and " + old_x_max + " and y BETWEEN " + old_y_min + " AND " + old_y_max + ")))";
                            cmd.CommandText = "UPDATE buttonList SET buttonList.x = " + newPoint.X + ", buttonList.y = " + newPoint.Y + " WHERE buttonList.[id_button] = "+btn.Tag;
                            cmd.Connection = con;
                            con.Open();
                            Console.WriteLine(cmd.CommandText);

                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception er)
                            {
                                Console.WriteLine(er.Message);
                                cmd.CommandText = "DELETE FROM buttonList where id_button = (SELECT TOP 1 id_button FROM buttonList WHERE x BETWEEN " + old_x_min + " and " + old_x_max + " and y BETWEEN " + old_y_min + " AND " + old_y_max + ")";
                                //cmd.ExecuteNonQuery();
                            }



                            con.Close();
                        }
                        else
                        {
                            MyButton button1 = (MyButton)sender;
                            {
                                button1.Location = new Point(old_locX, old_locY);
                                button1.Name = "btn" + old_locX + old_locY;
                            }
                            MessageBox.Show("Niedozwolone miejsce!", "Uwaga",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }



                    }
            }
        }
        private void btn_MouseDown(object sender, MouseEventArgs e)
        {

            Button button1 = (Button)sender;
            if (e.Button == MouseButtons.Left)
            {
                isDragged = true;
                Point ptStartPosition = button1.PointToScreen(new Point(e.X, e.Y));

                ptOffset = new Point();
                ptOffset.X = button1.Location.X - ptStartPosition.X;
                ptOffset.Y = button1.Location.Y - ptStartPosition.Y;

                old_locX = button1.Location.X;
                old_locY = button1.Location.Y;
            }
            else
            {
                isDragged = false;
            }

        }
        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Button button1 = (Button)sender;
                if (isDragged)
                {

                    newPoint = button1.PointToScreen(new Point(e.X, e.Y));
                    newPoint.Offset(ptOffset);
                    button1.Location = newPoint;
                    button1.Name = "btn" + newPoint.X + newPoint.Y;


                }
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
          
            if (!checkBox1.Checked)
            {
                Button btn = (Button)sender;
                btn.BackColor = System.Drawing.SystemColors.HotTrack;     
                detailsForm df = new detailsForm(btn.Tag.ToString(),this,panel3);
                df.Show();
            }

        }
        public void updateColor()
        {
            foreach (MyButton c in buttons)
            {
                c.Text = "";
                c.ForeColor = Color.Black;
        
                c.BackColor = Color.FromArgb(235, 235, 245);
                if (c.Tag2 == 1)
                { 
                    c.BackColor = SystemColors.ControlDark;
                c.Text = "Pod.";
                 }
                if (c.reserved == 1)
                {
                    c.Text = "wyk.";
                    c.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                }
                if (c.Tag3 == true && c.reserved == 0)
                {
                    c.BackColor = Color.DarkGreen;
                    c.Text = "Z";

                }
                if (c.empty && c.reserved == 0)
                {
                    c.BackColor = Color.LightGoldenrodYellow;
                    c.Text = "wolny";
                }
                if (c.kid)
                {
                    c.BackColor = Color.Yellow;
                    c.Text = "dziec";
                }
            }
        }
        public void blackColor(string id)
        {
            foreach (MyButton c in buttons)
            {
                c.BackColor = SystemColors.Control;
                if(c.Tag2 == 1)
                {
                    c.BackColor = SystemColors.ControlDark;
                }
                else if (c.empty)
                {
                    c.BackColor = Color.White;
                }
                if (c.Tag.ToString().Equals(id))
                {
                    c.BackColor = Color.Black;
                    c.ForeColor = Color.White;
                    c.Text = c.number;
                }

                
            }
        }

        private void label84_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wykonał: Patryk Kaszczuk\nNa potrzeby Urzędu Gmina Wierzbica\n\n@Wszelkie prawa zastrzeżone :)","Autor");
        }
        private void searchButton_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (MyButton c in buttons)
                {
                    c.BackColor = Color.FromArgb(235, 235, 245);
                    if (c.Tag2 == 1)
                        c.BackColor = SystemColors.ControlDark;
                    if (c.reserved == 1)
                    {
                        c.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                    }
                    if (c.Tag3 == true && c.reserved == 0)
                    {
                    c.BackColor = Color.DarkGreen;
                     
                    }
                    if (c.empty && c.reserved == 0)
                    {
                        c.BackColor = Color.LightGoldenrodYellow;
                    }
                    if (c.kid)
                    {
                        c.BackColor = Color.Yellow;
                    }
                }

            }
            catch (Exception re)
            {
                Console.WriteLine("to tu: " + re.Message);
            }

            if (searchBoxName.Text == "")
            {
                MessageBox.Show("Uzupełnij dane!", "Uzupełnij dane",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            else
            {
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        
                        string sqlQuery = "SELECT dane.Imie_nazwisko, dane.nr_ew_osoby, dane.data_pochowania, dane.osoba_wykup_grob, dane.adres, dane.nr_faktury, buttonList.* " +
                                                " FROM buttonList INNER JOIN dane ON buttonList.[id_button] = dane.[id_button]" +
                                                 " WHERE((dane.Imie_nazwisko)LIKE '%" + searchBoxName.Text + "%') OR ((dane.osoba_wykup_grob)LIKE '%" + searchBoxName.Text + "%')";

                        
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
                            foreach (MyButton c in buttons)
                            {
                                if ((int)c.Tag == (int)dr["id_button"])
                                {
                                    c.BackColor = SystemColors.HotTrack;
                                    butek = c;
                                }
                            }

                        }
                        
                        string sqlQuery2 = "SELECT * from buttonList  WHERE((buttonList.additional)LIKE '%" + searchBoxName.Text + "%')";
                    
                        da = new OleDbDataAdapter(sqlQuery2, con);

                        using (OleDbCommand command = new OleDbCommand(sqlQuery2, con))
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            adapter.Fill(ds);
                        }
                        dt = new DataTable();
                        dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            foreach (MyButton c in buttons)
                            {
                                if ((int)c.Tag == (int)dr["id_button"])
                                {
                                    c.BackColor = SystemColors.HotTrack;
                                    butek = c;
                                }
                            }

                        }

                    }
                    catch (Exception errorr)
                    {
                        Console.WriteLine(errorr.Message + "że tu");
                    }

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AllData ad = new projektGmina.AllData();
            ad.Show();
        }
        private void checkBox1_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.checkbox = checkBox1.Checked;
            Properties.Settings.Default.checkbox2 = checkBox2.Checked;
            Properties.Settings.Default.Save();
            Hide();

            Form1 frm1 = new Form1(checkBox1.Checked, false);
            frm1.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            try
            {

            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleHybridMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;

            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            List<PointLatLng> pointss = new List<PointLatLng>();


            pointss.Add(new PointLatLng(51.273474, 23.302216));
            pointss.Add(new PointLatLng(51.273508, 23.302786));
            pointss.Add(new PointLatLng(51.273037, 23.302816));
            pointss.Add(new PointLatLng(51.273024, 23.302204));

            GMapPolygon polygon = new GMapPolygon(pointss, "mypolygone");

            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Add(polygon);
            gMapControl1.Overlays.Add(polyOverlay);
             
                gMapControl1.Position = new GMap.NET.PointLatLng(51.273212, 23.302492);

               
            }
            catch
            {
                maplabel.Visible = true;                
            }
            if (!fromDetailsForm)
                if (Properties.Settings.Default.checkbox)
                {
                    checkBox1.Checked = Properties.Settings.Default.checkbox;

                }
                else
                {

                    checkBox1.Checked = false;
                }

            if (checkBox1.Checked == true)
            {
                label24.Visible = Visible;
                panel2.Visible = Visible;

            }
            checkBox2.Checked = Properties.Settings.Default.checkbox2;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.checkbox = checkBox1.Checked;
            Properties.Settings.Default.checkbox2 = checkBox2.Checked;
            Properties.Settings.Default.Save();

            Environment.Exit(1);
        }



        public class MyButton : Button
        {
            public string number { get; set; }
            public int Tag2 { get; set; }
            public int reserved { get; set; }
            public bool Tag3 { get; set; }
            public bool empty { get; set; }
            public bool kid { get; set; }
            public MyButton()
                : base()
            {

            }

        }
    }

}
