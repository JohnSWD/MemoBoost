using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class ScheduleManager :IScheduleManager
    {
        private double CalculateEF(double EF, int q)
        {
            double nEF = EF + (0.1 - (4 - q) * (0.08 + (4 - q) * 0.02));
            if (nEF >= 1.3)
                return nEF;
            else
                return 1.3;

        }

        private int CalculateInterval(double EF, int Interval, int skipped, int q)
        {
            double nInterval = (Interval + skipped / (0.5 * q * q - 4.5 * q + 11)) * EF;
            return (int)(Math.Round(nInterval));
        }

        private void Reviewing(Card card, int q)
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
                StudySession.Default.SaveInfo(2);
            }
            Factory.Default.GetCardsRepository().ChangeItem(card);
        }

        private void Learning(Card card, int q)
        {
            card.State = 1;
            if (q == 4)
            {
                card.Steps = 2;
                card.State = 2;
                card.Next = DateTime.Now.AddDays(4);
                StudySession.Default.SaveInfo(1);
            }
            else if (q == 3)
            {
                card.Steps += 1;
                if (card.Steps == 2)
                {
                    card.State = 2;
                    card.Next = DateTime.Now.AddDays(1);
                    StudySession.Default.SaveInfo(1);
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

        public void CardVerdict(Card card, int q)
        {
            if (card.State <2)
                Learning(card, q);
            else
                Reviewing(card, q);
        }

        public void Reset(Card card)
        {
            card.State = 0;
            card.Steps = 0;
            card.EF = 2.5;
            card.Next = DateTime.Now;
            card.Interval = 0;
            Factory.Default.GetCardsRepository().ChangeItem(card);
        }
    }
}
