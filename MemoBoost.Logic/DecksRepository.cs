using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class DecksRepository :Repository<Deck>
    {
        public DecksRepository()
        {
            _items = new List<Deck>
            {
                new Deck
                {
                    ID=1, Name="Default"
                },
                new Deck
                {
                    ID=2, Name="Default2"
                },
                new Deck
                {
                    ID=3, Name="Default3"
                }
            };
        }
    }
}
