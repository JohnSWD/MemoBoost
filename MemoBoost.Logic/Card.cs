using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class Card
    {
        public int ID { get; set; }

        public int DID { get; set; } //get stackoverflow exception when try to use Deck

        public int State { get; set; } //new-0,learning-1 or studying-2

        public DateTime Next { get; set; } //the date when the card will be shown

        public double EF { get; set; } //easines factor (calculated via a function), (1.3,2.5); 2.5 - default value

        public int Steps { get; set; } //the number of steps to get to studying state [0;2]

        public int Interval { get; set; } //function of EF and number of studying days

        public string Question { get; set; }

        public string Answer { get; set; }

     
    }
}
