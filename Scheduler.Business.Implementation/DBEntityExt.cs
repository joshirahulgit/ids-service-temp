using Scheduler.Business.Entity;
using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Implementation
{
    internal static class DBEntityExt
    {
        internal static AccountEnumDto ToAccountEnumDto(this AccountEnum accountEnum)
        {
            AccountEnumDto dto = new AccountEnumDto();
            dto.Id = accountEnum.Id;
            dto.Name = accountEnum.Name;
            dto.Value = accountEnum.Value;
            dto.IsVisible = accountEnum.IsVisible;
            dto.EnumType = accountEnum.EnumType;
            dto.IsDefault = accountEnum.IsDefault;
            dto.UserCanEdit = accountEnum.UserCanEdit;
            dto.UserCanDelete = accountEnum.UserCanDelete;
            return dto;
        }

        internal static AccountEnum ToAccountEnum(this AccountEnumDto acEnumDto)
        {
            AccountEnum res = new AccountEnum(acEnumDto.Id);
            res.Name = acEnumDto.Name;
            res.Value = acEnumDto.Value;
            res.IsVisible = acEnumDto.IsVisible;
            res.EnumType = acEnumDto.EnumType;
            res.IsDefault = acEnumDto.IsDefault;
            res.UserCanEdit = acEnumDto.UserCanEdit;
            res.UserCanDelete = acEnumDto.UserCanDelete;
            return res;
        }
    }
}
