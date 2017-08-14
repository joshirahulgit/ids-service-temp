using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientCommentDto : DtoBase
    {
        public PatientCommentDto()
        {
        }

        public PatientCommentDto(PatientCommentDto source)
        {
            this.CommentID = source.CommentID;
            this.Time = source.Time;
            this.Creator = source.Creator;
            this.Type = source.Type;
            this.Text = source.Text;
            this.CommentedEntityId = source.CommentedEntityId;
            this.CommentedEntityType = source.CommentedEntityType;
            this.TechnicianName = source.TechnicianName;
            this.IsAlert = source.IsAlert;
        }

        public PatientCommentDto(CommentTypeDto type, String text, String creator, bool isAlert, int? accountEnumId)
            : this(type, text, creator, isAlert)
        {
            this.AccountEnumId = accountEnumId;
        }

        public PatientCommentDto(CommentTypeDto type, String text, String creator, bool isAlert)
        {
            this.Type = type;
            this.Text = text;
            this.Creator = creator;
            this.Time = DateTime.Now;
            this.IsAlert = isAlert;
        }

        public long CommentID { get; set; }

        private CommentTypeDto _type;

        public CommentTypeDto Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _text;

        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string TextString
        {
            get
            {
                return string.Format("{0} ({1} {5} for {4}){2}{3}", Creator, Time.ToString("HH:mm:ss MM/dd/yyyy"), Environment.NewLine, Text,
              Enum.GetName(typeof(CommentedEntityTypes), this.CommentedEntityType), this.Type.TypeName);
            }
        }

        public string ReportString
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

        public bool IsAttachedToCurrentAppointment { get; set; }
#if SILVERLIGHT
        public Brush IsAlertColor
        {
            get { return IsAlert ? new SolidColorBrush(Color.FromArgb(255,255,101,101)) : new SolidColorBrush(Color.FromArgb(255, 4, 131, 249)); }
        }
#endif

        /*
                public Brush IsAlertColor
                {
                    get { return this.IsAlert ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Black);}
                }*/


        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public String Creator { get; set; }

        public String TechnicianName { get; set; }

        public String LocationTransferredTo { get; set; }

        public long? CommentedEntityId { get; set; }

        public CommentedEntityTypes CommentedEntityType { get; set; }

        public bool IsAlert { get; set; }

        public int? AccountEnumId { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1}) {2}", Enum.GetName(typeof(CommentedEntityTypes), CommentedEntityType), CommentedEntityId, Text);
        }
    }

    public class PatientCommentsDto : DtoBase
    {
        public IList<PatientCommentDto> Comments { get; set; }


        public static List<PatientCommentDto> GetCommentsPerAppointment(List<PatientCommentDto> comments,
                                                                       AppointmentDto appt)
        {

            List<PatientCommentDto> r = new List<PatientCommentDto>();

            List<PatientCommentDto> commentsToProcess = GetWithoutDublicates(comments, appt);
            foreach (PatientCommentDto dto in commentsToProcess)
            {
                if (dto.CommentedEntityId == appt.AppointmentID)
                    r.Add(dto);
            }

            return r;

        }


        public static List<PatientCommentDto> GetWithoutDublicates(List<PatientCommentDto> updatedComments, AppointmentDto appt)
        {
            List<PatientCommentDto> comments = new List<PatientCommentDto>();
            List<PatientCommentDto> apptComments;
            //            if(appt != null)
            //                apptComments = updatedComments.Where(a => a.CommentedEntityType == CommentedEntityTypes.Appointment &&
            //                    a.CommentedEntityId == appt.AppointmentID).Distinct(new DistinctComment()).ToList();
            //            else
            apptComments = updatedComments.Where(a => a.CommentedEntityType == CommentedEntityTypes.Appointment).
                //Distinct(new DistinctComment()).
                ToList();

            if (appt != null)
                updatedComments.ForEach(a => a.IsAttachedToCurrentAppointment = a.CommentedEntityType == CommentedEntityTypes.Appointment &&
                    a.CommentedEntityId == appt.AppointmentID);

            comments.AddRange(apptComments);
            var nonApptComments = updatedComments.Where(a => a.CommentedEntityType != CommentedEntityTypes.Appointment).ToList();
            comments.AddRange(nonApptComments);
            return comments.OrderByDescending(a => a.Time).ToList();
        }

        public static List<PatientCommentDto> GetWithoutCancelledDublicates(List<PatientCommentDto> updatedComments, AppointmentDto appt)
        {
            List<PatientCommentDto> comments = new List<PatientCommentDto>();
            List<PatientCommentDto> apptComments;
            //            if (appt != null)
            //                apptComments = updatedComments.Where(a => a.CommentedEntityType == CommentedEntityTypes.Appointment &&
            //                    a.CommentedEntityId == appt.AppointmentID).Distinct(new DistinctComment()).ToList();
            //            else
            apptComments = updatedComments.Where(a => a.CommentedEntityType == CommentedEntityTypes.Appointment).
                Distinct(new DistinctComment()).
                ToList();

            if (appt != null)
                updatedComments.ForEach(a =>
                        a.IsAttachedToCurrentAppointment = (a.CommentedEntityType == CommentedEntityTypes.Appointment &&
                             a.CommentedEntityId == appt.AppointmentID)
                    );

            comments.AddRange(apptComments);
            var nonApptComments = updatedComments.Where(a => a.CommentedEntityType != CommentedEntityTypes.Appointment).ToList();
            comments.AddRange(nonApptComments);
            return comments.OrderByDescending(a => a.Time).ToList();
        }

        //        public static List<PatientCommentDto> GetWithoutDublicates(List<PatientCommentDto> updatedComments)
        //        {
        //            List<PatientCommentDto> comments = new List<PatientCommentDto>();
        //            List<PatientCommentDto> apptComments = updatedComments.Where(a => a.CommentedEntityType == CommentedEntityTypes.Appointment).
        //                    Distinct(new DistinctComment()).ToList();
        //
        //            comments.AddRange(apptComments);
        //            var nonApptComments = updatedComments.Where(a => a.CommentedEntityType != CommentedEntityTypes.Appointment).ToList();
        //            comments.AddRange(nonApptComments);
        //            return comments.OrderByDescending(a=>a.Time).ToList();
        //        }
    }

    public class DistinctComment : IEqualityComparer<PatientCommentDto>
    {
        public bool Equals(PatientCommentDto x, PatientCommentDto y)
        {
            return x.CommentedEntityType == y.CommentedEntityType && x.Text == y.Text &&
                x.Time.Date == y.Time.Date && Math.Round(x.Time.TimeOfDay.TotalMinutes) == Math.Round(y.Time.TimeOfDay.TotalMinutes);
            //&&(!x.CommentedEntityId.HasValue || !y.CommentedEntityId.HasValue || (x.CommentedEntityId.Value != y.CommentedEntityId.Value));
        }

        public int GetHashCode(PatientCommentDto obj)
        {
            return obj.CommentedEntityType.GetHashCode() + obj.Text.GetHashCode();
        }
    }
}
