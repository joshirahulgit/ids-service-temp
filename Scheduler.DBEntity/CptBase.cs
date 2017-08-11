using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CptBase : EntityBase
    {
        public String ShortDescription { get; set; }
        public String MediumDescription { get; set; }
        public String LongDescription { get; set; }
        //public long Id { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsSnomed { get; set; }
        public String Code { get; set; }
        public DateTime? OnsetDate { get; set; }
        public String AlertText { get; set; }
        public long? LinkedRoomId { get; set; }
        public int LinkedRoomTypeId { get; set; }
        public long? LinkedApptId { get; set; }
        public String Category { get; set; }

        public String CommonDescription
        {
            get
            {
                if (!String.IsNullOrEmpty(this.ShortDescription))
                    return this.ShortDescription;
                else if (!String.IsNullOrEmpty(this.MediumDescription))
                    return this.MediumDescription;
                else if (!String.IsNullOrEmpty(this.LongDescription))
                    return this.LongDescription;
                else
                    return String.Empty;
            }
        }
    }
}
