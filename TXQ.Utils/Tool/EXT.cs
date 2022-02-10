using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TXQ.Utils.Tool
{
    public static class EXT
    {
        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        public static DataTable EXListToDataTable<T>(this List<T> list)
        {
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");

            //创建传入对象名称的列
            foreach (System.Reflection.PropertyInfo item in list.FirstOrDefault().GetType().GetProperties())
            {
                dt.Columns.Add(item.Name);
            }
            //循环存储
            foreach (T item in list)
            {
                //新加行
                DataRow value = dt.NewRow();
                //根据DataTable中的值，进行对应的赋值
                foreach (DataColumn dtColumn in dt.Columns)
                {
                    int i = dt.Columns.IndexOf(dtColumn);
                    //基元元素，直接复制，对象类型等，进行序列化
                    if (value.GetType().IsPrimitive)
                    {
                        value[i] = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item);
                    }
                    else
                    {
                        value[i] = JsonConvert.SerializeObject(item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item));
                    }
                }
                dt.Rows.Add(value);
            }
            return dt;
        }
    }
}
