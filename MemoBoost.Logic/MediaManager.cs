using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class MediaManager : IMediaManager
    {

        public void Copy(string file, out string filePath)//worked without out, 
        {
            string mediaFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media");
            if (!Directory.Exists(mediaFolder)) Directory.CreateDirectory(mediaFolder);
            filePath = Path.Combine(mediaFolder, Path.GetFileName(file));
            if (!File.Exists(filePath))
                File.Copy(file, filePath);
            else
            {
                filePath = Modify(filePath);
                File.Copy(file, filePath);
            }
        }

        private string Modify(string filepath)
        {
            int count = 1;
            string name = Path.GetFileNameWithoutExtension(filepath);
            string extension = Path.GetExtension(filepath);
            string path = Path.GetDirectoryName(filepath);
            string modified = filepath;
            while (File.Exists(modified))
            {
                string t = string.Format("{0}({1})", name, count++);
                modified=Path.Combine(path, t + extension);
            }
            return modified;
        }

        public void Remove(string path)//exception
        {
            string fpath= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media");
            File.Delete(Path.Combine(fpath, Path.GetFileName(path)));
        }
    }
}
