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
                    return this.CardsToReview().Count;
                else
                    return 0;
            } 
        }

        public int NewCard
        {
            get
            {
                if (Cards!= null)
                    return this.NewCards().Count;
                else
                    return 0;
            }
        }
        public int LrnCards
        {
            get
            {
                if (Cards!= null)
                    return this.CardsToLearn().Count;
                else
                    return 0;
            }
        }



        public List<Card> CardsToReview() //cards with reviewing state(1)
        {
            return Cards.Where(i => i.State == 2).ToList();
        }

        public List<Card> NewCards() //new cards which havent been learnt (0)
        {
            return Cards.Where(i => i.State == 0).ToList();
        }

        public List<Card> CardsToLearn() //cards with learning state (3)
        {
            return Cards.Where(i => i.State == 1).ToList();
        }
    }
}