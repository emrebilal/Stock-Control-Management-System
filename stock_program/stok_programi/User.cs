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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Ace.Oledb.12.0;Data Source=data.accdb";
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from [user] where username='"+textBox1.Text+"' and password='"+textBox2.Text+"' ";

            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                User u1 = new User();
                if (textBox1.Text == "admin" || textBox1.Text == "ADMİN")
                {
                    u1.Close();
                    Admin a1 = new Admin();
                    a1.Show();
                    this.Hide();
                }
                else
                {
                    u1.Close();
                    Form1 f1 = new Form1();
                    f1.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("User Name or Password is incorrect!","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            conn.Close();
        }
    }
}
