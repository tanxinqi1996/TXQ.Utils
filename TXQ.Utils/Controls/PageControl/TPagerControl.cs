using System;
using System.Diagnostics;
using System.Windows.Forms;
using TXQ.Utils.Tool;

namespace TXQ.Utils.Controls
{
    public partial class TPagerControl : UserControl
    {
        public TPagerControl()
        {
            InitializeComponent();
            DrawControl(true);
            toolStripSplitButton1.Text = string.Format("每页{0}条", PageSize);
        }
        #region 分页字段和属性
        private int pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public virtual int PageIndex
        {
            get => pageIndex;
            set => pageIndex = value;
        }
        public virtual DataGridView DataGridView => dataGridView1;
        public virtual DataGridViewRow SelectedRow => DataGridView.CurrentRow;
        private int pageSize = 100;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public virtual int PageSize
        {
            get => pageSize;
            set => pageSize = value;
        }
        private int recordCount;
        /// <summary>
        /// 总记录数
        /// </summary>
        public virtual int RecordCount
        {
            get => recordCount;
            set => recordCount = value;
        }
        private int pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (pageSize != 0)
                {
                    pageCount = GetPageCount();
                }
                return pageCount;
            }
            set => pageCount = value;
        }
        /// <summary>
        /// 显示导出按钮
        /// </summary>
        public bool ShowExport
        {
            get => EXPORT.Visible;
            set => EXPORT.Visible = value;
        }
        /// <summary>
        /// 显示工具栏
        /// </summary>
        public bool ShowTaskBar
        {
            get => statusStrip1.Visible;
            set => statusStrip1.Visible = value;
        }
        #endregion
        #region 页码变化触发事件
        public event EventHandler OnPageChanged;
        #endregion
        #region 分页及相关事件功能实现
        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <returns></returns>
        private int GetPageCount()
        {
            if (PageSize == 0)
            {
                return 0;
            }
            int pageCount;
            if (RecordCount % PageSize == 0)
            {
                pageCount = RecordCount / PageSize;
            }
            else
            {
                pageCount = RecordCount / PageSize + 1;
            }
            return pageCount;
        }
        /// <summary>
        /// 外部调用
        /// </summary>
        public void DrawControl(int count, object DataSource = null)
        {
            Invoke(new Action(() =>
            {
                recordCount = count;
                DrawControl(false);
                DataGridView.DataSource = DataSource;
            }));
        }
        /// <summary>
        /// 页面控件呈现
        /// </summary>
        private void DrawControl(bool callEvent)
        {
            toolStripStatusLabel1.Text = string.Format("{0} / {1} 页     共  {2}  条记录     ", PageIndex, PageCount, RecordCount);
            if (callEvent && OnPageChanged != null)
            {
                OnPageChanged(this, null);//当前分页数字改变时，触发委托事件
            }
            DOWN.Enabled = true;
            UP.Enabled = true;
            FIRST.Enabled = true;
            LAST.Enabled = true;
            if (PageCount <= 1)//有且仅有一页
            {
                DOWN.Enabled = false;
                UP.Enabled = false;
                LAST.Enabled = false;
                FIRST.Enabled = false;
            }
            else if (PageIndex == 1)//第一页
            {
                UP.Enabled = false;
            }
            else if (PageIndex == PageCount)//最后一页
            {
                DOWN.Enabled = false;
                LAST.Enabled = false;
            }
        }
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "每页50条";
            PageSize = 50;
            pageIndex = 1;
            DrawControl(true);
        }
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "每页100条";
            PageSize = 100;
            pageIndex = 1;
            DrawControl(true);
        }
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "每页200条";
            PageSize = 200;
            pageIndex = 1;
            DrawControl(true);
        }
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "每页500条";
            PageSize = 500;
            pageIndex = 1;
            DrawControl(true);
        }
        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "每页1000条";
            PageSize = 1000;
            pageIndex = 1;
            DrawControl(true);
        }
        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "每页10000条";
            PageSize = 10000;
            pageIndex = 1;
            DrawControl(true);
        }
        private void ToolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            PageIndex = Math.Max(1, PageIndex - 1);
            DrawControl(true);
        }
        private void ToolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            PageIndex = Math.Max(1, PageIndex + 1);
            DrawControl(true);
        }
        private void ToolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            string file = DataGridView.EXToExcel();
            if (file != null)
            {
                if (MessageBox.Show("是否打开导出文件", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(file);
                }
            }
        }
        private void ToolStripStatusLabel4_Click_1(object sender, EventArgs e)
        {
            PageIndex = pageCount;
            DrawControl(true);
        }
        private void ToolStripStatusLabel3_Click_1(object sender, EventArgs e)
        {
            PageIndex = 1;
            DrawControl(true);
        }
        #endregion

        private void 全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButton1.Text = "全部";
            PageSize = 0;
            pageIndex = 1;
            DrawControl(true);

        }
    }
}

