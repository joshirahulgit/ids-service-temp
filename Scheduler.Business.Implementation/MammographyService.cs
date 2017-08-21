using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Business.Entity;

namespace Scheduler.Business.Implementation
{
    public class MammographyService : ServiceBase, IMammographyService
    {
        public MammographyHistoriesDto FindMammoHistories(int pathId)
        {
            throw new NotImplementedException();
        }

        public MammographyDto GetMammographyByAppointmentId(long appointmentId)
        {
            throw new NotImplementedException();
        }

        public MammographyDto GetMammographyByJobId(string jobId)
        {
            throw new NotImplementedException();
        }

        public MammographyDto GetMammographyByOrderId(string orderId)
        {
            throw new NotImplementedException();
        }

        public MammographyDto GetMammographyByVisitId(int visitId)
        {
            throw new NotImplementedException();
        }

        public MammographyDto ManageMammography(MammographyDto mammo)
        {
            throw new NotImplementedException();
        }
    }
}
