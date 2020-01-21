using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CodeShare.Models
{
    public static class Constants
    {
        public static string FolderPath = "~/Files";
        public static string PublicDir = Path.Combine(FolderPath, "Public");
            
        public static string GetDir(string dirName)
        {
            return Path.Combine(FolderPath, dirName);
        }
    }
}