using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public enum AppointmentSearchModeType
    {
        AND = 1,
        OR = 2
    }

    public class AppointmentStatusDto : DtoBase
    {
        private List<AppointmentStatusTransitionDto> _allowedTransition = new List<AppointmentStatusTransitionDto>();

        public long StatusID { get; set; }

        public String StatusName { get; set; }

        public String Color { get; set; }

        public string AppliedStatusName { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsSystemStatus { get; set; }

        public int SortIndex { get; set; }

        public List<AppointmentStatusTransitionDto> AllowedTransition
        {
            get { return _allowedTransition; }
            set { _allowedTransition = value; }
        }

        public string Text
        {
            get { return StatusName; }
        }

        public string SystemName
        {
            get
            {
                if (StatusID > 0)
                {
                    AppointmentStatuses s = (AppointmentStatuses)StatusID;
                    return s.ToString();
                }
                return "";
            }
        }


        public override string ToString()
        {
            return Text;
        }
    }


    public class AppointmentStatusTransitionDto
    {
        public AppointmentStatusTransitionDto(int statusId, bool patientViewSpecific)
        {
            StatusId = statusId;
            PatientViewSpecific = patientViewSpecific;
        }

        public int StatusId { get; set; }

        public bool PatientViewSpecific { get; set; }
    }

    public class AppointmentStatusesDto : DtoBase
    {
        public IList<AppointmentStatusDto> Statuses { get; set; }
    }
}
