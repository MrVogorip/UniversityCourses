namespace HomeWorkDataBase.View
{
    partial class Function2
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
            this.buttonPoolMaster = new System.Windows.Forms.Button();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFullNameMaster = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPoolMaster
            // 
            this.buttonPoolMaster.Location = new System.Drawing.Point(423, 115);
            this.buttonPoolMaster.Name = "buttonPoolMaster";
            this.buttonPoolMaster.Size = new System.Drawing.Size(75, 23);
            this.buttonPoolMaster.TabIndex = 11;
            this.buttonPoolMaster.Text = "Execute";
            this.buttonPoolMaster.UseVisualStyleBackColor = true;
            this.buttonPoolMaster.Click += new System.EventHandler(this.buttonPoolMaster_Click);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(423, 45);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEnd.TabIndex = 10;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(423, 12);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStart.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 111);
            this.label1.TabIndex = 8;
            this.label1.Text = "2. Issue a list of work performed by this master during a specified period of tim" +
    "e, indicating the cost of each repair operation and the total cost of repairs pe" +
    "rformed by the PUL.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxFullNameMaster
            // 
            this.textBoxFullNameMaster.Location = new System.Drawing.Point(423, 80);
            this.textBoxFullNameMaster.Name = "textBoxFullNameMaster";
            this.textBoxFullNameMaster.Size = new System.Drawing.Size(200, 20);
            this.textBoxFullNameMaster.TabIndex = 15;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Result});
            this.dataGridView1.Location = new System.Drawing.Point(12, 157);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(753, 67);
            this.dataGridView1.TabIndex = 16;
            // 
            // Result
            // 
            this.Result.FillWeight = 700F;
            this.Result.HeaderText = "Result";
            this.Result.MinimumWidth = 15;
            this.Result.Name = "Result";
            this.Result.Width = 700;
            // 
            // Function2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(781, 248);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBoxFullNameMaster);
            this.Controls.Add(this.buttonPoolMaster);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.label1);
            this.Name = "Function2";
            this.Text = "Function2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonPoolMaster;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFullNameMaster;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
    }
}