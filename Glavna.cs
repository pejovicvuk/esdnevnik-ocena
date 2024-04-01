using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESDnevnik2023A
{
    public partial class Glavna : Form
    {
        public Glavna()
        {
            InitializeComponent();
        }

        private void osobaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void osobaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form1 osoba = new Form1();
            osoba.Show();
        }

        private void predmetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik predmet = new Sifarnik("predmet");
            predmet.ShowDialog();
        }

        private void raspodelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            raspodela ras = new raspodela();
            ras.ShowDialog();
        }

        private void skGodinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sifarnik predmet = new Sifarnik("skolska_godina");
            predmet.ShowDialog();
        }

        private void Glavna_Load(object sender, EventArgs e)
        {
            if (Program.user_prava < 2)
            {
                // moze horizontalni meni, samo onda nema broj
                osobaToolStripMenuItem1.Enabled = false;
            }
            
        }

        private void Glavna_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void upisnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Upisnica forma = new Upisnica();
            forma.ShowDialog();
        }

        private void ocenaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ocena ocena = new ocena();
            ocena.Show();
        }
    }
}
