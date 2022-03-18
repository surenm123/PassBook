using System;
using MySql.Data.MySqlClient;
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
    public partial class Ledger : System.Windows.Forms.Form
    {
        public Ledger()
        {
            InitializeComponent();
        }
        config o = new config();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLname.Text = " ";
            txtBank.Text = " ";
            txtBranch.Text = " ";
            txtAccount.Text = " ";
            txtIFSC.Text = " ";
            txtSearch.Text = " ";
            txtLID.Text = "0";
            
        }
        DataTable dtLedger;
        public void loadgrid()
        {
            string sql = "select * from ledger";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, o.con);
            dtLedger = new DataTable();
            da.Fill(dtLedger);
            dataGridView1.DataSource = dtLedger;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                if (string.IsNullOrEmpty(txtLname.Text))
                {
                    errorProvider1.SetError(txtLname, "Please Enter The Ledger Name");
                    txtLname.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    errorProvider1.SetError(txtBank, "Please Enter the Bank Name");
                    txtBank.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAccount.Text))
                {
                    errorProvider1.SetError(txtAccount, "Please Enter Account number");
                    txtAccount.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtIFSC.Text))
                {
                    errorProvider1.SetError(txtIFSC, "Please Enter the IFDC Code ");
                    txtIFSC.Focus();
                    return;
                }
                string lname = MySqlHelper.EscapeString(txtLname.Text);
                string bank = MySqlHelper.EscapeString(txtBank.Text);
                string branch = MySqlHelper.EscapeString(txtBranch.Text);
                string accno = MySqlHelper.EscapeString(txtAccount.Text);
                string ifsc = MySqlHelper.EscapeString(txtIFSC.Text);
                string lid = MySqlHelper.EscapeString(txtLID.Text);
                errorProvider1.Clear();
                string sql;
                if(lid == "0")
                {
                    sql = "insert into ledger (lname,bank, branch, accno,ifsc) " +
                        "values ('" + lname + "','" + bank + "','" + branch + "','" + accno + "','" + ifsc + "')";

                }
                else
                {
                    sql = "update ledger set LNAME= '" + lname + "',BANK ='" + bank + "',BRANCH='" + branch + "',ACCNO='" + accno + "',IFSC='" + ifsc + "' where LID ='"+lid +"'";

                }


                o.con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, o.con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ledger Details have Added Successfully...");
                loadgrid();
                btnClear_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if(o.con.State == ConnectionState.Open)
                {
                    o.con.Close();
                }
            }

        }

        private void Ledger_Load(object sender, EventArgs e)
        {
            loadgrid();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dtLedger);
            dv.RowFilter = "LNAME like '%"+txtSearch.Text +"%' ";
            dataGridView1.DataSource = dv;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                txtLID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtLname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtBank.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtBranch.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtAccount.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtIFSC.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtLID.Text != "0")
            {
                if (MessageBox.Show("Are you sure want to Delete?", "Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {  
                        string lid = MySqlHelper.EscapeString(txtLID.Text);

                        string sql = "delete from ledger where LID = '" + lid + "'";
                        

                        o.con.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, o.con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Ledger Details have Deleted Successfully..!!.");
                        loadgrid();
                        btnClear_Click(sender, e);
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
            }
            else
            {
                MessageBox.Show("Select Rocord To Delete ::");
            }
        }

        private void txtLID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
