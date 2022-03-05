using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace TXQ.Utils.Tool
{
    public static class EXDataTable
    {

        /// <summary>
        /// 导出EXCEL文件
        /// </summary>
        /// <param name="DT"></param>
        /// <param name="path"></param>
        public static void EXToXlsx(this DataTable DT, string path)
        {
            MiniExcel.SaveAs(path, DT);
        }
        /// <summary>
        /// 导出EXCEL文件
        /// </summary>
        /// <param name="DT"></param>
        public static void EXToXlsx(this DataTable DT)
        {
            string FILE = ExDirectoryInfo.SaveFileDialog("EXCEL|xlsx");
            if (FILE == null)
            {
                return;
            }
            MiniExcel.SaveAs(FILE, DT);
        }

        /// <summary>  
        /// 填充对象列表：用DataTable填充实体类
        /// </summary>  
        public static List<T> EXToTModel<T>(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T model = (T)Activator.CreateInstance(typeof(T));
                //  T model=default;
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    PropertyInfo propertyInfo = typeof(T).GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                    {
                        propertyInfo.SetValue(model, dr[i], null);
                    }
                }

                modelList.Add(model);
            }
            return modelList;
        }
    }
}
