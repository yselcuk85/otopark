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

namespace Otopark_Otomasyonu
{
    public partial class frmAraçOtoparkÇıkışı : Form
    {
        public frmAraçOtoparkÇıkışı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OB7PFB2;Initial Catalog=araç_otopark;Integrated Security=True");

        private void frmAraçOtoparkÇıkışı_Load(object sender, EventArgs e)
        {
            DoluYerler();
            Plakalar();
        }

        private void Plakalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araç_otopark_kaydı", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboPlaka.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void DoluYerler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu where durumu='DOLU'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from araç_otopark_kaydı where plaka='"+txtPlaka.Text+"'",baglanti);
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='BOŞ' where parkyeri='" + txtParkYeri2.Text + "'", baglanti);
            komut2.ExecuteNonQuery();


            baglanti.Close();
            MessageBox.Show("Araç Çıkışı Yapıldı");
            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                    txtParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";
                }
            }
            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();
            DoluYerler();
            Plakalar();

        }

        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araç_otopark_kaydı where plaka='"+comboPlaka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri.Text = read["parkyeri"].ToString();
                    }
            baglanti.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araç_otopark_kaydı where parkyeri='" + comboParkYeri.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri2.Text = read["parkyeri"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtPlaka.Text = read["plaka"].ToString();

            }
            baglanti.Close();
        }
    }
}
