using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class MarketingRep : EntityBase
    {
        public MarketingRep()
        {
        }

        public MarketingRep(int userId, string firstName, string lastName)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        public int UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        //public static MarketingRepDto Convert2Dto(MarketingRep rep)
        //{
        //    MarketingRepDto r = new MarketingRepDto();
        //    r.UserId = rep.UserId;
        //    r.FirstName = rep.FirstName;
        //    r.LastName = rep.LastName;
        //    return r;
        //}

        //public static MarketingRep ExtractFromDto(MarketingRepDto rep)
        //{
        //    MarketingRep r = new MarketingRep();
        //    r.UserId = rep.UserId;
        //    r.FirstName = rep.FirstName;
        //    r.LastName = rep.LastName;
        //    return r;
        //}

        public override string ToString()
        {
            return LastName + ", " + FirstName;
        }
    }
}
