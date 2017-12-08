using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
   public abstract class Repository<TEntity> where TEntity: class
    {
        protected IEnumerable<TEntity> _items;

        public IEnumerable<TEntity> Items
        {
            get
            {
                using (Context context = new Context())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return _items=context.Set<TEntity>().ToList();
                }
            }
        }

        public virtual void Add(TEntity item)
        {
            using (var context = new Context())
            {
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void AddRange(IEnumerable<TEntity> items)
        {
            using (var context = new Context())
            {
                foreach (var item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual void Delete(TEntity item)
        {
            using (var context = new Context())
            {
                context.Entry(item).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual void DeleteRange(IEnumerable<TEntity> items)
        {
            using (var context = new Context())
            {
                foreach (var item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }


        public virtual void ChangeItem(TEntity item)
        {
            using (var context = new Context())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
        {
            using (Context context = new Context())
            {
                var list = context.Set<TEntity>().ToList();
                list = list.Where(predicate).ToList();
                return list;
            }
        }
    }
}
