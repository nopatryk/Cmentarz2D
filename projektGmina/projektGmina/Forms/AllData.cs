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
   
    public partial class AllData : Form
    {
        PrintPreviewDialog previewdlg = new PrintPreviewDialog();
 

        public AllData()
        {
            InitializeComponent();
            //printDocument2.PrintPage += new PrintPageEventHandler(printdocument2_PrintPage);

            string path = Directory.GetCurrentDirectory().ToString();
            string connectionString = "provider=Microsoft.JET.OLEDB.4.0;" +
                          "data source = " + path + @"\gminaCmentarz.mdb; Jet OLEDB:Database Password=ugwierzbica";


            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                con.Open();

                string sqlQuery = "SELECT buttonList.number, buttonList.doubled, buttonList.alley, buttonList.row,buttonList.typ, dane.*, buttonList.id_button" +
                                    " FROM buttonList INNER JOIN dane ON buttonList.[id_button] = dane.[id_button]";
                DataSet ds = new DataSet();

                OleDbDataAdapter da = new OleDbDataAdapter(sqlQuery, con);

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
                    int rowCount = dataGridView2.NewRowIndex;
                    DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[rowCount].Clone();

                    //label1.Text = "id:" + dr["id"] + "\ntext:" + dr["tekst"] + "\nbutton Id:" + dr["id_button"];
                    row.Cells[0].Value = dr["alley"];
                    row.Cells[1].Value = dr["row"];
                    row.Cells[2].Value = dr["number"];
                    if ((int)dr["doubled"] == 1)
                    {
                        row.Cells[3].Value = "Podwójny " + dr["typ"];
                    }
                    else
                    {
                        row.Cells[3].Value = "Pojedynczy " + dr["typ"];
                    }

                    row.Cells[4].Value = dr["Imie_Nazwisko"];
                    row.Cells[5].Value = dr["nr_ew_osoby"].ToString();
                    row.Cells[6].Value = dr["nr_ew_grobu"].ToString();
                    row.Cells[7].Value = dr["data_pochowania"].ToString();

                    row.Cells[8].Value = dr["osoba_wykup_grob"];
                    row.Cells[9].Value = dr["adres"];
                    row.Cells[10].Value = dr["nr_faktury"];
                    dataGridView2.Rows.Add(row);

                }
                dataGridView2.AllowUserToAddRows = false;


            }


        }
        int xdiv = 0, ydiv = 0;
        internal string tempPath { get; set; }
        private int pageIndex = 0;
        internal List<Image> list { get; set; }
        Bitmap image_PctrBx;
        private void button1_Click(object sender,EventArgs e)
        {
            dataGridView2.ScrollBars = ScrollBars.None;
            int hig = dataGridView2.Height;
            int j = dataGridView2.Rows.Count;
            int h = dataGridView2.Rows[0].Height;
            dataGridView2.Height = j * h;
            image_PctrBx = new Bitmap(dataGridView2.Width, dataGridView2.Height);
            dataGridView2.DrawToBitmap(image_PctrBx, new Rectangle(0, 0, dataGridView2.Width, dataGridView2.Height));
            list = new List<Image>();
            Graphics g = Graphics.FromImage(image_PctrBx);
            Brush redBrush = new SolidBrush(Color.White);
            Pen pen = new Pen(redBrush, 3);
            decimal xdivider = image_PctrBx.Width / 1041m;
            xdiv = Convert.ToInt32(Math.Ceiling(xdivider));
            decimal ydivider = image_PctrBx.Height /841m;
            ydiv = Convert.ToInt32(Math.Ceiling(ydivider));
            
            xdiv = 1;
            for (int i = 0; i < xdiv; i++)
            {
                for (int y = 0; y < ydiv; y++)
                {
                    Rectangle r;
                    try
                    {
                        r = new Rectangle(i * Convert.ToInt32(image_PctrBx.Width / xdiv),
                                                    y * Convert.ToInt32(image_PctrBx.Height / ydiv),
                                                    image_PctrBx.Width / xdiv,
                                                    image_PctrBx.Height / ydiv);
                    }
                    catch (Exception)
                    {
                        r = new Rectangle(i * Convert.ToInt32(image_PctrBx.Width / xdiv),
                          y * Convert.ToInt32(image_PctrBx.Height),
                          image_PctrBx.Width / xdiv,
                          image_PctrBx.Height);
                    }

                   
                    g.DrawRectangle(pen, r);
                   
                    list.Add(cropImage(image_PctrBx, r));
                }
            }

            g.Dispose();
           // image_PctrBx.Invalidate();
            //image_PctrBx.Image = list[0];

            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
           
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;
            
            pageIndex = 0;// xdiv * ydiv;
            PrintDialog pd = new PrintDialog();
            if(DialogResult.OK == pd.ShowDialog())
            {
                ((Form)previewDialog).WindowState = FormWindowState.Maximized;

                previewDialog.ShowDialog();
                // don't forget to detach the event handler when you are done
                printDocument.PrintPage -= PrintDocument_PrintPage;
            }
            dataGridView2.Height = hig;
            dataGridView2.ScrollBars = ScrollBars.Vertical;
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            
            // Draw the image for the current page index
            e.Graphics.DrawImageUnscaled(list[pageIndex],
                                         e.PageBounds.X,
                                         e.PageBounds.Y);
           // e.Graphics.DrawString("e",this.Font,Brushes.Red,new PointF());
            // increment page index
            if (pageIndex < list.Count-1)
            {
                pageIndex++;
                e.HasMorePages = true;
                // indicate whether there are more pages or not

            }
            else
            {
                pageIndex = 0;
            }
         
        }
        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, System.Drawing.Imaging.PixelFormat.DontCare);
            return (Image)(bmpCrop);
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
         }
    }
}
