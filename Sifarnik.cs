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
    public partial class Sifarnik : Form
    {
        DataTable podaci;
        SqlDataAdapter Adapter;
        string tabela;
        public Sifarnik(string naziv)
        {
            tabela = naziv;
        }
        public Sifarnik()
        {
            InitializeComponent();
        }

        private void Sifarnik_Load(object sender, EventArgs e)
        {
            Adapter = new SqlDataAdapter("SELECT * FROM "+tabela, konekcija.vrati_vezu());
            podaci = new DataTable();
            Adapter.Fill(podaci);
            dataGridView1.DataSource = podaci;
            dataGridView1.Columns["id"].ReadOnly = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable menjano = podaci.GetChanges();
            Adapter.UpdateCommand = new SqlCommandBuilder(Adapter).GetUpdateCommand();
            Adapter.Update(menjano);
        }
    }
}
