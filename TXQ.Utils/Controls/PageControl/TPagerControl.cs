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

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.FIRST = new System.Windows.Forms.ToolStripStatusLabel();
            this.UP = new System.Windows.Forms.ToolStripStatusLabel();
            this.DOWN = new System.Windows.Forms.ToolStripStatusLabel();
            this.LAST = new System.Windows.Forms.ToolStripStatusLabel();
            this.EXPORT = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripSplitButton1,
            this.FIRST,
            this.UP,
            this.DOWN,
            this.LAST,
            this.EXPORT});
            this.statusStrip1.Location = new System.Drawing.Point(0, 180);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(599, 23);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 62;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 18);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(130, 18);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.全部ToolStripMenuItem});
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(74, 21);
            this.toolStripSplitButton1.Text = "每页50条";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "每页50条";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "每页100条";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem3.Text = "每页200条";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem4.Text = "每页500条";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.ToolStripMenuItem4_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem6.Text = "每页1000条";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.ToolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem7.Text = "每页10000条";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.ToolStripMenuItem7_Click);
            // 
            // FIRST
            // 
            this.FIRST.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FIRST.IsLink = true;
            this.FIRST.Name = "FIRST";
            this.FIRST.Size = new System.Drawing.Size(32, 18);
            this.FIRST.Text = "首页";
            this.FIRST.Click += new System.EventHandler(this.ToolStripStatusLabel3_Click_1);
            // 
            // UP
            // 
            this.UP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.UP.IsLink = true;
            this.UP.Name = "UP";
            this.UP.Size = new System.Drawing.Size(44, 18);
            this.UP.Text = "上一页";
            this.UP.Click += new System.EventHandler(this.ToolStripStatusLabel2_Click);
            // 
            // DOWN
            // 
            this.DOWN.IsLink = true;
            this.DOWN.Name = "DOWN";
            this.DOWN.Size = new System.Drawing.Size(44, 18);
            this.DOWN.Text = "下一页";
            this.DOWN.Click += new System.EventHandler(this.ToolStripStatusLabel3_Click);
            // 
            // LAST
            // 
            this.LAST.IsLink = true;
            this.LAST.Name = "LAST";
            this.LAST.Size = new System.Drawing.Size(32, 18);
            this.LAST.Text = "末页";
            this.LAST.Click += new System.EventHandler(this.ToolStripStatusLabel4_Click_1);
            // 
            // EXPORT
            // 
            this.EXPORT.IsLink = true;
            this.EXPORT.Name = "EXPORT";
            this.EXPORT.Size = new System.Drawing.Size(68, 18);
            this.EXPORT.Text = "导出当前页";
            this.EXPORT.Click += new System.EventHandler(this.ToolStripStatusLabel4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuItem5.Text = "100";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(599, 180);
            this.dataGridView1.TabIndex = 63;
            this.dataGridView1.VirtualMode = true;
            // 
            // 全部ToolStripMenuItem
            // 
            this.全部ToolStripMenuItem.Name = "全部ToolStripMenuItem";
            this.全部ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.全部ToolStripMenuItem.Text = "全部";
            this.全部ToolStripMenuItem.Click += new System.EventHandler(this.全部ToolStripMenuItem_Click);
            // 
            // PagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "PagerControl";
            this.Size = new System.Drawing.Size(599, 203);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripStatusLabel UP;
        private System.Windows.Forms.ToolStripStatusLabel DOWN;
        private System.Windows.Forms.ToolStripStatusLabel EXPORT;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel FIRST;
        private System.Windows.Forms.ToolStripStatusLabel LAST;
        private System.Windows.Forms.ToolStripMenuItem 全部ToolStripMenuItem;








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

