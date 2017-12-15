using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Items { get; }

        IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);

        void Add(TEntity item);

        void AddRange(IEnumerable<TEntity> items);

        void Delete(TEntity item);

        void DeleteRange(IEnumerable<TEntity> items);

        void ChangeItem(TEntity item);
    }
}
