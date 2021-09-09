using System;
using System.Windows.Forms;
using HomeWorkDataBase.Control;

namespace HomeWorkDataBase.View
{
    public partial class Function5 : Form
    {
        public Function5()
        {
            InitializeComponent();
        }

        private void buttonTopMaster_Click(object sender, EventArgs e)
        {
            try
            {
                string startDate = dateTimePickerStart.Text;
                string endDate = dateTimePickerEnd.Text;
                var result = Logon.DB.ExecuteQuery($"SELECT dbo.TopMaster('{startDate}','{endDate}') AS result");
                if (result.Count == 0)
                {
                    throw new MyException(3);
                }
                dataGridView1.Rows.Clear();
                dataGridView1.Visible = true;
                var rowNumber = 0;
                foreach (var row in result)
                {
                    dataGridView1.Rows.Add();
                    for (int i = 0; i < row.Count; i++)
                    {
                        dataGridView1[i, rowNumber].Value = row[i];
                    }
                    rowNumber++;
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
