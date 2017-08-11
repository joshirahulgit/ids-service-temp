using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class ReferringNote : CommentItem
    {
        public bool isAlert { get; set; }
        public string OrderId { get; set; }
        public int AppointmentId { get; set; }

        public ReferringNote()
        {
        }

        public ReferringNote(CommentItem ci)
        {
            this.Id = ci.Id;
            this.Text = ci.Text;
            this.Time = ci.Time;
            this.Type = ci.Type;
        }

        //public static ReferringNoteDto Convert2Dto(ReferringNote note)
        //{
        //    CommentItemDto ci = CommentItem.Convert2Dto((CommentItem)note);
        //    if (ci != null)
        //    {
        //        ReferringNoteDto dto = new ReferringNoteDto(ci);
        //        dto.isAlert = note.isAlert;
        //        dto.OrderId = note.OrderId;
        //        dto.AppointmentId = note.AppointmentId;
        //        return dto;
        //    }
        //    else return null;
        //}

        //public static ReferringNote ExtractFromDto(ReferringNoteDto noteDto)
        //{
        //    ReferringNote res = new ReferringNote();
        //    res.isAlert = noteDto.isAlert;
        //    res.OrderId = noteDto.OrderId;
        //    res.AppointmentId = noteDto.AppointmentId;
        //    res.Id = noteDto.Id;
        //    res.Text = noteDto.Text;
        //    res.Time = noteDto.Time;
        //    //Sunil, please review if we really need this!!!
        //    //  res.Type = Scheduler.BusinessDomain.Entities.Types.CommentType.ExtractFromDto(noteDto.Type);
        //    return res;
        //}
    }

    public class ReferringNotes
    {
        public List<ReferringNote> Notes { get; set; }

        public ReferringNotes()
        {
            Notes = new List<ReferringNote>();
        }

        public ReferringNotes(List<ReferringNote> notes)
        {
            Notes = notes;
        }

        //public static ReferringNotesDto Convert2Dto(ReferringNotes notes)
        //{
        //    ReferringNotesDto res = new ReferringNotesDto();
        //    foreach (ReferringNote note in notes.Notes)
        //    {
        //        res.Notes.Add(ReferringNote.Convert2Dto(note));
        //    }
        //    return res;
        //}
    }
}
