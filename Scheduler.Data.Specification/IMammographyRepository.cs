using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IMammographyRepository : IRepository, ICanCreate<Mammography>, ICanUpdate<Mammography>, ICanRemove<Mammography>
    {
        void ManageMammography(string orderId, Mammography mammo);
        bool IsMammographyActive();
        Mammography GetMammographyByAppointmentId(long appointmentId);
        List<Surgeon> GetSurgeonWhos(int type, string filter);
        List<PathologyPathResults> FilterPathResults(string filter);
        List<MammographyHistory> GetMammographyHistory(long id);
        Mammography GetMammographyByJobId(string jobId);
        Mammography GetMammographyByVisitId(int visitId);
        Mammography GetMammographyByOrderId(string orderId);
        void UpdateMammographyWithOrderId(long id, string orderId);
        void RemoveTumor(long tumorId);
        void UpdateTumor(Tumor tumor);
        int CreateTumor(long mammographyDataId, Tumor tumor);
        void RemovePathologyDiagnosis(long diagId);
        void UpdatePathologyDiagnosis(PathologyDiagnosis newDiag);
        int CreatePathologyDiagnosis(long mpId, PathologyDiagnosis newDiag);
        void RemovePathologyResult(long resultId);
        void UpdatePathologyResult(PathologyDetail newResult);
        int CreatePathologyResult(long mpId, PathologyDetail newResult);
        void BackUp(long mammoId);
        Mammography GetMammographyById(long mammoId);
        int CreateNewMammographyPathDataEntry(int mammoId, string side);

    }
}
