using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class CommentItemDto : DtoBase
    {

        public CommentItemDto()
        {
        }

        public CommentItemDto(CommentTypeDto type, String text, String creator)
        {
            this.Type = type;
            this.Text = text;
            this.Creator = creator;
            this.Time = DateTime.Now;
        }

        public long Id { get; set; }

        public CommentTypeDto Type { get; set; }

        public String Text { get; set; }

        public DateTime Time { get; set; }

        public String Creator { get; set; }

        public string TextString
        {
            get
            {
                return string.Format("{0} ({1} {4}){2}{3}", Creator, Time.ToString("HH:mm:ss MM/dd/yyyy"), Environment.NewLine, Text, this.Type.TypeName);
            }
        }

        public string PrintReportTextString
        {
            get { return string.Format("{0} - {1}", this.Type.TypeName, Text); }
        }

        public override string ToString()
        {
            return string.Format("({0}) {1}", this.Type.TypeName, Text);
        }
    }


    public class ReferringNoteDto : CommentItemDto
    {

        public ReferringNoteDto() { }

        public ReferringNoteDto(CommentItemDto ci)
        {
            this.Id = ci.Id;
            this.Text = ci.Text;
            this.Time = ci.Time;
            this.Type = ci.Type;
        }

        public bool isAlert { get; set; }

        public new string TextString
        {
            get { return string.Format("{0} - {1}", this.Type.TypeName, Text); }
        }

        public string OrderId { get; set; }

        public int AppointmentId { get; set; }

    }


    public class ReferringNotesDto : DtoBase
    {
        public ReferringNotesDto()
        {
            this.Notes = new List<ReferringNoteDto>();
        }

        public List<ReferringNoteDto> Notes { get; set; }
    }

    public class ReferringNotesBatchDto : DtoBase
    {
        public ReferringNotesBatchDto()
        {
            this.NotesBatch = new Dictionary<string, ReferringNotesDto>();
        }

        public Dictionary<string, ReferringNotesDto> NotesBatch { get; set; }
    }
}
