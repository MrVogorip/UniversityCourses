namespace HomeWorkDataBase.View
{
    partial class Function1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Function1));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxOperation = new System.Windows.Forms.TextBox();
            this.buttonOperation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 129);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Result});
            this.dataGridView1.Location = new System.Drawing.Point(12, 141);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(398, 67);
            this.dataGridView1.TabIndex = 1;
            // 
            // Result
            // 
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            this.Result.Width = 350;
            // 
            // textBoxOperation
            // 
            this.textBoxOperation.Location = new System.Drawing.Point(483, 39);
            this.textBoxOperation.Name = "textBoxOperation";
            this.textBoxOperation.Size = new System.Drawing.Size(209, 20);
            this.textBoxOperation.TabIndex = 2;
            // 
            // buttonOperation
            // 
            this.buttonOperation.Location = new System.Drawing.Point(483, 82);
            this.buttonOperation.Name = "buttonOperation";
            this.buttonOperation.Size = new System.Drawing.Size(75, 23);
            this.buttonOperation.TabIndex = 3;
            this.buttonOperation.Text = "Execute";
            this.buttonOperation.UseVisualStyleBackColor = true;
            this.buttonOperation.Click += new System.EventHandler(this.buttonOperation_Click);
            // 
            // Function1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(702, 243);
            this.Controls.Add(this.buttonOperation);
            this.Controls.Add(this.textBoxOperation);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "Function1";
            this.Text = "Function1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.TextBox textBoxOperation;
        private System.Windows.Forms.Button buttonOperation;
    }
}