using System;
using System.Windows.Forms;
using HomeWorkDataBase.Control;

namespace HomeWorkDataBase.View
{
    public partial class Procedure2 : Form
    {
        public Procedure2()
        {
            InitializeComponent();
        }

        private void buttonNewOperation_Click(object sender, EventArgs e)
        {
            try
            {
                string nameDetail = textBoxNameDetail.Text;
                string nameOperation = textBoxNameOperation.Text;
                string date = dateTimePickerDate.Text;
                int cost = 0;
                if (!int.TryParse(textBoxCost.Text, out cost))
                {
                    throw new MyException(1);
                }
                Logon.DB.Procedure("NewOperation", nameDetail, nameOperation, cost, date);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
