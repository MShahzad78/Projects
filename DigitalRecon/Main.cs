using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalRecon
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            hideSubMenu();

        }
        private void hideSubMenu()
        {
            panelCoreData.Visible = false;
            panel1LinkData.Visible = false;
            panelMaintenance.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

     
        private Form activeForm = null;

       

       

        private void btnCoreData_Click(object sender, EventArgs e)
        {
           
                showSubMenu(panelCoreData);
            
        }

        private void btnLinkData_Click(object sender, EventArgs e)
        {
            

                showSubMenu(panel1LinkData);

            
        }

        private void btnLoadCoreData_Click(object sender, EventArgs e)
        {
            frmCoreDataLoad myForm = new frmCoreDataLoad();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            myForm.Width = this.Width;

            this.panel1.Controls.Add(myForm);
            myForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frm1LinkDataLoad myForm = new frm1LinkDataLoad();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            myForm.Width = this.Width;

            this.panel1.Controls.Add(myForm);
            myForm.Show();
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    frmCoreDataLoad myForm = new frmCoreDataLoad();
        //    myForm.TopLevel = false;
        //    myForm.AutoScroll = true;
        //    myForm.Width = this.Width;

        //    this.panel1.Controls.Add(myForm);
        //    myForm.Show();
        //}
    }
}
