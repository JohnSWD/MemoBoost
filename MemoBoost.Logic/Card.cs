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

        public int State { get; set; } = 0;

        public DateTime Next { get; set; } = DateTime.Now; // when the card will be shown again

        public double EF { get; set; } = 2.5; //easines factore

        public int Steps { get; set; } = 0; //steps to graduate to review state

        public int Interval { get; set; } = 1; //gap between card's appearances

        public string Question { get; set; }

        public string Answer { get; set; }

        public string PASource{get;set;}//image name for answer

        [NotMapped]
        public string ASource { get { return PASource!=null ? Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media"), PASource) : null; } set { if (value != null) PASource = Path.GetFileName(value); } }
        
        public string PQSource { get; set; }//image name for question

        [NotMapped]
        public string QSource { get { return PQSource!=null ? Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Media"), PQSource):null; } set { if (value != null) PQSource = Path.GetFileName(value); } }

    }
}
    

