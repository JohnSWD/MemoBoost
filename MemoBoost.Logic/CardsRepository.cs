using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class CardsRepository :Repository<Card>
    {
        public CardsRepository()
        {
            
     
            /*Factory.Default.GetCardsRepository().Add(new Card { Question = "図書館", Answer = "library", DID = 1, ID = 2 });
            Factory.Default.GetCardsRepository().Add(new Card { Question = "新しい", Answer = "new", DID = 1, ID = 3 });
            Factory.Default.GetCardsRepository().Add(new Card { Question = "週末", Answer = "weekend", DID = 2, ID = 4 });
            Factory.Default.GetCardsRepository().Add(new Card { Question = "先週", Answer = "last week", DID = 3, ID = 5 });*/
        }
    }
}
