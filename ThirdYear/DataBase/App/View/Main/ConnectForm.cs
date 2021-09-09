using System;
using System.Windows.Forms;
using HomeWorkDataBase.Control;

namespace HomeWorkDataBase
{
    public partial class ConnectForm : Form
    {
        public ConnectForm()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Logon.nameServer = textBoxServer.Text;
                Logon.nameDataBase = textBoxDataBase.Text;
                Logon.nameUser = textBoxName.Text;
                Logon.paswordUser = textBoxPasword.Text;

                if (Logon.nameUser == String.Empty)
                    Logon.DB = new Wrapper.DBWrapper(Logon.nameServer, Logon.nameDataBase);
                else
                    Logon.DB = new Wrapper.DBWrapper(Logon.nameServer, Logon.nameDataBase, Logon.nameUser, Logon.paswordUser);
                if (Logon.DB is null)
                {
                    throw new MyException(2);
                }

                MainForm mainForm = new MainForm(this);
                mainForm.Show();
                Hide();
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
