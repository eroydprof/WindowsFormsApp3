using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp3
{
    public partial class FrmAdmin : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public FrmAdmin()
        {
            alamat = "server=localhost; database=db_vispro24; username=root; password=;";
            koneksi = new MySqlConnection(alamat);

            InitializeComponent();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAdmin_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtIDPengguna.Text != "")
                {
                    query = string.Format("select * from tbl_pengguna where id_pengguna = '{0}'", TxtIDPengguna.Text);
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            TxtUsername.Text = kolom["username"].ToString();
                            TxtPassword.Text = kolom["password"].ToString();
                            TxtNamaPengguna.Text = kolom["nama_pengguna"].ToString();
                            if (kolom["level"].ToString() == "1")
                            {
                                CBLevel.Text = "Administrator";
                            }
                            else
                            {
                                CBLevel.Text = "Pengguna";
                            }
                            

                            CBLevel.Enabled = true;
                            
                            BtnSave.Enabled = false;
                            BtnDelete.Enabled = true;
                            BtnUpdate.Enabled = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ID Pengguna masih kosong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("DELETE FROM `tbl_pengguna` where id_pengguna = '{0}'", TxtIDPengguna.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CBLevel.Text == "Administrator")
                {
                    CBLevel.Text = "1";
                }
                else
                {
                    CBLevel.Text = "2";
                }
                query = string.Format("UPDATE `tbl_pengguna` SET `username`='{0}',`password`='{1}',`nama_pengguna`='{2}',`level`='{3}' where id_pengguna = '{4}'", TxtUsername.Text, TxtPassword.Text, TxtNamaPengguna.Text, CBLevel.Text, TxtIDPengguna.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();

                FrmAdmin_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CBLevel.Text == "Administrator")
                {
                    CBLevel.Text = "1";
                }
                else
                {
                    CBLevel.Text = "2";
                }
                query = string.Format("insert into `tbl_pengguna` (`username`, `password`, `nama_pengguna`, `level`) VALUES ('{0}','{1}', '{2}', '{3}')", TxtUsername.Text, TxtPassword.Text, TxtNamaPengguna.Text, CBLevel.Text);

                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                int res = perintah.ExecuteNonQuery();

                koneksi.Close();
                if (res == 1)
                {
                    MessageBox.Show("Insert data success");
                    FrmAdmin_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Insert data Error");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                query = string.Format("select * from tbl_pengguna");
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                ds.Clear();
                adapter.Fill(ds);
                koneksi.Close();

                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Width = 30;
                dataGridView1.Columns[0].HeaderText = "No";
                dataGridView1.Columns[1].Width = 70;
                dataGridView1.Columns[1].HeaderText = "Username";
                dataGridView1.Columns[2].Width = 70;
                dataGridView1.Columns[2].HeaderText = "Password";
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[3].HeaderText = "Nama Pengguna";
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[4].HeaderText = "Level";

                TxtIDPengguna.Clear();
                TxtUsername.Clear();
                TxtPassword.Clear();


                TxtIDPengguna.Focus();

                BtnUpdate.Enabled = false;
                BtnDelete.Enabled = false;
                BtnRead.Enabled = false;
                BtnSave.Enabled = true;
                BtnRead.Enabled = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
