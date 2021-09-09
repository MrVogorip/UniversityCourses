namespace HomeWorkDataBase.View
{
    partial class Procedure2
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
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.textBoxNameOperation = new System.Windows.Forms.TextBox();
            this.textBoxNameDetail = new System.Windows.Forms.TextBox();
            this.buttonNewOperation = new System.Windows.Forms.Button();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(318, 40);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(200, 20);
            this.textBoxCost.TabIndex = 46;
            // 
            // textBoxNameOperation
            // 
            this.textBoxNameOperation.Location = new System.Drawing.Point(318, 68);
            this.textBoxNameOperation.Name = "textBoxNameOperation";
            this.textBoxNameOperation.Size = new System.Drawing.Size(200, 20);
            this.textBoxNameOperation.TabIndex = 44;
            // 
            // textBoxNameDetail
            // 
            this.textBoxNameDetail.Location = new System.Drawing.Point(318, 11);
            this.textBoxNameDetail.Name = "textBoxNameDetail";
            this.textBoxNameDetail.Size = new System.Drawing.Size(200, 20);
            this.textBoxNameDetail.TabIndex = 42;
            // 
            // buttonNewOperation
            // 
            this.buttonNewOperation.Location = new System.Drawing.Point(318, 132);
            this.buttonNewOperation.Name = "buttonNewOperation";
            this.buttonNewOperation.Size = new System.Drawing.Size(75, 23);
            this.buttonNewOperation.TabIndex = 39;
            this.buttonNewOperation.Text = "Execute";
            this.buttonNewOperation.UseVisualStyleBackColor = true;
            this.buttonNewOperation.Click += new System.EventHandler(this.buttonNewOperation_Click);
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CustomFormat = "hh:mm:ss";
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(318, 96);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerDate.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 75);
            this.label1.TabIndex = 37;
            this.label1.Text = "• Expand the list of repair operations performed.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Procedure2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(546, 169);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.textBoxNameOperation);
            this.Controls.Add(this.textBoxNameDetail);
            this.Controls.Add(this.buttonNewOperation);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.label1);
            this.Name = "Procedure2";
            this.Text = "Procedure2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCost;
        private System.Windows.Forms.TextBox textBoxNameOperation;
        private System.Windows.Forms.TextBox textBoxNameDetail;
        private System.Windows.Forms.Button buttonNewOperation;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label label1;
    }
}