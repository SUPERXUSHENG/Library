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
    public partial class deletereader : Form
    {
        public deletereader()
        {
            InitializeComponent();
        }
        string connString = @"Data Source=.;Initial Catalog=Library;User ID =sa;Pwd=123456";
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader dr;
        public static string num;
        public int q = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
                MessageBox.Show("查询信息不能为空！");
            string id = textBox1.Text.Trim();
            string condition = "";
            condition = String.Format("and a.Rid like %'{0}'%", id);
            string sql = " select * from Reader ";
            try
            {
                conn.Open();
                comm.CommandText = sql;
                dr = comm.ExecuteReader();

                while (dr.Read())
                {
                    string name = dr[1].ToString().Trim();
                    string category = dr["Rcategoryid"].ToString().Trim();
                    string ID = dr["Rid"].ToString().Trim();
                    string phone = dr["Phone"].ToString().Trim();
                    string Lnum = dr["Rblnum"].ToString().Trim();

                    if (id == ID)
                    {
                        num = Lnum;
                        richTextBox1.Text = "姓名：" + name + "\n身份证号：" + ID + "\n电话：" + phone + "\n已借书数目：" + Lnum;
                        q = q + 1;
                    }
                }
                if (q == 0 && textBox1.Text.Trim() != "")
                {
                    MessageBox.Show("未查找到!");
                    q = 0;
                    richTextBox1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作数据库出错", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
        }

        private void deletereader_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connString);
            comm = new SqlCommand();
            comm.Connection = conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("信息不完整！");
            }
            else
            {
                string id = textBox1.Text.Trim();
                conn.Open();
                string sql = String.Format("select count(*) from Reader where Rid = '{0}'", id);
                comm.CommandText = sql;
                int k = (int)comm.ExecuteScalar();
                conn.Close();
                if (k <= 0)
                    MessageBox.Show("删除失败！(无该读者)", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    sql = String.Format("select count(*) from Reader where Rid = '{0}' and  RbLnum > 0", id);
                    conn.Open();
                    comm.CommandText = sql;
                    k = (int)comm.ExecuteScalar();
                    conn.Close();
                    if (k > 0)
                        MessageBox.Show("删除失败！(有未归还书籍)", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        sql = String.Format("delete from Reader where Rid = '{0}'", id);
                        comm = new SqlCommand(sql, conn);
                        try
                        {
                            conn.Open();
                            comm.CommandText = sql;
                            int count = comm.ExecuteNonQuery();
                            if (count > 0 && num == "0")
                                MessageBox.Show("删除成功！", "删除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("删除失败！", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "操作数据库出错！", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
                textBox1.Clear();
                richTextBox1.Clear();
            }
        }
    }
}
