using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class StudySession
    {
        private static StudySession _default;
        private List<Card> _currentSession;
        public Deck CurrentDeck { get; set; }
        public int CurrentUserID { get; set; }


        public static StudySession Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new StudySession();
                }
                return _default;
            }
        }

        public List<Card> CurrentSession
        {
            get { return GetCardsToLearn(_currentSession); }
            set { _currentSession = GetCardsToLearn(value); }
        }

        private List<Card> GetCardsToLearn(List<Card> cards)
        {
            if (cards != null)
            {
                return cards.Where(c => c.Next <= DateTime.Now).OrderBy(c => c.State).OrderBy(c => c.Next).ToList();
            }
            else
                return new List<Card>();
        }

        public void SaveInfo(int state) //state: 1-study, 2-review
        {
            var i = Factory.Default.GetStsRepository().Items.FirstOrDefault(s => s.Date.ToShortDateString() == DateTime.Now.ToShortDateString() && s.UserID==CurrentUserID);
            if (i != null && state == 1)
            {
                i.Studied += 1;
                Factory.Default.GetStsRepository().ChangeItem(i);
            }
            else if (i != null && state == 2)
            {
                i.Reviewed += 1;
                Factory.Default.GetStsRepository().ChangeItem(i);
            }
            else
            {
                if (state == 1)
                {
                    var s = new Statisticks { Date = DateTime.Now, Studied = 1, UserID=CurrentUserID};
                    Factory.Default.GetStsRepository().Add(s);
                }
                else
                {
                    var s = new Statisticks { Date = DateTime.Now, Reviewed = 1, UserID=CurrentUserID};
                    Factory.Default.GetStsRepository().Add(s);
                }
            }


        }

    }
}
