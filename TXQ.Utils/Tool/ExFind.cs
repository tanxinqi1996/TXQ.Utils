using System;
using System.IO;

namespace TXQ.Utils.Tool
{
    public static class ExFind
    {
        public static DirectoryInfo FindDir(string Dir)
        {
            foreach (DriveInfo ITEM in DriveInfo.GetDrives())
            {
                string path = $"{ITEM}\\{Dir}";
                if (Directory.Exists(path))
                {
                    return new DirectoryInfo(path);
                }
            }
            throw new Exception("Directory Not Exist");
        }


        public static FileInfo FindFile(string Path)
        {
            foreach (DriveInfo ITEM in DriveInfo.GetDrives())
            {
                string path = $"{ITEM}\\{Path}";
                if (Directory.Exists(path))
                {
                    return new FileInfo(path);
                }
            }
            throw new Exception("File Not Exist");
        }
    }
}
