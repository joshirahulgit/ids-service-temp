using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class OrderCreateParametersDto : DtoBase
    {
        public String OrderId { get; set; }

        public DateTime DateOfService { get; set; }

        public String PatientId { get; set; }

        public int LocationId { get; set; }//this stores scheduler LocationId which is INT. When creating order this id is converted to Abbadox's Location.Location

        public String Location { get; set; }

        public String WorkTypeId { get; set; }

        public String Reason { get; set; }

        public String VisitType { get; set; }

        public String Dictator { get; set; }

        public String DictatorId { get; set; }

        public String Priority { get; set; }

        public String ExamCode { get; set; }

        public DateTime DateReceive { get; set; }

        public String Physician { get; set; }

        public String PhysicianId { get; set; }

        public String RecurringSeriesId { get; set; }

        public String Account { get; set; }

        public String UserId { get; set; }

        public long AppointmentId { get; set; }

        public String AppointmentItemId { get; set; }

        public String RoomName { get; set; }

        public long? AppointmentItemType { get; set; }

        public string Modality { get; set; }

        public string ExamDescription { get; set; }

        public List<ReferralDto> CC { get; set; }

        public bool IsUpdateToMammoRequired { get; set; }
    }

    public class OrderCreateParametersSetDto : DtoBase
    {
        public List<OrderCreateParametersDto> Set { get; set; }

        public OrderCreateParametersSetDto()
        {
            this.Set = new List<OrderCreateParametersDto>();
        }

        public void Add(OrderCreateParametersDto p)
        {
            this.Set.Add(p);
        }

        public void AddRange(OrderCreateParametersSetDto set)
        {
            this.Set.AddRange(set.Set);
        }
    }
}
