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
    public partial class BookSelectBorrowing : Form
    {
        public BookSelectBorrowing()
        {
            InitializeComponent();
        }
        string connString = @"Data Source=.;Initial Catalog=Library;User ID =sa;Pwd=123456";
        SqlConnection conn;
        private void BookSelectBorrowing_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if (radioButton2.Checked == true)
                sql = "select b.BookName as 书名,c.Rname as 姓名,a.LendTime as 借书时间,a.BackTime as 还书时间,a.money as 超期扣款,a.isback as 是否还书 "
            + "from BookLendList as a,BookList as b,Reader as c where a.ISBN=b.ISBN and a.Rid=c.Rid and a.ISBN='" + textBox1.Text.Trim() + "'";
            else
                sql = "select b.BookName as 书名,c.Rname as 姓名,a.LendTime as 借书时间,a.BackTime as 还书时间,a.money as 超期扣款,a.isback as 是否还书 "
            + "from BookLendList as a,BookList as b,Reader as c where a.ISBN=b.ISBN and a.Rid=c.Rid and b.BookName like '%" + textBox1.Text.Trim() + "%'";
            try
            {
                conn = new SqlConnection(connString);
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("没有相关记录", "请输入准确数据！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "操作数据库出错！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
