using Scheduler.Business.Entity;
using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IImageService : IContract
    {

        ImagesDto DownloadImage(ImageType type, long imageId);

        ImageDto UploadImage(ImageDto image);

        ImageDto UploadImageFileByWorkType(ImageDto image, string imageWorkType, string fileName);

        void DeleteImage(ImageDto image);

        void DeletePatientImage(long id, ImageDto image);

        ImageDto AddPatientImage(long id, ImageDto image);

        ImagesDto DownloadImagesForPatient(string mrn);

        ImagesDto DownloadChequeImages(int pOrderId);
    }
}
