using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Library
{
    public partial class allreader : Form
    {
        public allreader()
        {
            InitializeComponent();
        }
        string connString = @"Data Source=.;Initial Catalog=Library;User ID =sa;Pwd=123456";
        SqlConnection conn;

        private void allreader_Load_1(object sender, EventArgs e)
        {
            conn = new SqlConnection(connString);
            string sql = "select a.Rname as 姓名,b.Rcategoryname as 身份,a.Phone as 电话,a.RbLnum as 借书数"
                + " from ReaderCategory as b,Reader as a where a.Rcategoryid=b.Rcategoryid ";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
