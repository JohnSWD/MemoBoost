using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class MediaManager
    {
        public static void Copy(string file)
        {
            string mediaFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media");
            if (!Directory.Exists(mediaFolder)) Directory.CreateDirectory(mediaFolder);
            string filePath = Path.Combine(mediaFolder, Path.GetFileName(file));
            File.Copy(file, filePath);
        }
    }
}
