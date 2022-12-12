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
    public partial class insertreader : Form
    {
        public insertreader()
        {
            InitializeComponent();
        }

        string connString = @"Data Source =.;Initial Catalog = Library ; User ID= sa;Pwd = 123456";
        SqlConnection conn;
        SqlCommand comm;

        public bool check()
        {
            string username = textBox2.Text.Trim();
            string sql = String.Format("Select count(*) from ReaderCategory where Rcategoryname = '{0}'", username);
            conn.Open();
            comm.CommandText = sql;
            int k = (int)comm.ExecuteScalar();
            if (k > 0)
            {
                MessageBox.Show("该读者已存在", "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Clear();
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int bnum = 0, rday = 0;
            if (textBox2.Text.Trim() == string.Empty || textBox3.Text.Trim() == string.Empty || textBox4.Text.Trim() == string.Empty)
            {
                MessageBox.Show("信息不完整", "信息不完整", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (check())
                {
                    conn.Open();
                    string sql = "Select case when max(Rcategoryid) is NULL then 0 else max(Rcategoryid) end from ReaderCategory";
                    comm.CommandText = sql;
                    int categoryid = (int)comm.ExecuteScalar() + 1;
                    conn.Close();
                    
                    bool flag = true;
                    string categoryname = textBox2.Text.Trim();
                    try
                    {
                        bnum = Convert.ToInt32(textBox3.Text.Trim());
                        rday = Convert.ToInt32(textBox4.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "数据出错！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (bnum <= 0)
                        {
                            MessageBox.Show("借书数必须为正整数！", "数据出错！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            flag = false;
                        }
                        if (rday <= 0)
                        {
                            MessageBox.Show("借阅天数必须为正整数！", "数据出错！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            flag = false;
                        }
                        if (flag == true)
                        {
                            sql = String.Format("insert into ReaderCategory(Rcategoryid,Rcategoryname,Rbnum,Rday)values({0},'{1}',{2},{3}) ", categoryid, categoryname, bnum, rday);
                            try
                            {
                                conn.Open();
                                comm.CommandText = sql;
                                int count = comm.ExecuteNonQuery();
                                if (count > 0)
                                    MessageBox.Show("添加成功", "添加成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("添加成功", "添加成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "操作数据库出错", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            finally
                            {
                                conn.Close();
                            }
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                        }
                    }
                }
            }
        }

        private void insertreader_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connString);
            comm = new SqlCommand();
            comm.Connection = conn;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            check();
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            check();
        }
    }
}