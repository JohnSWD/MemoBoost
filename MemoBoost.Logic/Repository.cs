using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class Repository<T>
    {
        protected List<T> _items;

        public IEnumerable<T> Items
        {
            get
            {
                return _items; 
            }
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Delete(T item)
        {
            _items.Remove(item);
        }

        public void Change(T item)
        {
            //some code
        }

    }
}
