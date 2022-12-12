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
    public partial class DeleteLendBookList : Form
    {
        public DeleteLendBookList()
        {
            InitializeComponent();
        }

        string connString = @"Data Source=.;Initial Catalog=Library;User ID =sa;Pwd=123456";
        SqlConnection conn;
        SqlCommand comm;

        private void botton1_Click(object sender, EventArgs e)
        {
            string date1 = Convert.ToString(dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            string date2 = Convert.ToString(dateTimePicker2.Value.ToString("yyyy/MM/dd"));
            string sql = String.Format("Select count(*) from BookLendList where LendTime>= '{0}' and  LendTime<= '{1}' and isback = 1", date1, date2);
            conn.Open();
            comm.CommandText = sql;
            int mm = (int)comm.ExecuteScalar();
            conn.Close();
            string showlist = "共计 " + mm + " 条记录";
            if (mm == 0)
                MessageBox.Show(showlist + "！", "删除", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(mm > 0)
            {
                DialogResult result = MessageBox.Show(showlist+"，确定删除？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    sql = String.Format("Delete  from BookLendList where LendTime>= '{0}' and  LendTime<= '{1}' and isback = 1", date1, date2);
                    try
                    {
                        conn.Open();
                        comm.CommandText = sql;
                        int k = (int)comm.ExecuteNonQuery();
                        if (k == mm)
                            MessageBox.Show("删除成功！", "删除", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void DeleteLendBookList_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connString); 
            comm = new SqlCommand(); 
            comm.Connection = conn;
        }
    }
}
