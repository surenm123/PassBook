using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PassBook
{
    public partial class changePassword : System.Windows.Forms.Form
    {
        public changePassword()
        {
            InitializeComponent();
        }
        config o = new config();

        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCurrentPass.Text))
                {
                    errorProvider1.SetError(txtCurrentPass, " Enter Current Password.. ");
                    txtCurrentPass.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtNewPass.Text))
                {
                    errorProvider1.SetError(txtNewPass, "Enter New Password ...");
                    txtNewPass.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtConfirmPass.Text))
                {
                    errorProvider1.SetError(txtConfirmPass, "Enter Confirm Password ..");
                    txtConfirmPass.Focus();
                    return;
                }
                string currentPass = MySqlHelper.EscapeString(txtCurrentPass.Text);
                string newPass = MySqlHelper.EscapeString(txtNewPass.Text);
                string confirmPass = MySqlHelper.EscapeString(txtConfirmPass.Text);
                errorProvider1.Clear();
                string sql = " select * from admin_logs where upass = '" + currentPass + "' and ID = 1 ";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, o.con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    if (newPass == confirmPass)
                    {
                        o.con.Open();
                        sql = " update admin_logs set upass = '" + newPass + "'    where ID =1 ";
                        MySqlCommand cmd = new MySqlCommand(sql, o.con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" Password Updated Successfully ... ");

                    }
                    else
                    {
                        MessageBox.Show(" Missmatch Password ..");
                    }
                }
                else
                {
                    MessageBox.Show(" Invalid Password!!!! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                if (o.con.State == ConnectionState.Open)
                {
                    o.con.Close();
                }

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCurrentPass.Text = " ";
            txtNewPass.Text = " ";
            txtConfirmPass.Text = " ";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
