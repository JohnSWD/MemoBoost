using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class Card
    {
        public int ID { get; set; }

        public Deck Deck { get; set; }

        public int? DeckID { get; set; }

        public int State { get; set; } = 0;//new-0,learning-1 or studying-2

        public DateTime Next { get; set; } = DateTime.Now;//the date when the card will be shown

        public double EF { get; set; } = 2.5; //easines factor (calculated via a function), (1.3,2.5); 2.5 - default value

        public int Steps { get; set; } = 0;//the number of steps to get to studying state [0;2]

        public int Interval { get; set; } = 1;//function of EF and number of studying days

        public string Question { get; set; }

        public string Answer { get; set; }

        public string PASource{get;set;}

        [NotMapped]
        public string ASource { get { return PASource!=null ? Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media"), PASource) : null; } set { if (value != null) PASource = Path.GetFileName(value); } }

        public string PQSource { get; set; }

        [NotMapped]
        public string QSource { get { return PQSource!=null ? Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media"), PQSource):null; } set { if (value != null) PQSource = Path.GetFileName(value); } }

    }
}
    

