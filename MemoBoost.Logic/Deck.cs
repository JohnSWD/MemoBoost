using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class Deck //can be stored as json in a bigger class (collection) ?
    {
        public int ID { get; set; }

        public string Name { get; set; }

        //public string CardsJ { get; set; }

        public virtual ICollection<Card> Cards { get; set; } //will be stored as JSON string 

        public int RevCards
        {
            get
            {
                if(Cards!=null)
                    return this.StateCount(2).Count;
                else
                    return 0;
            } 
        }

        public int NewCard
        {
            get
            {
                if (Cards!= null)
                    return this.StateCount(0).Count;
                else
                    return 0;
            }
        }
        public int LrnCards
        {
            get
            {
                if (Cards!= null)
                    return this.StateCount(1).Count;
                else
                    return 0;
            }
        }

        public ICollection<Card> StateCount(int state)
        {
            return Cards.Where(i => i.State == state).Where(i=>i.Next<=DateTime.Now).ToList();
        }
    }
}