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
    public partial class Upisnica : Form
    {
        DataTable dtOsoba, dtUpisnica, dtOdeljenje;
        int broj_sloga;
        public Upisnica()
        {
            InitializeComponent();
        }

        private void popuni_odeljenje()
        {
            SqlConnection veza = konekcija.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("Select id, STR(razred)+indeks as naziv from odeljenje", veza);
            dtOdeljenje = new DataTable();
            adapter.Fill(dtOdeljenje);
            comboBox1.DataSource = dtOdeljenje;
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "naziv";
        }
        private void popuni_ucenika()
        {
            SqlConnection veza = konekcija.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("Select id, ime+prezime as naziv from osoba", veza);
            dtOsoba = new DataTable();
            adapter.Fill(dtOsoba);
            comboBox2.DataSource = dtOsoba;
            comboBox2.ValueMember = "ID";
            comboBox2.DisplayMember = "naziv";
        }
        private void popuni_grid()
        {
            SqlConnection veza = konekcija.vrati_vezu();
            SqlDataAdapter adapter = new SqlDataAdapter("Select upisnica.id, odeljenje_id, osoba_id, ime+prezime as naziv, STR(razred)+indeks from upisnica JOIN osoba on osoba.id=osoba_id JOIN odeljenje on odeljenje.id=odeljenje_id", veza);
            dtUpisnica = new DataTable();
            adapter.Fill(dtUpisnica);
            dataGridView1.DataSource = dtUpisnica;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // insert into upisnica values(3, 5)
            string naredba = "INSERT INTO upisnica VALUES(";
            naredba = naredba +comboBox2.SelectedValue.ToString();
            naredba = naredba + ", ";
            naredba = naredba + comboBox1.SelectedValue.ToString();
            naredba = naredba + ")";
            SqlConnection veza = konekcija.vrati_vezu();
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            veza.Open();
            ins_naredba.ExecuteNonQuery();
            veza.Close();
            popuni_grid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id_brisi = dataGridView1.Rows[broj_sloga].Cells["id"].Value.ToString();
            SqlConnection veza = konekcija.vrati_vezu();
            string naredba = "DELETE FROM upisnica WHERE id=" + id_brisi;
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            veza.Open();
            ins_naredba.ExecuteNonQuery();
            veza.Close();
            popuni_grid();
        }

        private void Upisnica_Load(object sender, EventArgs e)
        {
            popuni_odeljenje();
            popuni_ucenika();
            popuni_grid();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                broj_sloga = dataGridView1.CurrentRow.Index;
                if (dtUpisnica.Rows.Count != 0 && broj_sloga >=0)
                {
                    comboBox1.SelectedValue = dataGridView1.Rows[broj_sloga].Cells["odeljenje_id"].Value.ToString();
                    comboBox2.SelectedValue = dataGridView1.Rows[broj_sloga].Cells["osoba_id"].Value.ToString();

                }
            }
            
        }
    }
}
