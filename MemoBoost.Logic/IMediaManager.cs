using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public interface IMediaManager
    {
        void Copy(string file, out string filePath);
        void Remove();
        void ToBeDisposed(string path);
    }
}
