using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    class Context :DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<User> User{ get; set; }

        public Context() : base("MBDB")
        {
           Database.SetInitializer(new MBDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deck>().HasOptional(i => i.User).WithMany(u => u.Decks).HasForeignKey(k => k.UserID);
            modelBuilder.Entity<Card>().HasOptional(i => i.Deck).WithMany(d => d.Cards).HasForeignKey(k => k.DeckID);
            base.OnModelCreating(modelBuilder);
        }


    }
}
