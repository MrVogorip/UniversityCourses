namespace HomeWorkDataBase.View
{
    partial class Function3
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
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDetailPool = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name_detail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(345, 45);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(159, 20);
            this.dateTimePickerEnd.TabIndex = 25;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(345, 12);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(159, 20);
            this.dateTimePickerStart.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 110);
            this.label1.TabIndex = 23;
            this.label1.Text = "3. Compile a list of parts consumption for a specified period of time, find those" +
    " parts that were not used at all during this period of time;";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonDetailPool
            // 
            this.buttonDetailPool.Location = new System.Drawing.Point(297, 81);
            this.buttonDetailPool.Name = "buttonDetailPool";
            this.buttonDetailPool.Size = new System.Drawing.Size(75, 23);
            this.buttonDetailPool.TabIndex = 28;
            this.buttonDetailPool.Text = "Execute";
            this.buttonDetailPool.UseVisualStyleBackColor = true;
            this.buttonDetailPool.Click += new System.EventHandler(this.buttonDetailPool_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name_detail,
            this.Cost});
            this.dataGridView1.Location = new System.Drawing.Point(12, 133);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(306, 303);
            this.dataGridView1.TabIndex = 29;
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
            // Function3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(534, 448);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonDetailPool);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.label1);
            this.Name = "Function3";
            this.Text = "Function3";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDetailPool;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
    }
}