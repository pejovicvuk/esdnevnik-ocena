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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("unesite email i lozinku");
                return;
            }
            else
            {
                SqlConnection veza = konekcija.vrati_vezu();
                SqlCommand naredba = new SqlCommand("SELECT * FROM osoba WHERE email=@username", veza);
                naredba.Parameters.AddWithValue("@username", textBox1.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(naredba);
                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                int redovi = tabela.Rows.Count;
                if (redovi == 1)
                {
                    if (String.Compare(tabela.Rows[0]["pass"].ToString(),textBox2.Text) == 0)
                    {
                        MessageBox.Show("Login Successful!");
                        Program.user_prava = (int) tabela.Rows[0]["uloga"];
                        this.Hide();
                        Glavna forma = new Glavna();
                        forma.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Pogresna lozinka");
                    }
                }
                else
                {
                    MessageBox.Show("Nepostojeci email");
                }
            }
        }
    }
}
