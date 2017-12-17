using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class Statisticks
    {
        public int ID { get; set; }

        public User User { get; set; }

        public int UserID { get; set; }

        public DateTime Date { get; set; }

        public int Studied { get; set; } = 0;

        public int Reviewed { get; set; } = 0;
    }
}
