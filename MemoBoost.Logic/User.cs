using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public virtual List<Deck> Decks { get; set; } 
    }
}
