namespace projektGmina
{
    partial class additionalForm
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
            this.panel_view = new System.Windows.Forms.Panel();
            this.panel_add = new System.Windows.Forms.Panel();
            this.additionalView = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel_view.SuspendLayout();
            this.panel_add.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_view
            // 
            this.panel_view.Controls.Add(this.button2);
            this.panel_view.Controls.Add(this.additionalView);
            this.panel_view.Location = new System.Drawing.Point(12, 12);
            this.panel_view.Name = "panel_view";
            this.panel_view.Size = new System.Drawing.Size(346, 261);
            this.panel_view.TabIndex = 0;
            this.panel_view.Visible = false;
            // 
            // panel_add
            // 
            this.panel_add.Controls.Add(this.button1);
            this.panel_add.Controls.Add(this.textBox1);
            this.panel_add.Controls.Add(this.label2);
            this.panel_add.Location = new System.Drawing.Point(15, 9);
            this.panel_add.Name = "panel_add";
            this.panel_add.Size = new System.Drawing.Size(346, 261);
            this.panel_add.TabIndex = 1;
            this.panel_add.Visible = false;
            // 
            // additionalView
            // 
            this.additionalView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.additionalView.Location = new System.Drawing.Point(12, 17);
            this.additionalView.Name = "additionalView";
            this.additionalView.Size = new System.Drawing.Size(331, 205);
            this.additionalView.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(3, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Dodaj dodatkowe uwagi:\r\n";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(7, 54);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(326, 153);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 228);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Zapisz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(261, 225);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Zmień";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // additionalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 285);
            this.Controls.Add(this.panel_view);
            this.Controls.Add(this.panel_add);
            this.Name = "additionalForm";
            this.Text = "additionalForm";
            this.Load += new System.EventHandler(this.additionalForm_Load);
            this.panel_view.ResumeLayout(false);
            this.panel_add.ResumeLayout(false);
            this.panel_add.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_view;
        private System.Windows.Forms.Label additionalView;
        private System.Windows.Forms.Panel panel_add;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}