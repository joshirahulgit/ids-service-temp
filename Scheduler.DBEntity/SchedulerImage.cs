using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class SchedulerImage : EntityBase
    {
        public SchedulerImage(ImageType type, long imageId)
        {
            Id = imageId;
            Type = type;
        }

        public SchedulerImage(ImageType type, long imageId, byte[] data) : this(type, imageId)
        {
            ImageData = data;
        }

        public ImageType Type { get; private set; }

        public byte[] ImageData { get; private set; }

        public long PatientId { get; private set; }

        public string PatientNumber { get; private set; }


        //public static SchedulerImage ExtractFromDto(ImageDto imageDto)
        //{
        //    if (imageDto == null) return null;
        //    return new SchedulerImage(imageDto.Type, imageDto.ImageId, imageDto.ImageData);
        //}

        public void AttachPatient(AppointmentResourcePatient p)
        {
            this.PatientId = p.Id;
            this.PatientNumber = p.RecordNumber;
        }

        //public SchedulerImage Save(RepositoryLocator locator)
        //{
        //    return locator.ImageRepository.Create(this);
        //}

        //public SchedulerImage Save(RepositoryLocator locator, string imageWorkType, string fileName)
        //{
        //    return locator.ImageRepository.Create(this, imageWorkType, fileName);
        //}

        //public void Delete(RepositoryLocator locator)
        //{
        //    locator.ImageRepository.Remove(this);
        //}

        //public static ImageDto Convert2Dto(SchedulerImage image)
        //{
        //    return new ImageDto() { ImageData = image.ImageData, ImageId = image.Id, Type = image.Type };
        //}
    }
}
