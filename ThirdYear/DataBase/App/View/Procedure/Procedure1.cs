using System;
using System.Windows.Forms;
using HomeWorkDataBase.Control;

namespace HomeWorkDataBase.View
{
    public partial class Procedure1 : Form
    {
        public Procedure1()
        {
            InitializeComponent();
        }
        private void buttonNewCompletedWork_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = textBoxFullNameMaster.Text;
                string nameOperation = textBoxNameOperation.Text;
                string date = dateTimePickerDate.Text;
                int cost = 0;
                if(!int.TryParse(textBoxCost.Text,out cost))
                {
                    throw new MyException(1);
                }
                Logon.DB.Procedure("NewCompletedWork", fullName, nameOperation, cost, date);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
