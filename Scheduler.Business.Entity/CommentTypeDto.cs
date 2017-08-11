using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class CommentTypeDto : EntityTypeDto
    {
        public CommentTypeDto()
        {
        }

        public bool IsDeleted { get; set; }

        public string CannedCommentEnumType { get; set; }


        public CommentTypeDto(long typeId) :
            this()
        {
            base.TypeId = typeId;
        }

        public CommentTypeDto(long typeId, string typeName)
            : this(typeId)
        {
            base.TypeName = typeName;
        }
    }

    public class CommentTypesDto //: DtoBase
    {
        public CommentTypesDto(IList<CommentTypeDto> types)
        {
            this.Types = types;
        }

        private IList<CommentTypeDto> _types;

        public IList<CommentTypeDto> Types
        {
            get { return _types; }
            set { _types = value; }
        }
    }
}
