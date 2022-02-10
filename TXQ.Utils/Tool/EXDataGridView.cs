using MiniExcelLibs;
using System;
using System.Data;
using System.Windows.Forms;

namespace TXQ.Utils.Tool
{
    public static class EXDataGridView
    {
        public static DataTable EXToDataTable(this DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].HeaderText.ToString());
                dt.Columns.Add(dc);
            }
            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    if (dgv.Rows[count].Cells[countsub].Value == null)
                    {
                        dr[countsub] = " ";
                    }
                    else
                    {
                        dr[countsub] = dgv.Rows[count].Cells[countsub].Value.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 导出Excel，不依赖本地是否装有Excel，导出速度快
        /// </summary>
        /// <param name="dataGridView1">要导出的dataGridView控件</param>
        ///
        public static string EXToExcel(this DataGridView dataGridView1)
        {
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = "EXCEL|*.xlsx"
            };
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return null;
            }
            MiniExcel.SaveAs(fileDialog.FileName, dataGridView1.EXToDataTable());
            return fileDialog.FileName;
        }
    }
}

