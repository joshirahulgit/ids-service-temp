using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class ImageDto : DtoBase
    {
        public ImageType Type { get; set; }
        public long ImageId { get; set; }
        public byte[] ImageData { get; set; }

        public ImageDto()
        {

        }

        public ImageDto(ImageDto image)
            : this()
        {
            this.Type = image.Type;
            this.ImageId = image.ImageId;
            this.ImageData = image.ImageData.Clone() as byte[];
        }
    }

    public class ImagesDto : DtoBase
    {
        public List<ImageDto> Frames { get; set; }

        public ImagesDto()
        {
            this.Frames = new List<ImageDto>();
        }

        public void Add(ImageDto p)
        {
            this.Frames.Add(p);
        }

        public void AddRange(ImagesDto set)
        {
            this.Frames.AddRange(set.Frames);
        }
    }
}
