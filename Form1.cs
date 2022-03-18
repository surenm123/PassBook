using MySql.Data.MySqlClient;
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
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }
        config o = new config();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want To Exit Now ?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Application.Exit();
            }

        }
        

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "Please Enter The User Name");
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Please Enter The Password");
                txtUserName.Focus();
                return;
            }
            string uname = MySqlHelper.EscapeString(txtUserName.Text);
            string upass = MySqlHelper.EscapeString(txtPassword.Text);

            string sql = "select * from admin_logs where UNAME='" + uname + "' and UPASS='" + upass + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, o.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count == 1)
            {
                this.Hide();
                Home frm = new Home(dt.Rows[0][1].ToString());
                frm.Show();
            }
            else
            {
                 
                {
                    MessageBox.Show("Invalid Username and Password", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    txtUserName.Focus();
                }

            }


        }

       
    }
}
