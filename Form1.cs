using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ESDnevnik2023A
{
    public partial class Form1 : Form
    {
        DataTable dtOsoba;
        int br_reda;
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void Txt_Load()
        {
            if (br_reda == 0)
            {
                BTN_prev.Enabled = false;
                BTN_first.Enabled = false;
            }
            else {
                BTN_prev.Enabled = true;
                BTN_first.Enabled = true;
            }
            if (br_reda == dtOsoba.Rows.Count -1)
            {
                BTN_next.Enabled = false;
                BTN_last.Enabled = false;
            }
            else
            {
                BTN_next.Enabled = true;
                BTN_last.Enabled = true;
            }
            textBox1.Text = dtOsoba.Rows[br_reda][1].ToString();
            textBox2.Text = dtOsoba.Rows[br_reda][2].ToString();
            textBox3.Text = dtOsoba.Rows[br_reda][3].ToString();
            textBox4.Text = dtOsoba.Rows[br_reda][4].ToString();
            textBox5.Text = dtOsoba.Rows[br_reda][5].ToString();
            textBox6.Text = dtOsoba.Rows[br_reda][6].ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection veza = konekcija.vrati_vezu();
            dtOsoba = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM osoba", veza);
            adapter.Fill(dtOsoba);
            br_reda = 0;
            Txt_Load();
        }

        private void BTN_prev_Click(object sender, EventArgs e)
        {
            br_reda--;
            Txt_Load();
        }

        private void BTN_first_Click(object sender, EventArgs e)
        {
            br_reda = 0;
            Txt_Load();
        }

        private void BTN_next_Click(object sender, EventArgs e)
        {
            br_reda++;
            Txt_Load();
        }

        private void BTN_last_Click(object sender, EventArgs e)
        {
            br_reda=dtOsoba.Rows.Count - 1;
            Txt_Load();
        }
    }
}
