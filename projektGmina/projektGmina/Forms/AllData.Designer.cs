using System.Windows.Forms;

namespace projektGmina
{
    partial class AllData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.alley = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doubled = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.registrationNumberPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nr_ew_grobu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_of_burial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wykup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nr_faktury = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.alley,
            this.row,
            this.Number,
            this.doubled,
            this.name,
            this.registrationNumberPerson,
            this.nr_ew_grobu,
            this.date_of_burial,
            this.wykup,
            this.adress,
            this.nr_faktury});
            this.dataGridView2.Location = new System.Drawing.Point(54, 78);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 20;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView2.Size = new System.Drawing.Size(966, 608);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // alley
            // 
            this.alley.HeaderText = "Aleja";
            this.alley.Name = "alley";
            this.alley.ReadOnly = true;
            this.alley.Width = 55;
            // 
            // row
            // 
            this.row.HeaderText = "Rząd";
            this.row.Name = "row";
            this.row.ReadOnly = true;
            this.row.Width = 57;
            // 
            // Number
            // 
            this.Number.HeaderText = "Numer";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Width = 63;
            // 
            // doubled
            // 
            this.doubled.HeaderText = "Typ";
            this.doubled.Name = "doubled";
            this.doubled.ReadOnly = true;
            this.doubled.Width = 50;
            // 
            // name
            // 
            this.name.HeaderText = "Imie, Nazwisko";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 95;
            // 
            // registrationNumberPerson
            // 
            this.registrationNumberPerson.HeaderText = "nr ew. osoby";
            this.registrationNumberPerson.Name = "registrationNumberPerson";
            this.registrationNumberPerson.ReadOnly = true;
            this.registrationNumberPerson.Width = 85;
            // 
            // nr_ew_grobu
            // 
            this.nr_ew_grobu.HeaderText = "Nr ew. grobu";
            this.nr_ew_grobu.Name = "nr_ew_grobu";
            this.nr_ew_grobu.ReadOnly = true;
            this.nr_ew_grobu.Width = 86;
            // 
            // date_of_burial
            // 
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.date_of_burial.DefaultCellStyle = dataGridViewCellStyle1;
            this.date_of_burial.HeaderText = "Data pochowania";
            this.date_of_burial.Name = "date_of_burial";
            this.date_of_burial.ReadOnly = true;
            this.date_of_burial.Width = 106;
            // 
            // wykup
            // 
            this.wykup.HeaderText = "Osoba wykup. grób";
            this.wykup.Name = "wykup";
            this.wykup.ReadOnly = true;
            this.wykup.Width = 95;
            // 
            // adress
            // 
            this.adress.HeaderText = "Adres";
            this.adress.Name = "adress";
            this.adress.ReadOnly = true;
            this.adress.Width = 59;
            // 
            // nr_faktury
            // 
            this.nr_faktury.HeaderText = "Nr faktury";
            this.nr_faktury.Name = "nr_faktury";
            this.nr_faktury.ReadOnly = true;
            this.nr_faktury.Width = 72;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(248)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1108, 46);
            this.button1.TabIndex = 1;
            this.button1.Text = "Drukuj";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AllData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1108, 750);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Name = "AllData";
            this.Text = "AllData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn alley;
        private System.Windows.Forms.DataGridViewTextBoxColumn row;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn doubled;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn registrationNumberPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn nr_ew_grobu;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_of_burial;
        private System.Windows.Forms.DataGridViewTextBoxColumn wykup;
        private System.Windows.Forms.DataGridViewTextBoxColumn adress;
        private System.Windows.Forms.DataGridViewTextBoxColumn nr_faktury;
        private System.Windows.Forms.Button button1;
        private System.Drawing.Printing.PrintDocument printDocument2;
    }
}