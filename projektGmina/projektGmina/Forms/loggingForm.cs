using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projektGmina
{
    public partial class loggingForm : Form
    {
        private string path;
        private string connectionString,sqlCommand,login,password;
        public loggingForm()
        {
            InitializeComponent();

            path = Directory.GetCurrentDirectory().ToString();
            connectionString = "provider=Microsoft.JET.OLEDB.4.0;" +
                          "data source = " + path + @"\gminaCmentarz.mdb; Jet OLEDB:Database Password=ugwierzbica";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string passwordHash;
            login = loginBox.Text;
            password = passwordBox.Text;

            using(MD5 md5Hash = MD5.Create())
            {
                passwordHash = GetMd5Hash(md5Hash,password);
            }

            logIn(connectionString, passwordHash, login);

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
       

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1_Click(button1, e);
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void logIn(string connectionString,string passwordHash,string login)
        {
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                try
                {
                    con.Open();

                    sqlCommand = "SELECT * FROM users WHERE login = @login and password = @passwordHash";

                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlCommand,con);


                    dataAdapter.SelectCommand.Parameters.AddWithValue("@login", login);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@passwordHash", passwordHash);


                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "users");
                    
                    DataTable dataTable = dataSet.Tables[0];

                    if(dataTable.Rows.Count > 0)
                    {
                        Hide();
                        Form1 form1 = new Form1(Properties.Settings.Default.checkbox,false);
                        form1.Show();

                    }else
                    {
                        label3.Text = "";

                        label3.Text = "Sprawdź poprawność danych i spróbuj ponownie.";
                    }


                }catch(Exception e)
                {
                    MessageBox.Show("Błąd " + e.Message, "Błąd");
                }
            }
        }
    }
}
