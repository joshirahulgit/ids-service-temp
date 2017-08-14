using Scheduler.Core;
using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IImageRepository : IRepository, ICanGetById<SchedulerImage, long>, ICanCreate<SchedulerImage>, ICanRemove<SchedulerImage>
    {
        SchedulerImage GetByTypeAndId(ImageType type, long imageId);
        SchedulerImage[] GetByIdMultiPage(long imageId);
        SchedulerImage[] DownloadChequeImages(int pOrderId);
        // extend the create method
        SchedulerImage Create(SchedulerImage entity, string imageWorkType, string fileName);
    }
}
