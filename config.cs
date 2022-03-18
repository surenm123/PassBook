using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PassBook
{
    class config
    {
       // private object database;
        public MySqlConnection con;
        public config()
        {
            try
            {
                string str = "server=localhost;User Id= root;pwd= Maraga@84; database = PASSBOOK; Convert Zero Datetime=True "; 
                con = new MySqlConnection(str);
               // MessageBox.Show("Good");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }
    }
}
