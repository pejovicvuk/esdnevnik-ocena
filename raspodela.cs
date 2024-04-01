using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESDnevnik2023A
{
    public partial class raspodela : Form
    {
        DataTable dtRaspodela, dtOsoba, dtOdeljenje, dtSk_god, dtPredmet;

        private void button6_Click(object sender, EventArgs e)
        {
            br_reda++;
            ComboLoad();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        int br_reda;
        public raspodela()
        {
            InitializeComponent();
        }
        private void ComboLoad()
        {
            comboBox1.SelectedValue = dtRaspodela.Rows[br_reda]["nastavnik_id"].ToString();
            comboBox2.SelectedValue = dtRaspodela.Rows[br_reda]["predmet_id"].ToString();
            comboBox4.SelectedValue = dtRaspodela.Rows[br_reda]["odeljenje_id"].ToString();
        }
        private void raspodela_Load(object sender, EventArgs e)
        {
            SqlConnection veza = konekcija.vrati_vezu();
            dtRaspodela = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM raspodela", veza);
            adapter.Fill(dtRaspodela);
            br_reda = 0;

            dtOsoba = new DataTable();
            adapter = new SqlDataAdapter("SELECT id, ime+prezime as ucenik FROM osoba", veza);
            adapter.Fill(dtOsoba);
            comboBox1.DataSource = dtOsoba;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "ucenik";
            
            
            dtPredmet=new DataTable();
            adapter = new SqlDataAdapter("Select * from predmet", veza);
            adapter.Fill(dtPredmet);
            comboBox2.DataSource = dtPredmet;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "naziv";
            

            dtOdeljenje =new DataTable();
            adapter = new SqlDataAdapter("Select id, STR(Odeljenje.razred)+'-'+Odeljenje.indeks AS naziv from Odeljenje", veza);
            adapter.Fill (dtOdeljenje);
            comboBox4.DataSource = dtOdeljenje;
            comboBox4.ValueMember = "id";
            comboBox4.DisplayMember = "naziv";
            ComboLoad();
        }
    }
}
