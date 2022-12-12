using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class ManagerMenu : Form
    {
        public ManagerMenu()
        {
            InitializeComponent();
        }

        public event ChildClose closeFather;

        private bool showChildrenForm(string p_ChildrenFormName)
        {
            int i;
            //依次检测当前窗体的子窗体  
            for (i = 0; i < this.MdiChildren.Length; i++)
            {
                //判断当前子窗体的name属性值是否与传入的字符串值相同  
                if (this.MdiChildren[i].Name == p_ChildrenFormName)
                {
                    //此子窗体是目标子窗体，激活之  
                    //this.MdiChildren[i].Activate();
                    this.MdiChildren[i].WindowState = FormWindowState.Normal;
                    return true;
                }
            }
            return false;
        }

        private void TopMostChildrenForm(string p_ChildrenFormName)
        {
            int i;
            for (i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Name == p_ChildrenFormName)
                {
                    //此子窗体是目标子窗体，激活之  
                    this.MdiChildren[i].Activate();
                    this.MdiChildren[i].WindowState = FormWindowState.Normal;
                    this.MdiChildren[i].Show();
                    break;
                }
            }
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定退出？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
                Application.Exit();
        }

        private void 注销DToolStripMenuItem_Click(object sender, EventArgs e)
        {
             DialogResult dr = MessageBox.Show("确定注销？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             if (dr == DialogResult.OK)
                 this.Close();
        }
        
        private void tsmAddExp_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("addworker"))
            {
                addworker a = new addworker();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }
            toolStripStatusLabel2.Text = "添加普通用户";
            TopMostChildrenForm("addworker");
        }
        
        private void tsmStatistics_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("UpdateReader"))
            {
                UpdateReader a = new UpdateReader();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "重置用户密码";
            TopMostChildrenForm("UpdateReader");
        }

        private void 添加图书类别AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("AddBookCategory"))
            {
                AddBookCategory a = new AddBookCategory();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "添加图书类别";
            TopMostChildrenForm("AddBookCategory");
        }
        
        private void 修改图书类别MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("ModifyBookCategory"))
            {
                ModifyBookCategory a = new ModifyBookCategory();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }
            toolStripStatusLabel2.Text = "修改图书类别";
            TopMostChildrenForm("ModifyBookCategory");
        }
        
        private void 查询图书类别SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("SelectBookCategory"))
            {
                SelectBookCategory a = new SelectBookCategory();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "查询图书类别";
            TopMostChildrenForm("SelectBookCategory");
        }
        
        private void 添加图书信息AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("AddBookList"))
            {
                AddBookList a = new AddBookList();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "添加图书类别";
            TopMostChildrenForm("AddBookList");
        }
        
        private void 修改图书信息MToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("ModifyBookList"))
            {
                ModifyBookList a = new ModifyBookList();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }
            toolStripStatusLabel2.Text = "修改图书信息";
            TopMostChildrenForm("ModifyBookList");
        }
        
        private void 查询图书信息SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("SelectBookList"))
            {
                SelectBookList a = new SelectBookList();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "查询图书信息";
            TopMostChildrenForm("SelectBookList");
        }
        
        private void 添加读者类别AToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("insertreader"))
            {
                insertreader a = new insertreader();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }
            toolStripStatusLabel2.Text = "添加读者类别";
            TopMostChildrenForm("insertreader");
        }
        
        private void 查改读者类别MToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("selsectcategory"))
            {
                selsectcategory a = new selsectcategory();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "查改图书类别";
            TopMostChildrenForm("selsectcategory");
        }
        
        private void 添加读者信息AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("Reader"))
            {
                Reader a = new Reader();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "添加读者信息";
            TopMostChildrenForm("Reader");
        }
        
        private void 查改读者信息MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("allreader"))
            {
                allreader a = new allreader();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "查改读者信息";
            TopMostChildrenForm("allreader");
        }

        private void 活跃读者统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("StatisticsActiveReaders"))
            {
                StatisticsActiveReaders a = new StatisticsActiveReaders();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "活跃读者统计";
            TopMostChildrenForm("StatisticsActiveReaders");
        }
        
        private void 最热图书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("StatisticsBestBook"))
            {
                StatisticsBestBook a = new StatisticsBestBook();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "最热图书统计";
            TopMostChildrenForm("StatisticsBestBook");
        }
        
        private void 查询读者信息SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("selectreader"))
            {
                selectreader a = new selectreader();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "查询读者信息";
            TopMostChildrenForm("selectreader");
        }
        
        private void 删除借阅记录DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("DeleteLendBookList"))
            {
                DeleteLendBookList a = new DeleteLendBookList();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "删除借阅记录";
            TopMostChildrenForm("DeleteLendBookList");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now; // 声明一个DateTime的对象；  
            //获取当前的日期；  
            //string strDate = dt.ToLongDateString().ToString();  
            // 获取当前的时间；  
            string strTime = dt.ToLongTimeString().ToString();
            // 用控件显示出来；  
            label2.Text = strTime; 
        }
        
        private void 查改普通用户UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("SelectDeleteMana"))
            {
                SelectDeleteMana a = new SelectDeleteMana();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "查改普通用户";
            TopMostChildrenForm("SelectDeleteMana");
        }
        
        private void 删除读者信息DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("deletereader"))
            {
                deletereader a = new deletereader();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "删除读者信息";
            TopMostChildrenForm("deletereader");
        }
        
        private void 失信读者统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("StatisticsBadReader"))
            {
                StatisticsBadReader a = new StatisticsBadReader();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "失信读者统计";
            TopMostChildrenForm("StatisticsBadReader");
        }
        
        private void 删除图书信息DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("DeleteBookList"))
            {
                DeleteBookList a = new DeleteBookList();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "删除图书信息";
            TopMostChildrenForm("DeleteBookList");
        }
        
        private void 报废图书处理BToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("ScrapBook"))
            {
                ScrapBook a = new ScrapBook();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }   
            toolStripStatusLabel2.Text = "报废图书处理";
            TopMostChildrenForm("ScrapBook");
        }

        private void ManagerMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            int k = 0;
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].WindowState == FormWindowState.Maximized || this.MdiChildren[i].WindowState == FormWindowState.Minimized || this.MdiChildren[i].WindowState == FormWindowState.Normal)
                {
                    k++;
                }
            }
            if (k > 1)
            {
                DialogResult result = MessageBox.Show("当前窗口以打开多个页面，确定要全部关闭？", "关闭", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    closeFather();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else 
            {
                closeFather();
            }
        }
        
        private void 最热图书统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showChildrenForm("StatisticsGoodBook"))
            {
                StatisticsGoodBook a = new StatisticsGoodBook();
                this.IsMdiContainer = true;
                a.MdiParent = this;
                a.Show();
            }    
            toolStripStatusLabel2.Text = "最热图书统计";
            TopMostChildrenForm("StatisticsGoodBook");
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("开发者：马超     张含松\n            宋富路   赵中奇\n\n系统版本：1.0.0");

        }
    }
}
