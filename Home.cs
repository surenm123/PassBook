using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassBook
{
    public partial class Home : System.Windows.Forms.Form
    {
        public Home(string name)
        {
            InitializeComponent();
            lblWelcome.Text = "Welcome " + name;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void Home_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Form f in Application.OpenForms)
            {
                if(f.GetType() == typeof (Ledger))
                {
                    f.Activate();
                    return;
                }
            }
            Ledger frm = new Ledger();
            frm.MdiParent = this;
            frm.Show();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(System.Windows.Forms.Form f in Application.OpenForms)
            {
                if(f.GetType() == typeof (Transaction))
                {
                    f.Activate();
                    return;
                }
            }
            Transaction frm = new Transaction();
            frm.MdiParent = this;
            frm.Show();
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (System.Windows.Forms.Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(changePassword))
                {
                    f.Activate();
                    return;
                }
            }
            changePassword frm = new changePassword();
            frm.MdiParent = this;
            frm.Show();
        }
        private void reportDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Form f in Application.OpenForms)
            {
                if (f.GetType() == typeof(Report_Details))
                {
                    f.Activate();
                    return;
                }
            }
            Report_Details frm = new Report_Details();
            frm.MdiParent = this;
            frm.Show();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form[] Chid = this.MdiChildren;
            foreach(System.Windows.Forms.Form f in Chid)
            {
                f.Close();
            }
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    } 
}
