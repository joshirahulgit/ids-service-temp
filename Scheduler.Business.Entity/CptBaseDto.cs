using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class CptBaseDto : DtoBase
    {
        private string _globalId;

        public CptBaseDto(long id, string code, string shortDescription, string mediumDescription, string longDescription, string guid, bool isGlobal)
        {
            Id = id;
            ShortDescription = shortDescription;
            MediumDescription = mediumDescription;
            Code = code;
            GlobalId = guid;
            LongDescription = longDescription;
            IsGlobal = isGlobal;
        }

        public CptBaseDto() { }

        public CptBaseDto(CptBaseDto dto) :
            this(dto.Id, dto.Code, dto.ShortDescription, dto.MediumDescription, dto.LongDescription, dto.GlobalId, dto.IsGlobal)
        {
            Category = dto.Category;
            AlertText = dto.AlertText;
            OnsetDate = dto.OnsetDate;
            Flag = dto.Flag;
        }
        public string Flag { get; set; }

        public long Id { get; set; }
        public string AlertText { get; set; }
        public string Amount { get; set; }
        public string Volume { get; set; }
        public string Code { get; set; }
        public DateTime? OnsetDate { get; set; }
        public string ShortDescription { get; set; }

        public string MediumDescription { get; set; }
        public string LongDescription { get; set; }

        public String GlobalId
        {
            get { return _globalId; }
            set { _globalId = value; }
        }

        public bool IsGlobal { get; set; }
        public String Category { get; set; }
        public long? LinkedRoomId { get; set; }
        public long? LinkedApptId { get; set; }

        public string DisplayText
        {
            get { return this.ToString(); }
        }

        public override string ToString()
        {
            String desc = String.Empty;
            if (!String.IsNullOrEmpty(ShortDescription))
                desc = ShortDescription;
            //else if (!String.IsNullOrEmpty(MediumDescription))
            //    desc = MediumDescription;
            else if (!String.IsNullOrEmpty(LongDescription))
                desc = LongDescription;

            string ret;

            if (String.IsNullOrEmpty(desc))
            {
                if (String.IsNullOrEmpty(Code))
                    return String.Empty;
                else
                    return String.Concat("(", Code, ")");
            }
            else
            {
                if (String.IsNullOrEmpty(Code))
                    ret = desc;
                else
                    ret = String.Concat(desc, " (", Code, ")");
            }
            return ret;
        }
    }
}
