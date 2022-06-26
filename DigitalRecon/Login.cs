using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalRecon.DAL;
namespace DigitalRecon
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            DBComponent db = new DBComponent();
            db.CreateConnection();
            Main mn = new Main();
            mn.Show();
            this.Hide();
        }
    }
}
