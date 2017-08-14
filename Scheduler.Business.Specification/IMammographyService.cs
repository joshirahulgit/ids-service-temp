using Scheduler.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IMammographyService
    {
        MammographyDto ManageMammography(MammographyDto mammo);

        MammographyHistoriesDto FindMammoHistories(int pathId);

        MammographyDto GetMammographyByOrderId(string orderId);

        MammographyDto GetMammographyByAppointmentId(long appointmentId);

        MammographyDto GetMammographyByJobId(string jobId);

        MammographyDto GetMammographyByVisitId(int visitId);
    }
}
