using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PassBook
{
    public partial class Transaction : System.Windows.Forms.Form
    {
        public Transaction()
        {
            InitializeComponent();
        }
        config o = new config();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void datePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Transaction_Load(object sender, EventArgs e)
        {
            string sql = "SELECT LID,LNAME from ledger";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, o.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboLID.DataSource = dt;
            comboLID.DisplayMember = "LNAME";
            comboLID.ValueMember = "LID";
            comboLID.SelectedIndex = -1;

            loadgrid();
        }
        DataTable dtTrans;
        public void loadgrid()
        {
            string sql = "SELECT b.TID,l.LNAME,b.TTYPE,b.TDATE as DATE, b.TRANS_TYPE,b.DESCRIPTION,b.TRANS_DETAILS,b.CR,b.DR from bank_transaction b inner join ledger l on l.LID=b.LID order by TDATE desc;";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, o.con);
            dtTrans = new DataTable();
            da.Fill(dtTrans);
            dataGridView1.DataSource = dtTrans;
            

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            comboTType.SelectedIndex = -1;
            comboLID.SelectedIndex = -1;
            comboTrans_Type.SelectedIndex = -1;
            txtTID.Text = "0";
            txtAmount.Text = " ";
            txtDes.Text = " ";
            txtTDetails.Text = " ";
            txtSearch.Text = " ";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboTType.SelectedIndex==-1)
                {
                    errorProvider1.SetError(comboTType, "Please Select Account Type");
                    comboTType.Focus();
                    return;
                }
                if(comboLID.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboLID, "Please Select ledger");
                    comboLID.Focus();
                    return;
                }
                if(comboTrans_Type.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboTrans_Type, "Please Enter Transaction Type");
                    comboTrans_Type.Focus();
                    return;
                }
                if(string.IsNullOrEmpty(txtAmount.Text))
                {
                    errorProvider1.SetError(txtAmount, "Enter the Amount");
                    txtAmount.Focus();
                    return;
                }
                errorProvider1.Clear();
                string ttype = MySqlHelper.EscapeString(comboTType.Text);
                string tdate = datePicker1.Value.ToString("yyyy-MM-dd");
                String lid = MySqlHelper.EscapeString(comboLID.SelectedValue.ToString());             
                string trans_type = MySqlHelper.EscapeString(comboTrans_Type.Text);
                string description = MySqlHelper.EscapeString(txtDes.Text);
                string trans_Details = MySqlHelper.EscapeString(txtTDetails.Text);
                string amt = MySqlHelper.EscapeString(txtAmount.Text);
                string tid = MySqlHelper.EscapeString(txtTID.Text);
                string cr = "0";
                string dr = "1";

                if(ttype == "CREDIT")
                {
                    cr = amt;
                }
                else
                {
                    dr = amt;
                }
                string sql;
                if(tid == "0")
                {
                    sql = "insert into bank_transaction (TTYPE,TDATE,LID,TRANS_TYPE,DESCRIPTION,TRANS_DETAILS,CR,DR) " +
                        "values ( '" + ttype + "', '" + tdate + "','" + lid + "','" + trans_type + "','" + description + "','" + trans_Details + "','" + cr + "','" + dr + "' )";
                }
                else
                {
                    sql = "Update bank_transaction set TTYPE = '" + ttype + "',TDATE = '" + tdate + "', LID = '" + lid + "'," +
                        "TRANS_TYPE = '" + trans_type + "', DESCRIPTION = '" + description + "', TRANS_DETAILS = '" + trans_Details + "', CR = '" + cr + "', DR = '" + dr + "'  where TID = '" + tid + "'  ";
                }
                o.con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, o.con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Transaction Details Updated Successfully...!!!");
                loadgrid();
                btnClear_Click(sender, e);

            }
            catch( Exception ex)
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
        public void transaction_load (object sender, EventArgs e)
        {
            loadgrid();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtTID.Text != "0")
            {
                if (MessageBox.Show("Are you sure want to Delete ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        string tid = MySqlHelper.EscapeString(txtTID.Text);
                        string sql = "delete from bank_transaction where TID = '" + tid + "'";
                        o.con.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, o.con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(" Transaction Details Deleted Successfully .!!");
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

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(dtTrans);
            dv.RowFilter = "LNAME like '%" + txtSearch.Text + "%' or  TRANS_DETAILS like '%" + txtSearch.Text + "%'  ";
            dataGridView1.DataSource = dv;
        }

       

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                txtTID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboLID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboTType.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                datePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboTrans_Type.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtDes.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtTDetails.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "0")
                {
                    txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                }
                else
                {
                    txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                }

            }
        }
    }
}
