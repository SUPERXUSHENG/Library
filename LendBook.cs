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
    public partial class LendBook : Form
    {
        public LendBook()
        {
            InitializeComponent();
        }
        string connString = @"Data Source = .;Initial Catalog = Library; User ID = sa; Pwd = 123456";
        SqlConnection conn; //声明连接对象
        SqlCommand comm; //声明命令对象
        string sql;

        private void LendBook_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connString); //创建Connection对象
            comm = new SqlCommand(); //创建Commmand对象
            comm.Connection = conn; //设置command使用的Connection对象
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            conn.Open();
            string Rid = txtReaderid.Text.Trim();
            string BISBN = txtbookISBN.Text.Trim();
            string d = dateTimePicker1.Value.ToString();

            if (txtReaderid.Text.Trim() == "" || txtbookISBN.Text.Trim() == "")
                MessageBox.Show("请完整填写信息！", "借阅", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                sql = String.Format("Select count(*) from BookList where ISBN = '{0}'", BISBN);
                comm.CommandText = sql;
                int kkk = (int)comm.ExecuteScalar();

               
                if (kkk <= 0)
                    MessageBox.Show("ISBN查找失败！\n   请查对", "借阅", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    sql = String.Format("Select count(*) from BookList where ISBN = '{0}' and Lendnum < num and bookstate = 1", BISBN);
                    comm.CommandText = sql;
                    kkk = (int)comm.ExecuteScalar();
                    if (kkk <= 0)
                        MessageBox.Show("该书已无库存！", "借阅", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        sql = String.Format("Select count(*) from Reader where Rid = '{0}'", Rid);
                        comm.CommandText = sql;
                        kkk = (int)comm.ExecuteScalar();
                        if (kkk <= 0)
                            MessageBox.Show("身份证号查找失败！\n   请查对", "借阅", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            //dateTimePicker2.Text = DateTime.Parse(DateTime.Now.ToString(dateTimePicker1.Text)).AddDays(90).ToShortDateString();
                            DateTime dt = DateTime.Now; //获取当前的日期； 
                            SqlDataReader dr;
                            sql = String.Format("Select count(*) from BookLendList where Rid = '{0}' and ISBN = '{1}' and isback = 0", Rid, BISBN);
                            comm.CommandText = sql;
                            int k2 = (int)comm.ExecuteScalar();
                            if (k2 > 0)
                            {
                                MessageBox.Show("该读者已借此书，且并未归还！");
                            }
                            else
                            {
                                //MessageBox.Show("Pass");
                                sql = String.Format("Select count(*) from BookLendList where Rid = '{0}' and BackTime < '{1}' and isback = 0", Rid, dt);
                                comm.CommandText = sql;
                                int k = (int)comm.ExecuteScalar();
                                double pm = 0;

                                sql = String.Format("Select * from Reader, ReaderCategory where Reader.Rid = '{0}' and Reader.Rcategoryid = ReaderCategory.Rcategoryid", Rid);
                                comm.CommandText = sql;
                                dr = comm.ExecuteReader();
                                dr.Read();
                                rtbRead.Clear();
                                rtbRead.Text = "身份证号：" + dr[0].ToString() + "\n姓名：" + dr["Rname"].ToString()
                                    + "\n读者类别：" + dr["Rcategoryname"].ToString() + "\n可借阅数目：" + dr["Rbnum"].ToString()
                                    + "\n已借阅数目：" + dr["RbLnum"].ToString() + "\n超期数目：";
                                int n = (int)dr["RbLnum"];
                                int m = (int)dr["Rbnum"];
                                int ad = (int)dr["Rday"];
                                dateTimePicker2.Text = DateTime.Parse(DateTime.Now.ToString(dateTimePicker1.Text)).AddDays(ad).ToShortDateString();
                                dr.Close();
                                if (k <= 0 && n < m)
                                {
                                    rtbRead.Text += 0;
                                    sql = String.Format("Select * from BookList, Bookcategory where ISBN = '{0}' and BookList.bCategoryid = Bookcategory.bCategoryid", BISBN);
                                    comm.CommandText = sql;
                                    dr = comm.ExecuteReader();
                                    dr.Read();
                                    rtbBook.Clear();
                                    rtbBook.Text = "ISBN：" + dr[0].ToString() + "\n书名：" + dr["BookName"].ToString() + "\n图书类别：" + dr["bCategoryname"].ToString()
                                           + "\n作者：" + dr["author"].ToString() + "\n出版日期：" + dr["publishTime"];
                                    dr.Close();
                                    DialogResult result = MessageBox.Show("确定借阅？", "借阅", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (result == DialogResult.OK)
                                    {
                                        sql = String.Format("Insert into BookLendList values('{0}','{1}','{2}','{3}', 0.0, 0, 0)", Rid, BISBN, d, dateTimePicker2.Value.ToString());
                                        comm.CommandText = sql;
                                        int kk = (int)comm.ExecuteNonQuery();
                                        sql = String.Format("Update BookList set lendnum = lendnum + 1  where ISBN = '{0}'", BISBN);
                                        comm.CommandText = sql;
                                        kk += (int)comm.ExecuteNonQuery();
                                        sql = String.Format("Update Reader set RbLnum = RbLnum + 1 where Rid = '{0}'", Rid);
                                        comm.CommandText = sql;
                                        kk += (int)comm.ExecuteNonQuery();
                                        if (kk == 3)
                                            MessageBox.Show("借阅成功！", "借阅", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        else
                                            MessageBox.Show("借阅失败！", "借阅", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else if (k > 0)
                                {
                                    rtbRead.Text += k;
                                    MessageBox.Show("该读者存在超期未还书籍", "借阅失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    sql = String.Format("Select * from BookList, BookLendList where Rid = '{0}' and BookList.ISBN = BookLendList.ISBN and BackTime < '{1}' and isback = 0", Rid, dt);
                                    comm.CommandText = sql;
                                    dr = comm.ExecuteReader();
                                    rtbBook.Clear();
                                    while (dr.Read())
                                    {
                                        rtbBook.Text += "书名：" + dr["BookName"].ToString() + "\n" + "借阅时间：" + dr["LendTime"].ToString() + "\n应还日期：" + dr["BackTime"].ToString() + "\n超期扣款：";
                                        string ddd = dr["BackTime"].ToString();
                                        string a = ddd.Substring(0, ddd.IndexOf("/"));
                                        int t = ddd.IndexOf("/");
                                        ddd = ddd.Remove(t, 1);
                                        ddd = ddd.Insert(t, "a");
                                        int t1 = ddd.IndexOf("a");
                                        t = ddd.IndexOf("/") - 1;
                                        string b = ddd.Substring(t1 + 1, t - t1);
                                        if (b.Length == 1)
                                            b = "0" + b;
                                        t1 = ddd.IndexOf(" ") - 1;
                                        string c = ddd.Substring(t + 2, t1 - t - 1);
                                        if (c.Length == 1)
                                            c = "0" + c;
                                        System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                                        dtFormat.ShortDatePattern = "yyyy/MM/dd";
                                        DateTime dd = Convert.ToDateTime(a + "/" + b + "/" + c, dtFormat);
                                        TimeSpan d3 = dt.Subtract(dd);
                                        pm += (double)d3.Days * 0.1;
                                        rtbBook.Text += (double)d3.Days * 0.1 + "\n****************************\n";
                                    }
                                    rtbRead.Text += "\n扣款总额：" + pm.ToString();
                                    dr.Close();
                                }
                                else if (n >= m)
                                {
                                    rtbRead.Text += k;
                                    MessageBox.Show("该读者借阅数目超限", "借阅失败", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }//正常
                        }//身份证号查阅失败
                    }//无库存
                }//ISBN查找失败
            }
            conn.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtReaderid.Clear();
            txtbookISBN.Clear();
            rtbRead.Clear();
            rtbBook.Clear();
        }
    }
}
