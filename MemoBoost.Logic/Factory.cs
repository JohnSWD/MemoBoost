using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoBoost.Logic
{
    public class Factory
    {
        private static Factory _default;
        private IRepository<Card> _cardsrep = new CardsRepository();
        private IRepository<Deck> _decksrep = new DecksRepository();
        private IRepository<User> _usersrep = new UsersRepository();
        private IRepository<Statisticks> _strep = new StatisticksRepository();
        private IScheduleManager _scheduleManager = new ScheduleManager();
        private IMediaManager _mediaManager = new MediaManager();

        public static Factory Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new Factory();
                }
                return _default;
            }
        }

        public IRepository<Statisticks> GetStsRepository()
        {
            return _strep;
        }

        public IRepository<Card> GetCardsRepository()
        {
            return _cardsrep;
        }

        public IRepository<Deck> GetDecksRepository()
        {
            return _decksrep;
        }

        public IRepository<User> GetUsersRepository()
        {
            return _usersrep;
        }

        public IScheduleManager GetScheduler()
        {
            return _scheduleManager;
        }
        
        public IMediaManager GetMediaManager()
        {
            return _mediaManager;
        }
    }
}
