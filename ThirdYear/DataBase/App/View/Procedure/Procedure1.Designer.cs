namespace HomeWorkDataBase.View
{
    partial class Procedure1
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
            this.textBoxFullNameMaster = new System.Windows.Forms.TextBox();
            this.buttonNewCompletedWork = new System.Windows.Forms.Button();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNameOperation = new System.Windows.Forms.TextBox();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxFullNameMaster
            // 
            this.textBoxFullNameMaster.Location = new System.Drawing.Point(305, 24);
            this.textBoxFullNameMaster.Name = "textBoxFullNameMaster";
            this.textBoxFullNameMaster.Size = new System.Drawing.Size(200, 20);
            this.textBoxFullNameMaster.TabIndex = 32;
            // 
            // buttonNewCompletedWork
            // 
            this.buttonNewCompletedWork.Location = new System.Drawing.Point(305, 145);
            this.buttonNewCompletedWork.Name = "buttonNewCompletedWork";
            this.buttonNewCompletedWork.Size = new System.Drawing.Size(75, 23);
            this.buttonNewCompletedWork.TabIndex = 28;
            this.buttonNewCompletedWork.Text = "Execute";
            this.buttonNewCompletedWork.UseVisualStyleBackColor = true;
            this.buttonNewCompletedWork.Click += new System.EventHandler(this.buttonNewCompletedWork_Click);
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(305, 109);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerDate.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 56);
            this.label1.TabIndex = 25;
            this.label1.Text = "• Enter information about new repairs.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxNameOperation
            // 
            this.textBoxNameOperation.Location = new System.Drawing.Point(305, 54);
            this.textBoxNameOperation.Name = "textBoxNameOperation";
            this.textBoxNameOperation.Size = new System.Drawing.Size(200, 20);
            this.textBoxNameOperation.TabIndex = 34;
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(305, 80);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(200, 20);
            this.textBoxCost.TabIndex = 36;
            // 
            // Procedure1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(519, 201);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.textBoxNameOperation);
            this.Controls.Add(this.textBoxFullNameMaster);
            this.Controls.Add(this.buttonNewCompletedWork);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.label1);
            this.Name = "Procedure1";
            this.Text = "Procedure1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFullNameMaster;
        private System.Windows.Forms.Button buttonNewCompletedWork;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNameOperation;
        private System.Windows.Forms.TextBox textBoxCost;
    }
}