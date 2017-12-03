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
            _items = new List<Card>
            {
                new Card
                {
                    Question="大人",
                    Answer="adult",
                    DID=1,
                    ID=1
                },

                new Card
                {
                    Question="図書館",　
                    Answer="library",
                    DID=1,
                    ID=2
                },

                new Card
                {
                    Question="新しい",
                    Answer="new",
                    DID=1,
                    ID=3
                },

                new Card
                {
                    Question="週末",
                    Answer="weekend",
                    DID=2,
                    ID=4
                },

                new Card
                {
                    Question="先週",
                    Answer="last week",
                    DID=3,
                    ID=5
                }
            };
        }
    }
}
