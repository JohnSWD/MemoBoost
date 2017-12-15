using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public interface IScheduleManager
    {
        void CardVerdict(Card card, int q);

        void Reset(Card card);
    }
}
