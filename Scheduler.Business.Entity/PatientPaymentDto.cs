using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientPaymentDto : DtoBase
    {
        public long Id { get; set; }

        public int PatientIntId { get; set; }

        public int PatientPaymentOrderID { get; set; }

        public long? AppointmentId { get; set; }

        public string OrderId { get; set; }

        public decimal PaymentAmount { get; set; }

        public bool IsByCreditCardNew { get; set; }

        public bool IsByCreditCardAuth { get; set; }

        public bool IsByCheque { get; set; }

        public bool IsByCash { get; set; }

        public string CreditCardAuthorization { get; set; }

        public CreditCardPaymentDto CreditCardInfo { get; set; }

        public ChequePaymentDto ChequeInfo { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentStatus { get; set; }

        public string PaymentDetails { get; set; }

        public string ModeOfPayment { get; set; }

        public string ProcedureCode { get; set; }

        public string ScheduleFeeName { get; set; }

        public string ProcedureDescription { get; set; }

        public int ScheduleFeeId { get; set; }

        public string Comment { get; set; }

        public int? CollectedLocationId { get; set; }

        public string CollectedLocationName { get; set; }

        public PatientPaymentDto() { }

        public PatientPaymentDto(PatientPaymentDto payment)
        {
            this.PatientIntId = payment.PatientIntId;
            this.PatientPaymentOrderID = payment.PatientPaymentOrderID;
            this.AppointmentId = payment.AppointmentId;
            this.OrderId = payment.OrderId;
            this.PaymentAmount = payment.PaymentAmount;
            this.IsByCreditCardNew = payment.IsByCreditCardNew;
            this.IsByCreditCardAuth = payment.IsByCreditCardAuth;
            this.IsByCheque = payment.IsByCheque;
            this.IsByCash = payment.IsByCash;
            this.CreditCardAuthorization = payment.CreditCardAuthorization;
            this.CreditCardInfo = payment.CreditCardInfo == null ? null : new CreditCardPaymentDto(payment.CreditCardInfo);
            this.ChequeInfo = payment.ChequeInfo == null ? null : new ChequePaymentDto(payment.ChequeInfo);
            this.PaymentDate = payment.PaymentDate;
            this.PaymentStatus = payment.PaymentStatus;
            this.PaymentDetails = payment.PaymentDetails;
            this.ModeOfPayment = payment.ModeOfPayment;
            this.ProcedureCode = payment.ProcedureCode;
            this.ProcedureDescription = payment.ProcedureDescription;
            this.CollectedLocationId = payment.CollectedLocationId;
            this.CollectedLocationName = payment.CollectedLocationName;

            this.Comment = payment.Comment;
        }
    }

    public class PatientPaymentsDto : DtoBase
    {
        public PatientPaymentsDto()
        {
            PatientPayments = new List<PatientPaymentDto>();
        }

        public IList<PatientPaymentDto> PatientPayments { get; set; }
    }
}
