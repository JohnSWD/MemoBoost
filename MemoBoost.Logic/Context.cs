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

        public Context() : base("MBDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>().HasOptional(i => i.Deck).WithMany(d => d.Cards).HasForeignKey(k => k.DeckID);
            base.OnModelCreating(modelBuilder);
        }
    }
}
