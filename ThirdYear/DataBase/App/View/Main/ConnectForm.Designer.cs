namespace HomeWorkDataBase
{
    partial class ConnectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxDataBase = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxPasword = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter server name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter database name";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Enter your username (optional)";
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Enter your user password (optional)";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(260, 12);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(148, 20);
            this.textBoxServer.TabIndex = 4;
            this.textBoxServer.Text = "COMPUTER\\SQLEXPRESS";
            // 
            // textBoxDataBase
            // 
            this.textBoxDataBase.Location = new System.Drawing.Point(260, 40);
            this.textBoxDataBase.Name = "textBoxDataBase";
            this.textBoxDataBase.Size = new System.Drawing.Size(148, 20);
            this.textBoxDataBase.TabIndex = 5;
            this.textBoxDataBase.Text = "CarService";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(260, 74);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(148, 20);
            this.textBoxName.TabIndex = 6;
            // 
            // textBoxPasword
            // 
            this.textBoxPasword.Location = new System.Drawing.Point(260, 103);
            this.textBoxPasword.Name = "textBoxPasword";
            this.textBoxPasword.Size = new System.Drawing.Size(148, 20);
            this.textBoxPasword.TabIndex = 7;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(158, 129);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(114, 46);
            this.buttonConnect.TabIndex = 8;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonConnect);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxPasword);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBoxDataBase);
            this.panel1.Controls.Add(this.textBoxServer);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 181);
            this.panel1.TabIndex = 9;
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(600, 327);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectForm";
            this.Text = "Connect Form";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.TextBox textBoxDataBase;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxPasword;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Panel panel1;
    }
}