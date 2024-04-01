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

namespace ESDnevnik2023A {
    public partial class ocena : Form {
        DataTable dtGrid;
        public ocena() {
            InitializeComponent();
        }

        private void ocena_Load(object sender, EventArgs e) {
            godinaPopuni();
            profesorPopuni();
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            comboBox6.Items.Add(1);
            comboBox6.Items.Add(2);
            comboBox6.Items.Add(3);
            comboBox6.Items.Add(4);
            comboBox6.Items.Add(5);
        }

        private void godinaPopuni() {
            SqlConnection conn = konekcija.vrati_vezu();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("select * from skolska_godina", conn));
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "naziv";
        }

        private void profesorPopuni() {
            SqlConnection conn = konekcija.vrati_vezu();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("select osoba.id as id, ime + ' ' + prezime as naziv from osoba " +
                "join raspodela on osoba.id = nastavnik_id " +
                "where godina_id = " + comboBox1.SelectedValue.ToString(), conn));
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "naziv";
            comboBox2.SelectedIndex = -1;
        }

        private void predmetPopuni() {
            SqlConnection conn = konekcija.vrati_vezu();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("select distinct predmet.id as id, naziv from predmet " +
                "join raspodela on predmet.id = predmet_id where godina_id = " + comboBox1.SelectedValue.ToString() + " and nastavnik_id = " +
                comboBox2.SelectedValue.ToString(), conn));
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox3.DataSource = dt;
            comboBox3.ValueMember = "id";
            comboBox3.DisplayMember = "naziv";
            comboBox3.SelectedIndex = -1;
        }

        private void odeljenjePopuni() {
            SqlConnection conn = konekcija.vrati_vezu();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("select distinct odeljenje.id as id, str(razred) + '/' + indeks as naziv from odeljenje " +
                "join raspodela on odeljenje.id = odeljenje_id where raspodela.godina_id = " + comboBox1.SelectedValue.ToString() + " and nastavnik_id = " +
                comboBox2.SelectedValue.ToString() + " and predmet_id = " +
                comboBox3.SelectedValue.ToString(), conn));
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox4.DataSource = dt;
            comboBox4.ValueMember = "id";
            comboBox4.DisplayMember = "naziv";
            comboBox4.SelectedIndex = -1;
        }

        private void ucenikPopuni() {
            SqlConnection conn = konekcija.vrati_vezu();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("select osoba.id as id, ime + ' ' + prezime as naziv from osoba " +
                "join upisnica on osoba.id = osoba_id " +
                "where upisnica.odeljenje_id = " + comboBox4.SelectedValue.ToString() , conn));
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            comboBox5.DataSource = dt;
            comboBox5.ValueMember = "id";
            comboBox5.DisplayMember = "naziv";
            comboBox5.SelectedIndex = -1;
        }

        private void gridPopuni() {
            SqlConnection conn = konekcija.vrati_vezu();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("select ocena.id as id, ime + ' ' + prezime as naziv, ocena, ucenik_id, datum from osoba " +
                "join ocena on ucenik_id = osoba.id " +
                "join raspodela on raspodela_id = raspodela.id " +
                "where raspodela_id = " +
                "(select id from raspodela " +
                "where godina_id = " + comboBox1.SelectedValue + " " +
                "and nastavnik_id = " + comboBox2.SelectedValue + " " +
                "and predmet_id = " + comboBox3.SelectedValue + " " +
                "and odeljenje_id = " + comboBox4.SelectedValue + ")" +
                "and uloga = 1", conn));
            dtGrid = new DataTable();
            sqlDataAdapter.Fill(dtGrid);
            dataGridView1.DataSource = dtGrid;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns["ucenik_id"].Visible = false;
        }

        private void disableenable( char c ) {
            foreach (ComboBox cb in this.Controls.OfType<ComboBox>()) {
                    if (cb.Name.Last() <= c)
                        cb.Enabled = true;
                    else { cb.Enabled = false; cb.SelectedIndex = -1; }
                }
            dtGrid = new DataTable();
            dataGridView1.DataSource = dtGrid;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if ( comboBox1.IsHandleCreated && comboBox1.Focused ) {
                profesorPopuni();
                disableenable('2');
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox2.IsHandleCreated && comboBox2.Focused) {
                predmetPopuni();
                disableenable('3');
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox3.IsHandleCreated && comboBox3.Focused) {
                odeljenjePopuni();
                disableenable('4');
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox4.IsHandleCreated && comboBox4.Focused) {
                disableenable('5');
                ucenikPopuni();
                gridPopuni();
                try {
                    idkVise(0);
                } catch {
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) {
                idkVise(e.RowIndex);
            }
        }

        private void idkVise( int i) {
            comboBox5.SelectedValue = dtGrid.Rows[i]["ucenik_id"];
            comboBox6.SelectedItem = dtGrid.Rows[i]["ocena"];
            textBox1.Text = dtGrid.Rows[i]["id"].ToString();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox5.IsHandleCreated) {
                comboBox6.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e){
            string naredba = "select id from raspodela " +
                "where godina_id = " + comboBox1.SelectedValue + " " +
                "and nastavnik_id = " + comboBox2.SelectedValue + " " +
                "and predmet_id = " + comboBox3.SelectedValue + " " +
                "and odeljenje_id = " + comboBox4.SelectedValue;
            SqlConnection conn = konekcija.vrati_vezu();
            SqlCommand cmd = new SqlCommand(naredba, conn);
            int idRaspodele = 0;
            try {
                conn.Open();
                idRaspodele = (int)cmd.ExecuteScalar();
                conn.Close();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            if ( idRaspodele > 0 ) {
                DateTime date = dateTimePicker1.Value;
                naredba = "insert into ocena (datum, raspodela_id, ucenik_id, ocena) values('" +
                    date.ToString("yyyy-MM-dd") + "', '" +
                    idRaspodele + "', '" +
                    comboBox5.SelectedValue.ToString() + "', '" +
                    comboBox6.SelectedItem.ToString() + "')";
                cmd = new SqlCommand(naredba, conn);

                try {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                gridPopuni();
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if ( int.Parse(textBox1.Text) > 0 ) {
                DateTime dateTime = dateTimePicker1.Value;
                string naredba = "update ocena set " +
                    "ucenik_id = '" + comboBox5.SelectedValue.ToString() + "', " +
                    "ocena = '" + comboBox6.SelectedItem.ToString() + "', " +
                    "datum = '" + dateTime.ToString("yyyy-MM-dd") + "' " +
                    "where id = " + textBox1.Text;
                SqlConnection conn = konekcija.vrati_vezu();
                SqlCommand cmd = new SqlCommand(naredba, conn);
                try {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                gridPopuni();
            }  
        }

        private void button3_Click(object sender, EventArgs e) {
            if ( int.Parse(textBox1.Text) > 0 ) {
                string naredba = "delete from ocena where id = " + textBox1.Text;
                SqlConnection conn = konekcija.vrati_vezu();
                SqlCommand cmd = new SqlCommand(naredba, conn);
                try {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                gridPopuni();
                idkVise(0);
            }
        }
    }
}
