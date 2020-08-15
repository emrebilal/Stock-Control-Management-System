using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace stok_programi
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public OleDbConnection bag = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=data.accdb");
        public DataTable tablo = new DataTable();
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public OleDbCommand kmt = new OleDbCommand();
        int id;
        public void listele()
        {
            tablo.Clear();
            bag.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select id,username,password From [user]", bag);
            adtr.Fill(tablo);
            dataGridView2.DataSource = tablo;
            adtr.Dispose();
            bag.Close();
            try
            {
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //              
                dataGridView2.Columns[0].HeaderText = "id";               
                dataGridView2.Columns[1].HeaderText = "username";
                dataGridView2.Columns[2].HeaderText = "password";
                //
                dataGridView2.Columns[0].Width = 30;
                dataGridView2.Columns[1].Width = 70;
                dataGridView2.Columns[2].Width = 73;
            }
            catch
            {
                ;
            }

        }

        private void Admin_Load_1(object sender, EventArgs e)
        {           
            this.stokbilTableAdapter.Fill(this.dataDataSet.stokbil);
            listele();
            textBox1.ReadOnly = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            try
            {
                kmt = new OleDbCommand("select * from [user] where [username]='" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "'", bag);
                bag.Open();
                OleDbDataReader oku = kmt.ExecuteReader();
                oku.Read();
                if (oku.HasRows)
                {
                    id = Convert.ToInt32(oku[0].ToString());
                }
                bag.Close();
            }
            catch
            {
                bag.Close();
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text.Trim() == "") errorProvider1.SetError(textBox2, "Required field!");
                else errorProvider1.SetError(textBox2, "");
                if (textBox3.Text.Trim() == "") errorProvider1.SetError(textBox3, "Required field!");
                else errorProvider1.SetError(textBox3, "");

                if (textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "")
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "INSERT INTO [user]([username],[password]) VALUES ('" + textBox2.Text + "','" + textBox3.Text + "')";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    listele();
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                    MessageBox.Show("User added.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch
            {
                MessageBox.Show("Registered user!");
                bag.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Are you sure you want to delete the user?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes && dataGridView2.CurrentRow.Cells[0].Value.ToString().Trim() != "")
                {
                    bag.Open();
                    kmt.Connection = bag;
                    kmt.CommandText = "DELETE from [user] WHERE username='" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "' ";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    bag.Close();
                    listele();
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                }
            }
            catch
            {
                ;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
            if (textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "")
            {
                string sorgu = "UPDATE [user] SET [username]='" + textBox2.Text + "',[password]='" + textBox3.Text + "' WHERE [id]=" + id;
                OleDbCommand kmt = new OleDbCommand(sorgu, bag);
                bag.Open();
                kmt.ExecuteNonQuery();
                kmt.Dispose();
                bag.Close();
                listele();
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
                MessageBox.Show("The update has been completed successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Fill in all fields!");
            }
            }
            catch(Exception h)
            {
                MessageBox.Show(h.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            linkLabel1.LinkVisited = true;
        }
    }
}
