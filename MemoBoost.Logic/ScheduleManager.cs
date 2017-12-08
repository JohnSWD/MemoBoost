using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class ScheduleManager//responsible for moidfying cards' states; main algorithm
    {
        private static double CalculateEF(double EF, int q)
        {
            double nEF = EF + (0.1 - (4 - q) * (0.08 + (4 - q) * 0.02));
            if (nEF >= 1.3)
                return nEF;
            else
                return 1.3;

        }

        private static int CalculateInterval(double EF, int Interval, int skipped, int q) //q>1; skipped=DateTime.Now-Next
        {
            double nInterval = (Interval + skipped / (0.5 * q * q - 4.5 * q + 11)) * EF;
            return (int)(Math.Round(nInterval));
        }

        private static void Reviewing(Card card, int q)
        {
            if (q == 1)
            {
                card.State = 0;
                card.Interval = 1;
                card.Next = DateTime.Now.AddDays(1);
            }
            else
            {
                TimeSpan diff = card.Next - DateTime.Now;
                var skipped = (int)diff.TotalDays;
                card.EF = CalculateEF(card.EF, q);
                card.Interval = CalculateInterval(card.EF, card.Interval, skipped, q);
                card.Next = DateTime.Now.AddDays(card.Interval);
            }
            Factory.Default.GetCardsRepository().ChangeItem(card);
        }

        private static void Learning(Card card, int q) //easy=4, good=3, relearn=1
        {
            card.State = 1;
            if (q == 4)
            {
                card.Steps = 2;
                card.State = 2;
                card.Next = DateTime.Now.AddDays(4);
            }
            else if (q == 3)
            {
                card.Steps += 1;
                if (card.Steps == 2)
                {
                    card.State = 2;
                    card.Next = DateTime.Now.AddDays(1);
                }
                else
                    card.Next = DateTime.Now.AddMinutes(10);
            }
            else
            {
                card.Steps = 0;
                card.Next=DateTime.Now.AddMinutes(1);
            }
            Factory.Default.GetCardsRepository().ChangeItem(card);
        }

        public static void CardVerdict(Card card, int q)
        {
            if (card.State == 0)
                Learning(card, q);
            else
                Reviewing(card, q);
        }
    }
}
