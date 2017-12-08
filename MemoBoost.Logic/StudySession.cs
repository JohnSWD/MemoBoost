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
            get { return _currentSession; }
            set { _currentSession = GetCardsToLearn(value); }
        }

        public List<Card> GetCardsToLearn(List<Card> cards)
        {
            return cards.Where(c => c.Next <= DateTime.Now).OrderBy(c => c.State).OrderBy(c => c.Next).ToList();
        }

    }
}
