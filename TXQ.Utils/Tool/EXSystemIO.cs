using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TXQ.Utils.Tool
{
    public static class EXSystemIO
    {
        /// <summary>
        /// 使用递归的方式获取目录下的所有文件,包含子目录
        /// </summary>
        /// <param name="directoryInfo">目录</param>
        /// <returns>所有文件</returns>
        public static List<FileInfo> EXGetAllFiles(this DirectoryInfo directoryInfo)
        {
            List<DirectoryInfo> dirs = EXGetAllDirectories(directoryInfo);
            List<FileInfo> files = directoryInfo.GetFiles().ToList();
            foreach (DirectoryInfo item in dirs)
            {
                files.AddRange(item.GetFiles());
            }
            return files;
        }
        /// <summary>
        /// 使用递归的方式获取目录下的所有子目录
        /// </summary>
        /// <param name="directoryInfo">目录信息</param>
        /// <returns>子目录列表</returns>
        public static List<DirectoryInfo> EXGetAllDirectories(this DirectoryInfo directoryInfo)
        {
            List<DirectoryInfo> list = new List<DirectoryInfo>();
            foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
            {
                list.Add(dir);
                list.AddRange(dir.EXGetAllDirectories());
            }
            return list;

        }

        public static string SaveFileDialog(string Filter)
        {
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = Filter
            };
            if (fileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return null;
            }
            else
            {
                return fileDialog.FileName;
            }
        }
        public static string OpenFileDialog(string Filter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = Filter,
            };
            if (fileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return null;
            }
            else
            {
                return fileDialog.FileName;
            }
        }
    }
}
