using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientInvoiceDto : DtoBase
    {
        public int Id { get; set; }
        public PatientInvoiceDto()
        {
            PatientPayments = new List<PatientPaymentDto>();
        }

        public DateTime CreatedDate { get; set; }

        public List<PatientPaymentDto> PatientPayments { get; set; }

        public string Description
        {
            get
            {
                string result = string.Empty;
                foreach (PatientPaymentDto payment in PatientPayments)
                {
                    result += string.Format("{0} by {1};",
                        string.IsNullOrEmpty(payment.ProcedureCode) ?
                        "Appointment" :
                        string.Format("{0} ({1})", payment.ProcedureDescription, payment.ProcedureCode),
                        payment.ModeOfPayment);

                }
                return result;
            }
        }

        public decimal TotalCount
        {
            get { return PatientPayments.Sum(s => s.PaymentAmount); }
        }
    }

    public class PatientInvoicesDto : DtoBase
    {
        public PatientInvoicesDto()
        {
            Invoices = new List<PatientInvoiceDto>();
        }

        public List<PatientInvoiceDto> Invoices { get; set; }
    }
}
