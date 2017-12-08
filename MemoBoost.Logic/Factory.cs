using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class Factory
    {
        private static Factory _default;
        private Repository<Card> _cardsrep = new CardsRepository();
        private Repository<Deck> _decksrep = new DecksRepository();
      

        public static Factory Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new Factory();
                }
                return _default;
            }
        }

        public Repository<Card> GetCardsRepository()
        {
            return _cardsrep;
        }

        public Repository<Deck> GetDecksRepository()
        {
            return _decksrep;
        }

        
    }
}
