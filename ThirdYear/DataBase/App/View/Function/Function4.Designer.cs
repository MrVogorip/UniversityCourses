namespace HomeWorkDataBase.View
{
    partial class Function4
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name_detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxFullNameMaster = new System.Windows.Forms.TextBox();
            this.buttonDetailConsumption = new System.Windows.Forms.Button();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name_detail,
            this.Cost,
            this.end_time});
            this.dataGridView1.Location = new System.Drawing.Point(12, 157);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(462, 206);
            this.dataGridView1.TabIndex = 25;
            // 
            // name_detail
            // 
            this.name_detail.HeaderText = "name_detail";
            this.name_detail.MinimumWidth = 15;
            this.name_detail.Name = "name_detail";
            this.name_detail.Width = 130;
            // 
            // Cost
            // 
            this.Cost.HeaderText = "cost";
            this.Cost.Name = "Cost";
            // 
            // end_time
            // 
            this.end_time.HeaderText = "end_time ";
            this.end_time.Name = "end_time";
            this.end_time.Width = 180;
            // 
            // textBoxFullNameMaster
            // 
            this.textBoxFullNameMaster.Location = new System.Drawing.Point(345, 80);
            this.textBoxFullNameMaster.Name = "textBoxFullNameMaster";
            this.textBoxFullNameMaster.Size = new System.Drawing.Size(200, 20);
            this.textBoxFullNameMaster.TabIndex = 24;
            // 
            // buttonDetailConsumption
            // 
            this.buttonDetailConsumption.Location = new System.Drawing.Point(345, 115);
            this.buttonDetailConsumption.Name = "buttonDetailConsumption";
            this.buttonDetailConsumption.Size = new System.Drawing.Size(75, 23);
            this.buttonDetailConsumption.TabIndex = 20;
            this.buttonDetailConsumption.Text = "Execute";
            this.buttonDetailConsumption.UseVisualStyleBackColor = true;
            this.buttonDetailConsumption.Click += new System.EventHandler(this.buttonDetailConsumption_Click);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(345, 45);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEnd.TabIndex = 19;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(345, 12);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStart.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 110);
            this.label1.TabIndex = 17;
            this.label1.Text = "4. Compile a list of parts consumption for a specified period of time for a given" +
    " master.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Function4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(567, 393);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBoxFullNameMaster);
            this.Controls.Add(this.buttonDetailConsumption);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.label1);
            this.Name = "Function4";
            this.Text = "Function4";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBoxFullNameMaster;
        private System.Windows.Forms.Button buttonDetailConsumption;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn end_time;
    }
}