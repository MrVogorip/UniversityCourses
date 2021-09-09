using System;
using System.Windows.Forms;

namespace Puzzle15
{
    public partial class Form1 : Form
    {
        Interface g;
        bool a = false;
        public Form1()
        {
            InitializeComponent();
            g = new Game(4);
        }

        private void button0_Click(object sender, EventArgs e)
        {
            int pos = Convert.ToInt16(((Button)sender).Tag);
            g.shift(pos);
            if (g.check())
            {
                MessageBox.Show("Victory!", "Hooray");
            }
            refe();
            if (a == false)
            {
                g.count = 1;
                a = true;
            }
            toolStripStatusLabel2.Text = g.count.ToString();
        }
        private Button but (int pos)
        {
            switch (pos)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;
            }
        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            st();
            g.count = 1;
        }
        private void refe()
        {
            for (int pos = 0; pos < 16; ++pos)
            {
                but(pos).Text = g.get_num(pos).ToString();
                but(pos).Visible = (g.get_num(pos) > 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            st();
        }
        private void st()
        {
        
            g.start();

            for (int i = 0; i < 100; ++i)
            {
                g.ran();
            }
            refe();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var mes = MessageBox.Show("Do you want to go out?", "Exit", MessageBoxButtons.YesNo);
            if (mes == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
