using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientInvoice : EntityBase
    {
        public PatientInvoice()
        {
            PatientPayments = new List<PatientPayment>();
        }

        public DateTime CreatedDate { get; set; }
        public List<PatientPayment> PatientPayments { get; set; }
    }

    public class PatientInvoices : EntityBase
    {
        public PatientInvoices()
        {
            Invoices = new List<PatientInvoices>();
        }

        public List<PatientInvoices> Invoices { get; set; }
    }
}
