using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Business.Entity;
using Scheduler.Core;

namespace Scheduler.Business.Implementation
{
    public class ImageService : ServiceBase, IImageService
    {
        public ImageDto AddPatientImage(long id, ImageDto image)
        {
            throw new NotImplementedException();
        }

        public void DeleteImage(ImageDto image)
        {
            throw new NotImplementedException();
        }

        public void DeletePatientImage(long id, ImageDto image)
        {
            throw new NotImplementedException();
        }

        public ImagesDto DownloadChequeImages(int pOrderId)
        {
            throw new NotImplementedException();
        }

        public ImagesDto DownloadImage(ImageType type, long imageId)
        {
            throw new NotImplementedException();
        }

        public ImagesDto DownloadImagesForPatient(string mrn)
        {
            throw new NotImplementedException();
        }

        public ImageDto UploadImage(ImageDto image)
        {
            throw new NotImplementedException();
        }

        public ImageDto UploadImageFileByWorkType(ImageDto image, string imageWorkType, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
