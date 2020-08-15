using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stok_programi
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Text = "LOGIN";
            var pos = this.PointToScreen(label1.Location);
            pos = pictureBox1.PointToClient(pos);
            label1.Parent = pictureBox1;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Login l1 = new Login();
            l1.Close();
            User u1 = new User();
            u1.Show();
            this.Hide();
        }
    }
}
