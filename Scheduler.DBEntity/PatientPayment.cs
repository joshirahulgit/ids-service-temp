using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientPayment : EntityBase
    {
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
        public CreditCardPayment CreditCardInfo { get; set; }
        public ChequePayment ChequeInfo { get; set; }
        public string ProcedureCode { get; set; }

        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public int ScheduleFeeId { get; set; }
        public string ScheduleFeeName { get; set; }
        public string Comment { get; set; }
        public string ProcedureDescription { get; set; }
        public int? CollectedLocationId { get; set; }
        public string CollectedLocationName { get; set; }


        public string ModeOfPayment
        {
            get
            {
                if (this.IsByCreditCardNew || this.IsByCreditCardAuth)
                    return "Credit Card";
                else if (this.IsByCheque)
                    return "Cheque";
                else if (this.IsByCash)
                    return "Cash";
                return "";
            }
        }

        public string PaymentDetails
        {
            get
            {
                StringBuilder stb = new StringBuilder();
                if (this.IsByCreditCardNew || this.IsByCreditCardAuth)
                {
                    if (this.CreditCardInfo != null)
                    {
                        stb.Append(string.IsNullOrEmpty(this.CreditCardInfo.CardType) ? "" : this.CreditCardInfo.CardType + ":");
                        stb.Append(string.IsNullOrEmpty(this.CreditCardInfo.CardNumber) ? "" :
                            " [***" + this.CreditCardInfo.CardNumber.GetLast(4) + "]");
                        stb.Append(string.IsNullOrEmpty(this.CreditCardAuthorization) ? "" : " #" + this.CreditCardAuthorization);
                    }
                }
                else if (this.IsByCheque)
                {
                    if (this.ChequeInfo != null)
                    {
                        stb.Append(string.IsNullOrEmpty(this.ChequeInfo.ChequeBankName) ? "" : this.ChequeInfo.ChequeBankName + ":");
                        stb.Append(string.IsNullOrEmpty(this.ChequeInfo.ChequeAccountNumber) ? "" :
                            " [***" + this.ChequeInfo.ChequeAccountNumber.GetLast(4) + "]");
                        stb.Append(string.IsNullOrEmpty(this.ChequeInfo.ChequeNumber) ? "" : " #" + this.ChequeInfo.ChequeNumber);
                    }
                }
                else if (this.IsByCash)
                    return "---";
                return stb.ToString();
            }
        }

        public PatientPayment() { }

        //public static PatientPaymentDto Convert2Dto(PatientPayment payment)
        //{
        //    PatientPaymentDto result = new PatientPaymentDto();
        //    result.Id = payment.Id;
        //    result.PatientIntId = payment.PatientIntId;
        //    result.PatientPaymentOrderID = payment.PatientPaymentOrderID;
        //    result.AppointmentId = payment.AppointmentId;
        //    result.OrderId = payment.OrderId;
        //    result.PaymentAmount = payment.PaymentAmount;
        //    result.IsByCash = payment.IsByCash;
        //    result.IsByCheque = payment.IsByCheque;
        //    result.IsByCreditCardNew = payment.IsByCreditCardNew;
        //    result.IsByCreditCardAuth = payment.IsByCreditCardAuth;

        //    if (!result.IsByCash)
        //        if (result.IsByCreditCardNew || result.IsByCreditCardAuth)
        //        {
        //            result.CreditCardAuthorization = payment.CreditCardAuthorization;
        //            result.CreditCardInfo = CreditCardPayment.Convert2Dto(payment.CreditCardInfo);
        //        }
        //        else if (result.IsByCheque)
        //            result.ChequeInfo = ChequePayment.Convert2Dto(payment.ChequeInfo);

        //    result.ModeOfPayment = payment.ModeOfPayment;
        //    result.PaymentDetails = payment.PaymentDetails;
        //    result.PaymentDate = payment.PaymentDate;
        //    result.PaymentStatus = payment.PaymentStatus;
        //    result.ProcedureCode = payment.ProcedureCode;
        //    result.ScheduleFeeId = payment.ScheduleFeeId;
        //    result.ScheduleFeeName = payment.ScheduleFeeName;
        //    result.Comment = payment.Comment;
        //    result.ProcedureDescription = payment.ProcedureDescription;
        //    result.CollectedLocationId = payment.CollectedLocationId;
        //    result.CollectedLocationName = payment.CollectedLocationName;
        //    return result;
        //}

        //internal static PatientPayment ExtractFromDto(PatientPaymentDto paymentDto)
        //{
        //    PatientPayment result = new PatientPayment();

        //    result.Id = paymentDto.Id;
        //    result.PatientIntId = paymentDto.PatientIntId;
        //    result.PatientPaymentOrderID = paymentDto.PatientPaymentOrderID;
        //    result.AppointmentId = paymentDto.AppointmentId;
        //    result.OrderId = paymentDto.OrderId;
        //    result.PaymentAmount = paymentDto.PaymentAmount;
        //    result.IsByCash = paymentDto.IsByCash;
        //    result.IsByCheque = paymentDto.IsByCheque;
        //    result.IsByCreditCardNew = paymentDto.IsByCreditCardNew;
        //    result.IsByCreditCardAuth = paymentDto.IsByCreditCardAuth;

        //    if (!result.IsByCash)
        //        if (result.IsByCreditCardNew || result.IsByCreditCardAuth)
        //        {
        //            result.CreditCardAuthorization = paymentDto.CreditCardAuthorization;
        //            if (paymentDto.CreditCardInfo != null) result.CreditCardInfo = CreditCardPayment.ExtractFromDto(paymentDto.CreditCardInfo);
        //        }
        //        else if (result.IsByCheque)
        //            if (paymentDto.ChequeInfo != null) result.ChequeInfo = ChequePayment.ExtractFromDto(paymentDto.ChequeInfo);

        //    result.PaymentDate = paymentDto.PaymentDate;
        //    result.PaymentStatus = paymentDto.PaymentStatus;
        //    result.ProcedureCode = paymentDto.ProcedureCode;
        //    result.ScheduleFeeId = paymentDto.ScheduleFeeId;
        //    result.ScheduleFeeName = paymentDto.ScheduleFeeName;
        //    result.Comment = paymentDto.Comment;
        //    result.ProcedureDescription = paymentDto.ProcedureDescription;
        //    result.CollectedLocationId = paymentDto.CollectedLocationId;
        //    result.CollectedLocationName = paymentDto.CollectedLocationName;
        //    return result;
        //}

        public void SetCCInfo(CreditCardPayment ccp)
        {
            CreditCardInfo = ccp;
        }

        public string GetAuditString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(" {0}", PaymentAmount.ToString("C"));

            if (IsByCash)
            {
                result.Append(", by cash");
            }

            if (IsByCheque)
            {
                result.Append(", by cheque");
            }

            if (IsByCreditCardNew || IsByCreditCardAuth)
            {
                result.Append(" ,by cheque");
            }

            if (!string.IsNullOrEmpty(Comment))
            {
                result.AppendFormat(" ({0})", Comment);
            }

            return result.ToString();
        }

        public void SetId(int id)
        {
            this.Id = id;
        }
    }

    public class PatientPaymentCollection : EntityBase
    {
        public IList<PatientPayment> PatientPayments { get; set; }
    }
}
