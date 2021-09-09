using System;
using System.Windows.Forms;
using HomeWorkDataBase.View;
using HomeWorkDataBase.Control;

namespace HomeWorkDataBase
{

    public partial class MainForm : Form
    {
        ConnectForm connectForm;
        Request request;

        public MainForm(ConnectForm connectForm)
        {
            InitializeComponent();
            this.connectForm = connectForm;
            request = (nameTable, column) => "SELECT " + column + " FROM " + nameTable;
            FormClosing += delegate { connectForm.Close(); };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.supplyTableAdapter.Fill(this.carServiceDataSet.Supply);
            this.stockTableAdapter.Fill(this.carServiceDataSet.Stock);
            this.operationsTableAdapter.Fill(this.carServiceDataSet.Operations);
            this.mastersTableAdapter.Fill(this.carServiceDataSet.Masters);
            this.detailTableAdapter.Fill(this.carServiceDataSet.Detail);
            this.archiveCompletedWorkTableAdapter.Fill(this.carServiceDataSet.ArchiveCompletedWork);
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {
            archiveCompletedWorkDataGridView.DataSource = Logon.DB.FillDataSet(request("ArchiveCompletedWork", "*"));
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            detailDataGridView.DataSource = Logon.DB.FillDataSet(request("Detail", "*"));
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            mastersDataGridView.DataSource = Logon.DB.FillDataSet(request("Masters", "*"));
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            operationsDataGridView.DataSource = Logon.DB.FillDataSet(request("Operations", "*"));
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            stockDataGridView.DataSource = Logon.DB.FillDataSet(request("Stock", "*"));
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {
            supplyDataGridView.DataSource = Logon.DB.FillDataSet(request("Supply", "*"));
        }


        private void function1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function1 function1 = new Function1();
            function1.Show();
        }

        private void function2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function2 function2 = new Function2();
            function2.Show();
        }

        private void function3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function3 function3 = new Function3();
            function3.Show();
        }

        private void function4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function4 function4 = new Function4();
            function4.Show();
        }

        private void function5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function5 function5 = new Function5();
            function5.Show();
        }

        private void procedure1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Procedure1 procedure1 = new Procedure1();
            procedure1.Show();
        }

        private void procedure2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Procedure2 procedure2 = new Procedure2();
            procedure2.Show();
        }
    }
}
