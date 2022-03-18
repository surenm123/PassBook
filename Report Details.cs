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
    public partial class Report_Details : System.Windows.Forms.Form
    {
        public Report_Details()
        {
            InitializeComponent();
        }
        config o = new config();

        private void btnView_Click(object sender, EventArgs e)
        {
            string fdate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string tdate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string sql = "SELECT l.LNAME as 'LEDGER' , b.TTYPE as 'TYPE', DATE_FORMAT(b.TDATE,'%d-%m-%Y')" +
            " as 'DATE',b.TRANS_TYPE, b.DESCRIPTION, b.TRANS_DETAILS,b.CR,b.DR from bank_transaction b inner join ledger l on l.LID = b.LID where tdate between '" + fdate + "' and '" +tdate  +"' order by b.TDATE; ";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, o.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt; 

        }
    }
}
