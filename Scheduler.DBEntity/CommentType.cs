using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CommentType : EntityType, IEquatable<CommentType>
    {
        public bool IsVisible { get; set; }
        public bool IsSystem { get; set; }
        public bool IsDeleted { get; set; }
        public string CannedCommentEnumType { get; set; }

        public CommentType(long id, string name, bool isVisible, bool isSystem, string cannedCommentEnumType)
            : this(id, name, isVisible, isSystem)
        {
            this.CannedCommentEnumType = cannedCommentEnumType;
        }

        public CommentType(long id, string name, bool isVisible, bool isSystem)
        {
            this.Id = id;
            this.Name = name;
            this.IsVisible = isVisible;
            this.IsSystem = isSystem;
        }

        //internal static CommentType ExtractFromDto(CommentTypeDto commentTypeDto)
        //{
        //    return new CommentType(commentTypeDto.TypeId, commentTypeDto.TypeName, commentTypeDto.IsVisible,
        //        commentTypeDto.IsSystem, commentTypeDto.CannedCommentEnumType)
        //    { IsDeleted = commentTypeDto.IsDeleted };
        //}

        //public static CommentTypeDto Convert2Dto(CommentType type)
        //{
        //    CommentTypeDto r = new CommentTypeDto();
        //    r.TypeId = type.Id;
        //    r.TypeName = type.Name;
        //    r.IsVisible = type.IsVisible;
        //    r.IsSystem = type.IsSystem;
        //    r.CannedCommentEnumType = type.CannedCommentEnumType;

        //    return r;
        //}

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            CommentType ct = obj as CommentType;
            if (ct == null)
            {
                return false;
            }
            return Eq(ct);
        }

        public bool Equals(CommentType other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Eq(other);
        }

        private bool Eq(CommentType ct)
        {
            return Name == ct.Name &&
                   IsVisible == ct.IsVisible &&
                   IsSystem == ct.IsSystem &&
                   IsDeleted == ct.IsDeleted;
        }

        public override int GetHashCode()
        {
            int hashResult = Name == null ? 0 : Name.GetHashCode();
            hashResult ^= IsVisible.GetHashCode();
            hashResult ^= IsSystem.GetHashCode();
            hashResult ^= IsDeleted.GetHashCode();
            return hashResult;
        }
    }
}
